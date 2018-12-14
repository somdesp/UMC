using PFC.DAO;
using System.Web.Mvc;

namespace PFC.Controllers
{
    public class ListasController : Controller
    {
        /////////////////////////////Listar Entidades sem Authorize //////////////////

        [HttpGet]
        public JsonResult GetGenero()
        {
            GeneroDAO gen = new GeneroDAO();
            return Json(gen.ListarGenero(), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetCurso()
        {
            CursoDAO curso = new CursoDAO();
            return Json(curso.ListarCurso(), JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetAutorização()
        {
            return View();
        }
        //////////////////////////////////FIM///////////////////////////////////////////////////////
    }
}