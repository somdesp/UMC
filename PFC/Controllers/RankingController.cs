using System.Collections.Generic;
using System.Web.Http;
using PFC.Business;
using PFC.Model;

namespace PFC.Controllers
{
    public class RankingController : ApiController
    {
        #region Listar Usuario Rank
        [System.Web.Http.HttpPost]
        public List<Usuario> ListarRank()
        {
            RankBLL rank = new RankBLL();
            return rank.ListarRank();

        }

        #endregion
    }
}
