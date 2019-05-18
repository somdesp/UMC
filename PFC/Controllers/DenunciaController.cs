using Microsoft.AspNet.Identity;
using PFC.Business;
using PFC.Model;
using System.Threading.Tasks;
using System.Web.Http;

namespace PFC.Controllers
{
    [Authorize]
    public class DenunciaController : ApiController
    {
        DenunciaBLL topicoBll = new DenunciaBLL();


        [AcceptVerbs("POST")]
        public async Task<bool> DenunciaUsuario([FromBody]Denuncia denuncia)
        {

            return await topicoBll.DenunciaUsuario(denuncia);

        }

        [AcceptVerbs("POST")]
        public async Task<bool> RemoverResposta([FromBody]Denuncia denuncia)
        {

            return await topicoBll.DenunciaUsuario(denuncia);

        }


        
    }
}
