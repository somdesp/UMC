using PFC.Business;
using PFC.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace PFC.Controllers
{
    [Authorize]
    public class NotificacaoController : ApiController
    {
        NotificacaoBLL notificacaoBLL = new NotificacaoBLL();

        #region AmizadeSOlicitada
        [AcceptVerbs("POST")]
        public async Task<List<Amizade>> NotificacaoAmizade([FromBody]Usuario usuario)
        {
           var notif = await notificacaoBLL.NotificacaoAmizade(usuario);

            if (notif.Count > 0)
            {
                return notif;
            }
          
            return null;

        }
        #endregion

        #region VerificaNotificacaoAmizade
        [AcceptVerbs("POST")]
        public async Task<bool> VerificaNotificacaoAmizadeAsync([FromBody]Usuario usuario)
        {
            return await notificacaoBLL.VerificaNotificacaoAmizadeAsync(usuario);
        }
        #endregion        

        #region VerificaNotificacaoAmizade
        [AcceptVerbs("POST")]
        [Authorize(Roles = "Master,Moderador")]
        public async Task<bool> VerificaNotificacaoDenunciaAsync([FromBody]Usuario usuario)
        {
            return await notificacaoBLL.VerificaNotificacaoDenunciaAsync(usuario);
        }
        #endregion

        #region Denuncia
        [AcceptVerbs("POST")]
        public async Task<List<Denuncia>> NotificacaoDenunciaAsync([FromBody]Usuario usuario)
        {
            var notif = await notificacaoBLL.NotificacaoDenunciaAsync(usuario);

            if (notif.Count > 0)
            {
                return notif;
            }

            return null;

        }
        #endregion
    }
}
