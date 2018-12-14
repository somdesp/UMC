using Microsoft.AspNet.Identity;
using PFC.DAO;
using PFC.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PFC.Controllers
{
    [Authorize]
    public class TemaController : Controller
    {


        // GET: Temas
        public ActionResult Index()
        {

            return View();
        }


        [Authorize(Roles = "Admin")]
        // GET: Temas/Create
        public ActionResult Create()
        {
            return View();
        }

        #region Criar tema
        // POST: Temas/Create
        [HttpPost]
        public ActionResult AdicionarTema(Tema tema)
        {
            Boolean retorno = false;
            TemaDAO temaDAO = new TemaDAO();
            tema.usuario.Id = User.Identity.GetUserId<int>();
            if (ModelState.IsValid)
            {
                temaDAO.AdicionarTema(tema);
                retorno = true;
            }
            return Json(retorno);
        }
        #endregion

        #region Atualizar tema
        [Authorize(Roles = "Admin")]
        public ActionResult AtualizarTema(Tema tema)
        {

            TemaDAO temaDAO = new TemaDAO();

            try
            {
                return Json(temaDAO.AtualizarTema(tema));
            }
            catch
            {
                return Json(false);
            }
        }
        #endregion

        #region Excluir tema        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ExcluirTema(Tema tema, FormCollection collection)
        {
            TemaDAO temaDAO = new TemaDAO();
            return Json(temaDAO.ExcluirTema(tema));
        }
        #endregion

        #region Listar temas
        [HttpGet]
        public JsonResult GetTema()
        {
            TemaDAO temaDao = new TemaDAO();
            return Json(temaDao.ListarTema(), JsonRequestBehavior.AllowGet);

        }
        #endregion

        public ActionResult TemaSelecionado(int id)
        {           

            return View();
        }

        // POST: Temas/Edit/5
        [HttpPost]
        public ActionResult EditarTema(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Temas/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }


        /////////////////////CONSULTA TEMA/////////////////
            


    }
}