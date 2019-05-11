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
        public async Task<List<Solicitacao>> NotificacaoAmizade([FromBody]Usuario usuario)
        {
            SolicitacaoBLL amizadeBll = new SolicitacaoBLL();
           var notif = await amizadeBll.NotificacaoAmizade(usuario);

            if (notif.Count > 0)
            {
                return notif;
            }
          
            return null;

        }
        #endregion

        #region VerificaNotificacaoAmizade
        [AcceptVerbs("POST")]
        public async Task<bool> VerificaNotificacaoAmizade([FromBody]Usuario usuario)
        {
            return await notificacaoBLL.VerificaNotificacaoAmizadeAsync(usuario);
        }
        #endregion
    }
}
