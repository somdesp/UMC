﻿using System.Threading.Tasks;
using System.Web.Http;
using PFC.Business;
using PFC.Model;

namespace PFC.Controllers
{
    [Authorize]
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

        #region AmizadeSOlicitada
        [AcceptVerbs("POST")]
        public async Task<bool> AmizadeSolicitada([FromBody]Amizade amizade)
        {
            AmizadeBLL amizadeBll = new AmizadeBLL();
            //NotificacaoHub notificacaoHub = new NotificacaoHub();
            //notificacaoHub.EnvioMensSoli(amizade);

            return (await amizadeBll.SolicitacaoAmizade(amizade.usuario, amizade.usuarioSolicitado));

        }
        #endregion

        #region ValidaAmizade
        [AcceptVerbs("POST")]
        public async Task<int> ValidaAmizade([FromBody]Amizade amizade)
        {
            AmizadeBLL amizadeBll = new AmizadeBLL();
            return (await amizadeBll.ValidaAmizade(amizade.usuario, amizade.usuarioSolicitado));

        }
        #endregion

        #region AceitaAmizade
        [AcceptVerbs("POST")]
        public async Task<bool> AceitaAmizade([FromBody]Amizade amizade)
        {
            AmizadeBLL amizadeBll = new AmizadeBLL();
            return (await amizadeBll.AceitaAmizade(amizade.usuario, amizade.usuarioSolicitado));

        }
        #endregion

        #region CancelaAmizade
        [AcceptVerbs("POST")]
        public async Task<bool> CancelaAmizade([FromBody]Amizade amizade)
        {
            AmizadeBLL amizadeBll = new AmizadeBLL();
            return (await amizadeBll.CancelaAmizade(amizade.usuario, amizade.usuarioSolicitado));

        }
        #endregion

        #region Carrega Lista de amizade do Chat



        #endregion
    }
}