using PFC.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PFC.Controllers
{
    public class SemestreController : Controller
    {
        [HttpPost]
        public async  Task<JsonResult> GetSemestre(string CursoId)
        {
            SemestreDAO semestre = new SemestreDAO();
            return Json(await semestre.ListarSemestre(Convert.ToInt32(CursoId)), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<JsonResult> GetSemestre()
        {
            SemestreDAO semestre = new SemestreDAO();
            return Json(await semestre.ListarSemestre(), JsonRequestBehavior.AllowGet);
        }

    }
}