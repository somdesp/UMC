using PFC.Business;
using PFC.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace PFC.Controllers
{
    [Authorize]
    public class DenunciaController : ApiController
    {
        DenunciaBLL denunciaBll = new DenunciaBLL();


        [AcceptVerbs("POST")]
        public async Task<bool> DenunciaUsuario([FromBody]Denuncia denuncia)
        {

            return await denunciaBll.DenunciaUsuario(denuncia);

        }

        [AcceptVerbs("POST")]
        public async Task<bool> RemoverResposta([FromBody]Denuncia denuncia)
        {

            return await denunciaBll.RemoverResposta(denuncia);

        }

        [AcceptVerbs("POST")]
        [Authorize(Roles = "Master,Moderador")]
        public async Task<List<Denuncia>> ListaDenuncia()
        {

            return await denunciaBll.ListaDenuncia();

        }

        


    }
}
