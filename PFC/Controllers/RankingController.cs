using Hangfire;
using PFC.Business;
using System;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using PFC.Model;
using System.Collections.Generic;

namespace PFC.Controllers
{
    public class RankingController : Controller
    {
        #region Listar Usuario Rank
        [Authorize]
        [HttpPost]
        public JsonResult ListarRank()
        {
            RankBLL rank = new RankBLL();
            List<Usuario> resultado = new List<Usuario>();


            RecurringJob.AddOrUpdate("RankingDiario",() => rank.ExecutarRankDiarioJob(), Cron.Daily);
            if(rank.ListarRank().Count==0)
            {
                resultado = null;
            }
            else
            {
                resultado = rank.ListarRank();
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }
        #endregion
        [Authorize]
        [HttpPost]
        public JsonResult ListarRankSemanal()
        {
            RankBLL rank = new RankBLL();


            RecurringJob.AddOrUpdate("RankingSemanal", () => rank.ExecutarRankSemanalJob(), Cron.Weekly(DayOfWeek.Saturday));

            return Json(rank.ListarRankSemanal(), JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        [HttpPost]
        public JsonResult ListarRankMensal()
        {
            RankBLL rank = new RankBLL();


            RecurringJob.AddOrUpdate("RankingMensal", () => rank.ExecutarRankMensalJob(), Cron.Monthly);

            return Json(rank.ListarRankMensal(), JsonRequestBehavior.AllowGet);

        }

    }
}
