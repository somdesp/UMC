using PFC.DAO;
using PFC.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace PFC.Controllers
{
    public class TemaApiController : ApiController
    {

        [System.Web.Mvc.HttpGet]
        public async Task<JsonResult> GetTema()
        {
            TemaDAO temaDao = new TemaDAO();
            return Json(await temaDao.ListarTema(), JsonRequestBehavior.AllowGet);

        }

        private JsonResult Json(List<Tema> list, JsonRequestBehavior allowGet)
        {
            throw new NotImplementedException();
        }
    }
}
