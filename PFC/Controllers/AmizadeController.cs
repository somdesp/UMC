using System.Web.Http;
using PFC.Business;
using PFC.Model;

namespace PFC.Controllers
{

    public class AmizadeController : ApiController
    {
        #region UsuarioEscolhido
        //public JsonResult VisualizarPerfil(Usuario usuario)
        //{
        //    UsuarioBLL usuarioBll = new UsuarioBLL();
        //    usuario = usuarioBll.ConsultaUsuarioInt(usuario);
        //    return Json(usuario, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region AmizadeSOlicitada

        //public ActionResult AmizadeSolicitada(Usuario usuario, Usuario usuarioSolicitado)
        //{
        //    AmizadeBLL amizadeBll = new AmizadeBLL();
        //    return Json(amizadeBll.solicitacaoAmizade(usuario, usuarioSolicitado),
        //        JsonRequestBehavior.AllowGet);

        //}
        #endregion

        #region ValidaAmizade

        [AcceptVerbs("POST")]
        public bool ValidaAmizade([FromBody]Amizade amizade)
        {
            AmizadeBLL amizadeBll = new AmizadeBLL();
            return (amizadeBll.ValidaAmizade(amizade.usuario, amizade.usuarioSolicitado));

        }

        #endregion
    }
}
