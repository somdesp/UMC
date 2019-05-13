using PFC.DAO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PFC.Controllers
{
    public class ListasController : Controller
    {
        /////////////////////////////Listar Entidades sem Authorize //////////////////

        [HttpGet]
        public async Task<JsonResult> GetGenero()
        {
            GeneroDAO gen = new GeneroDAO();
            return Json(await gen.ListarGenero(), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public async Task<JsonResult> GetCurso()
        {
            CursoDAO curso = new CursoDAO();
            return Json(await curso.ListarCurso(), JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetAutorização()
        {
            return View();
        }
        //////////////////////////////////FIM///////////////////////////////////////////////////////
    }
}