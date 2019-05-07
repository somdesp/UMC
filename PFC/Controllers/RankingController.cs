using Hangfire;
using PFC.Business;
using System;
using System.Web.Mvc;
using PFC.Model;
using System.Collections.Generic;
using System.Linq;

namespace PFC.Controllers
{
    public class RankingController : Controller
    {
        #region Listar Usuario Rank
        [Authorize]
        [HttpPost]
        [OutputCache(Duration = 120)]
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
        [OutputCache(Duration = 120)]
        public JsonResult ListarRankSemanal()
        {
            RankBLL rank = new RankBLL();
            List<Usuario> resultado = new List<Usuario>();


            RecurringJob.AddOrUpdate("RankingSemanal", () => rank.ExecutarRankSemanalJob(), Cron.Weekly(DayOfWeek.Saturday));
            if (rank.ListarRankSemanal().Count == 0)
            {
                resultado = null;
            }
            else
            {
                resultado = rank.ListarRankSemanal();
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        [HttpPost]
        [OutputCache(Duration = 120)]
        public JsonResult ListarRankMensal()
        {
            RankBLL rank = new RankBLL();
            List<Usuario> resultado = new List<Usuario>();


            RecurringJob.AddOrUpdate("RankingMensal", () => rank.ExecutarRankMensalJob(), Cron.Monthly);
            if (rank.ListarRankMensal().Count == 0)
            {
                resultado = null;
            }
            else
            {
                resultado = rank.ListarRankMensal();
            }


            return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        [HttpPost]
        public JsonResult ListarRankInicial()
        {
            RankBLL rank = new RankBLL();
            List<Usuario> resultado = new List<Usuario>();


            RecurringJob.AddOrUpdate("RankingInicial", () => rank.ListarUsuariosInicial(), Cron.Minutely);
            if (rank.ListarUsuariosInicial().Count == 0)
            {
                resultado = null;
            }
            else
            {
                resultado = rank.ListarUsuariosInicial().OrderByDescending(r=>r.avaliacao.pontos).ToList();
            }


            return Json(resultado, JsonRequestBehavior.AllowGet);

        }



    }
}
