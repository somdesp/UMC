using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PFC.Model;
using System.Security.Principal;

namespace PFC.Hubs
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        // ChatAppEntities db = new ChatAppEntities();

        public void say(string message)
        {
            Clients.All.hello();
            Trace.WriteLine(message);
        }
        static readonly HashSet<string> Rooms = new HashSet<string>();
        static List<LoginViewModel> loggedInUsers = new List<LoginViewModel>();
        //static List<Room> roomsWiseUser = new List<Room>();
        public string Login(string name )
        {
            var LoginViewModel = new LoginViewModel { Nome = name, ConnectionId = Context.ConnectionId, Id = 23, sex = "Male", memberType = "Re+gistered", fontColor = "red", status = Model.Status.Online.ToString() };
            Clients.Caller.rooms(Rooms.ToArray());
            Clients.Caller.setInitial(Context.ConnectionId, name);
            var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(loggedInUsers);
            loggedInUsers.Add(LoginViewModel);
            Clients.Caller.getOnlineUsers(sJSON);
            Clients.Others.newOnlineUser(LoginViewModel);
            return name;
        }

        public void SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.ConnectionId;
            var toUser = loggedInUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = loggedInUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);
            if (toUser != null && fromUser != null)
            {
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.Nome, message);
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.Nome, message);
            }
        }
        public void UpdateStatus(string status)
        {
            string userId = Context.ConnectionId;
            loggedInUsers.FirstOrDefault(x => x.ConnectionId == userId).status = status;
            //var fromUser = loggedInUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);                          
            Clients.Others.statusChanged(userId, status);

        }
        public void UserTyping(string connectionId, string msg)
        {
            var id = Context.ConnectionId;
            Clients.Client(connectionId).isTyping(id, msg);
        }
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = loggedInUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                loggedInUsers.Remove(item); // list = 
                var id = Context.ConnectionId;
                Clients.Others.newOfflineUser(item);
            }
            return base.OnDisconnected(true);
        }
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (loggedInUsers.Count(x => x.ConnectionId == id) == 0)
            {
                loggedInUsers.Add(new LoginViewModel { ConnectionId = id, Nome = userName });
                Clients.Caller.onConnected(id, userName, loggedInUsers);
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }
    }
}