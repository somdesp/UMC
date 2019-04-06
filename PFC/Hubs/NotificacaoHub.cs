using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using PFC.Model;

namespace PFC.Hubs
{
    [HubName("notification")]
    public class NotificacaoHub : Hub
    {
        public void EnvioMensSoli(Solicitacao solicitacao)
        {
            var msg = String.Format("Nova Solicitação Amizade: {0}", solicitacao.usuario.Nome);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificacaoHub>();
            hubContext.Clients.All.newContact(msg);


            //string fromUserId = Context.ConnectionId;
            using (MeHelpChat dc = new MeHelpChat())
            {
                var toUser = dc.ChatUsuDetal.FirstOrDefault(x => x.EmailID == solicitacao.usuarioSolicitado.Email);
                var fromUser = dc.ChatUsuDetal.FirstOrDefault(x => x.EmailID == solicitacao.usuario.Email);

                hubContext.Clients.Client(toUser.ConnectionId).newContact(msg);

                //if (toUser != null && fromUser != null)
                //{
                //    // send to 
                //    Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);

                //    // send to caller user
                //    Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);

                //}
            }

        }

        public static void SendMessageDeleteContact(Usuario contact)
        {
            var msg = String.Format("Contato removido: {0} <{1}>", contact.Nome, contact.Email);

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificacaoHub>();
            hubContext.Clients.All.deleteContact(msg);
        }



    }


}