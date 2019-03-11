using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PFC.Business;
using PFC.Business.Business;
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

       
        public JsonResult<bool> ValidaAmizade([FromBody] Usuario usuario, [FromBody] Usuario usuarioSolicitado)
        {
            AmizadeBLL amizadeBll = new AmizadeBLL();
            return Json (amizadeBll.ValidaAmizade(usuario, usuarioSolicitado));

        }

        #endregion
    }
}
