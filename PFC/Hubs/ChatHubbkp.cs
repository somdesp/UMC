using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using PFC.Business;
using PFC.Model;

namespace PFC.Hubs
{
    public class ChatHubbkp : Hub
    {
        public static string emailIDLoaded = "";

        public static void EnvioMensSoli(Solicitacao solicitacao)
        {
            var msg = String.Format("Nova Solicitação Amizade: {0} <{1}>", solicitacao.usuario.Nome, solicitacao.usuario.Email);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificacaoHub>();


            //string fromUserId = Context.ConnectionId;
            using (MeHelpChat dc = new MeHelpChat())
            {
                var toUser = dc.ChatUsuDetal.FirstOrDefault(x => x.EmailID == solicitacao.usuarioSolicitado.Email);
                var fromUser = dc.ChatUsuDetal.FirstOrDefault(x => x.EmailID == solicitacao.usuario.Email);

                hubContext.Clients.Client(toUser.ConnectionId).newContact(msg);

                if (toUser != null && fromUser != null)
                {
                    //    // send to 
                    //    Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);

                    //    // send to caller user
                    //    Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);

                }
            }

        }

        #region Connect
        public void Connect(LoginViewModel login)
        {
            emailIDLoaded = login.Email;
            string id = Context.ConnectionId;
            using (MeHelpChat dc = new MeHelpChat())
            {
                var item = dc.ChatUsuDetal.FirstOrDefault(x => x.EmailID == login.Email);
                if (item != null)
                {
                    dc.ChatUsuDetal.Remove(item);
                    dc.SaveChanges();

                    // Disconnect
                    Clients.All.onUserDisconnectedExisting(item.ConnectionId, item.UserName);
                }

                var Users = dc.ChatUsuDetal.ToList();
                if (Users.Where(x => x.EmailID == login.Email).ToList().Count == 0)
                {
                    var userdetails = new ChatUsuDetal
                    {
                        ConnectionId = id,
                        UserName = login.Nome,
                        EmailID = login.Email
                    };
                    dc.ChatUsuDetal.Add(userdetails);
                    dc.SaveChanges();
                    Users = dc.ChatUsuDetal.ToList();
                    // send to caller
                    SolicitacaoBLL solicitacaoBLL = new SolicitacaoBLL();
                    var userss = solicitacaoBLL.ListaAmizade(login.Id);
                    List<ChatUsuDetal> list = new List<ChatUsuDetal>();

                    //valida se achou amigos 
                    if (userss != null)
                    {
                        for (int i = 0; userss.Count > i; i++)
                        {
                            list.Add(Users.Find(x => x.EmailID == userss[i].Email));
                        }
                    }


                    var connectedUsers = list.ToList();
                    var CurrentMessage = dc.ChatMensDetal.ToList();
                    Clients.Caller.onConnected(id, login.Nome, connectedUsers, CurrentMessage);
                }


                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, login.Nome, login.Email);
            }
        }
        #endregion

        #region Disconnect
        public override Task OnDisconnected(bool stopCalled)
        {
            using (MeHelpChat dc = new MeHelpChat())
            {
                var item = dc.ChatUsuDetal.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (item != null)
                {
                    dc.ChatUsuDetal.Remove(item);
                    dc.SaveChanges();

                    
                    Clients.All.onUserDisconnected(Context.ConnectionId, item.UserName);
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
            using (MeHelpChat dc = new MeHelpChat())
            {
                var toUser = dc.ChatUsuDetal.FirstOrDefault(x => x.ConnectionId == toUserId);
                var fromUser = dc.ChatUsuDetal.FirstOrDefault(x => x.ConnectionId == fromUserId);
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
            using (MeHelpChat dc = new MeHelpChat())
            {
                List<PrivateChatMessage> msg = new List<PrivateChatMessage>();

                var v = (from a in dc.ChatPrivMensMaster
                         join b in dc.ChatPrivMensDetal on a.EmailID equals b.MasterEmailID into cc
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

            using (MeHelpChat dc = new MeHelpChat())
            {
                List<PrivateChatMessage> msg = new List<PrivateChatMessage>();
                var v = (from a in dc.ChatPrivMensMaster
                         join b in dc.ChatPrivMensDetal on a.EmailID equals b.MasterEmailID into cc
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
            using (MeHelpChat dc = new MeHelpChat())
            {
                var messageDetail = new ChatMensDetal
                {
                    UserName = userName,
                    Message = message,
                    EmailID = emailIDLoaded
                };
                dc.ChatMensDetal.Add(messageDetail);
                dc.SaveChanges();
            }
        }

        private void AddPrivateMessageinCache(string fromEmail, string chatToEmail, string userName, string message)
        {
            using (MeHelpChat dc = new MeHelpChat())
            {
                // Save master
                var master = dc.ChatPrivMensMaster.ToList().Where(a => a.EmailID.Equals(fromEmail)).ToList();
                if (master.Count == 0)
                {
                    var result = new ChatPrivMensMaster
                    {
                        EmailID = fromEmail,
                        UserName = userName
                    };
                    dc.ChatPrivMensMaster.Add(result);
                    dc.SaveChanges();
                }

                // Save details
                var resultDetails = new ChatPrivMensDetal
                {
                    MasterEmailID = fromEmail,
                    ChatToEmailID = chatToEmail,
                    Message = message
                };
                dc.ChatPrivMensDetal.Add(resultDetails);
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