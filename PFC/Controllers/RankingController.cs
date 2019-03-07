using Hangfire;
using PFC.Business;
using System;
using System.Web.Mvc;

namespace PFC.Controllers
{
    public class RankingController : Controller
    {
        #region Listar Usuario Rank
        [HttpPost]
        public JsonResult ListarRank()
        {
            RankBLL rank = new RankBLL();

                       
            RecurringJob.AddOrUpdate("RankingDiario",() => rank.ExecutarRankDiarioJob(), Cron.Daily);

            return Json(rank.ListarRank(), JsonRequestBehavior.AllowGet);

        }
        #endregion

        [HttpPost]
        public JsonResult ListarRankSemanal()
        {
            RankBLL rank = new RankBLL();


            RecurringJob.AddOrUpdate("RankingSemanal", () => rank.ExecutarRankSemanalJob(), Cron.Weekly(DayOfWeek.Saturday));

            return Json(rank.ListarRankSemanal(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ListarRankMensal()
        {
            RankBLL rank = new RankBLL();


            RecurringJob.AddOrUpdate("RankingMensal", () => rank.ExecutarRankSemanalJob(), Cron.Monthly);

            return Json(rank.ListarRankMensal(), JsonRequestBehavior.AllowGet);

        }

    }
}
