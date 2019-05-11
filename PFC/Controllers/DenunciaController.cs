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
        [AcceptVerbs("POST")]
        public async Task<bool> DenunciaUsuario([FromBody]Denuncia denuncia)
        {
            DenunciaBLL topicoBll = new DenunciaBLL();
            return await topicoBll.DenunciaUsuario(denuncia);

        }
    }
}
