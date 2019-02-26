

using PFC.Business;
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
            return Json(rank.ListarRank(), JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}
