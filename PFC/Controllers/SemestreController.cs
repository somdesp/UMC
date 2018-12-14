using PFC.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PFC.Controllers
{
    public class SemestreController : Controller
    {
        [HttpPost]
        public JsonResult GetSemestre(string CursoId)
        {
            SemestreDAO semestre = new SemestreDAO();
            return Json(semestre.ListarSemestre(Convert.ToInt32(CursoId)), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetSemestre()
        {
            SemestreDAO semestre = new SemestreDAO();
            return Json(semestre.ListarSemestre(), JsonRequestBehavior.AllowGet);
        }

    }
}