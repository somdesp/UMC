using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
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
            HankBLL hank = new HankBLL();
            return hank.ListarUsuariosHank().OrderByDescending(x => x.avaliacao.pontos).Distinct().ToList();

        }

        #endregion
    }
}
