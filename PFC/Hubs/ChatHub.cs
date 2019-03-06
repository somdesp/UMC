using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using PFC.DAO;
using PFC.Model;

namespace PFC.Hubs
{
    public class ChatHub : Hub
    {
        public static string emailIDLoaded = "";

        #region Connect
        public void Connect(LoginViewModel login)
        {
            emailIDLoaded = login.Email;
            var id = Context.ConnectionId;
            using (SignalREntities dc = new SignalREntities())
            {
                var item = dc.ChatUserDetail.FirstOrDefault(x => x.EmailID == login.Email);
                if (item != null)
                {
                    dc.ChatUserDetail.Remove(item);
                    dc.SaveChanges();

                    // Disconnect
                    Clients.All.onUserDisconnectedExisting(item.ConnectionId, item.UserName);
                }

                var Users = dc.ChatUserDetail.ToList();
                if (Users.Where(x => x.EmailID == login.Email).ToList().Count == 0)
                {
                    var userdetails = new ChatUserDetail
                    {
                        ConnectionId = id,
                        UserName = login.Nome,
                        EmailID = login.Email
                    };
                    dc.ChatUserDetail.Add(userdetails);
                    dc.SaveChanges();

                    // send to caller
                    var connectedUsers = dc.ChatUserDetail.ToList();
                    var CurrentMessage = dc.ChatMessageDetail.ToList();
                    Clients.Caller.onConnected(id, login.Nome, connectedUsers, CurrentMessage);
                }

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, login.Nome, login.Email);
            }
        }
        #endregion

        #region Disconnect
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            using (SignalREntities dc = new SignalREntities())
            {
                var item = dc.ChatUserDetail.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (item != null)
                {
                    dc.ChatUserDetail.Remove(item);
                    dc.SaveChanges();

                    var id = Context.ConnectionId;
                    Clients.All.onUserDisconnected(id, item.UserName);
                }
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region Send_To_All
        public void SendMessageToAll(string userName, string message)
        {
            // store last 100 messages in cache
            AddAllMessageinCache(userName, message);

            // Broad cast message
            Clients.All.messageReceived(userName, message);
        }
        #endregion

        #region Private_Messages
        public void SendPrivateMessage(string toUserId, string message, string status)
        {
            string fromUserId = Context.ConnectionId;
            using (SignalREntities dc = new SignalREntities())
            {
                var toUser = dc.ChatUserDetail.FirstOrDefault(x => x.ConnectionId == toUserId);
                var fromUser = dc.ChatUserDetail.FirstOrDefault(x => x.ConnectionId == fromUserId);
                if (toUser != null && fromUser != null)
                {
                    if (status == "Click")
                        AddPrivateMessageinCache(fromUser.EmailID, toUser.EmailID, fromUser.UserName, message);

                    // send to 
                    Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);

                    // send to caller user
                    Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);
                }
            }
        }
        public List<PrivateChatMessage> GetPrivateMessage(string fromid, string toid, int take)
        {
            using (SignalREntities dc = new SignalREntities())
            {
                List<PrivateChatMessage> msg = new List<PrivateChatMessage>();

                var v = (from a in dc.ChatPrivateMessageMaster
                         join b in dc.ChatPrivateMessageDetails on a.EmailID equals b.MasterEmailID into cc
                         from c in cc
                         where (c.MasterEmailID.Equals(fromid) && c.ChatToEmailID.Equals(toid)) || (c.MasterEmailID.Equals(toid) && c.ChatToEmailID.Equals(fromid))
                         orderby c.ID descending
                         select new
                         {
                             UserName = a.UserName,
                             Message = c.Message,
                             ID = c.ID
                         }).Take(take).ToList();
                v = v.OrderBy(s => s.ID).ToList();

                foreach (var a in v)
                {
                    var res = new PrivateChatMessage()
                    {
                        userName = a.UserName,
                        message = a.Message
                    };
                    msg.Add(res);
                }
                return msg;
            }
        }

        private int takeCounter = 0;
        private int skipCounter = 0;
        public List<PrivateChatMessage> GetScrollingChatData(string fromid, string toid, int start = 10, int length = 1)
        {
            takeCounter = (length * start); // 20
            skipCounter = ((length - 1) * start); // 10

            using (SignalREntities dc = new SignalREntities())
            {
                List<PrivateChatMessage> msg = new List<PrivateChatMessage>();
                var v = (from a in dc.ChatPrivateMessageMaster
                         join b in dc.ChatPrivateMessageDetails on a.EmailID equals b.MasterEmailID into cc
                         from c in cc
                         where (c.MasterEmailID.Equals(fromid) && c.ChatToEmailID.Equals(toid)) || (c.MasterEmailID.Equals(toid) && c.ChatToEmailID.Equals(fromid))
                         orderby c.ID descending
                         select new
                         {
                             UserName = a.UserName,
                             Message = c.Message,
                             ID = c.ID
                         }).Take(takeCounter).Skip(skipCounter).ToList();

                foreach (var a in v)
                {
                    var res = new PrivateChatMessage()
                    {
                        userName = a.UserName,
                        message = a.Message
                    };
                    msg.Add(res);
                }
                return msg;
            }
        }
        #endregion

        #region Save_Cache
        private void AddAllMessageinCache(string userName, string message)
        {
            using (SignalREntities dc = new SignalREntities())
            {
                var messageDetail = new ChatMessageDetail
                {
                    UserName = userName,
                    Message = message,
                    EmailID = emailIDLoaded
                };
                dc.ChatMessageDetail.Add(messageDetail);
                dc.SaveChanges();
            }
        }

        private void AddPrivateMessageinCache(string fromEmail, string chatToEmail, string userName, string message)
        {
            using (SignalREntities dc = new SignalREntities())
            {
                // Save master
                var master = dc.ChatPrivateMessageMaster.ToList().Where(a => a.EmailID.Equals(fromEmail)).ToList();
                if (master.Count == 0)
                {
                    var result = new ChatPrivateMessageMaster
                    {
                        EmailID = fromEmail,
                        UserName = userName
                    };
                    dc.ChatPrivateMessageMaster.Add(result);
                    dc.SaveChanges();
                }

                // Save details
                var resultDetails = new ChatPrivateMessageDetails
                {
                    MasterEmailID = fromEmail,
                    ChatToEmailID = chatToEmail,
                    Message = message
                };
                dc.ChatPrivateMessageDetails.Add(resultDetails);
                dc.SaveChanges();
            }
        }
        #endregion
    }

    public class PrivateChatMessage
    {
        public string userName { get; set; }
        public string message { get; set; }
    }
}