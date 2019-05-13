using Hangfire;
using PFC.Business;
using System;
using System.Web.Mvc;
using PFC.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFC.Controllers
{
    public class RankingController : Controller
    {
        #region Listar Usuario Rank
        [HttpPost]
        [OutputCache(Duration = 120)]
        public async Task<JsonResult> ListarRank()
        {
            RankBLL rank = new RankBLL();
            List<Usuario> resultado = new List<Usuario>();


            RecurringJob.AddOrUpdate("RankingDiario",() => rank.ExecutarRankDiarioJob(), Cron.Daily);
            var  listRank = await rank.ListarRank();
            if (listRank.Count  == 0)
            {
                resultado = null;
            }
            else
            {
                resultado =await rank.ListarRank();
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }
        #endregion
        [HttpPost]
        [OutputCache(Duration = 120)]
        public async Task<JsonResult> ListarRankSemanal()
        {
            RankBLL rank = new RankBLL();
            List<Usuario> resultado = new List<Usuario>();


            RecurringJob.AddOrUpdate("RankingSemanal", () => rank.ExecutarRankSemanalJob(), Cron.Weekly(DayOfWeek.Saturday));
            var ListarRankSemanal = await rank.ListarRankSemanal();

            if (ListarRankSemanal.Count == 0)
            {
                resultado = null;
            }
            else
            {
                resultado = await rank.ListarRankSemanal();
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        [Authorize]
        [HttpPost]
        [OutputCache(Duration = 120)]
        public async Task<JsonResult> ListarRankMensal()
        {
            RankBLL rank = new RankBLL();
            List<Usuario> resultado = new List<Usuario>();


            RecurringJob.AddOrUpdate("RankingMensal", () => rank.ExecutarRankMensalJob(), Cron.Monthly);
            var ListarRankMensal = await rank.ListarRankMensal();
            if (ListarRankMensal.Count == 0)
            {
                resultado = null;
            }
            else
            {
                resultado =await rank.ListarRankMensal();
            }


            return Json(resultado, JsonRequestBehavior.AllowGet);

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> ListarRankInicial()
        {
            RankBLL rank = new RankBLL();
            List<Usuario> resultado = new List<Usuario>();


            RecurringJob.AddOrUpdate("RankingInicial", () => rank.ListarUsuariosInicial(), Cron.Minutely);
            var ListarUsuariosInicial =await rank.ListarUsuariosInicial();
            if (ListarUsuariosInicial.Count == 0)
            {
                resultado = null;
            }
            else
            {
               
                resultado = ListarUsuariosInicial.OrderByDescending(r => r.avaliacao.pontos).ToList();
            }


            return Json(resultado, JsonRequestBehavior.AllowGet);

        }



    }
}
