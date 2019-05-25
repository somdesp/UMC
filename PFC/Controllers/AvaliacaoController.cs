using PFC.Model;
using System.Web.Mvc;
using PFC.Business;
using System.Threading.Tasks;

namespace PFC.Controllers
{
    public class AvaliacaoController : Controller
    {
        #region Método de avaliacao de pontos retornar todos os pontos correspondentes
        [HttpPost]
        public async Task<JsonResult> AvaliacaoPontos(Avaliacao avaliacao)
        {
            //int pontosAvaliacao = Convert.ToInt16(TopicosSelc);

            AvaliacaoBLL avaliacaobussiness = new AvaliacaoBLL();
            avaliacao = await avaliacaobussiness.inserirPontos(avaliacao);

            return Json(avaliacao, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
        #region Método de avaliacao de pontos retornar todos os pontos correspondentes LikeDeslike
        [HttpPost]
        public async Task<JsonResult> AvaliacaoPontosDeslike(Avaliacao avaliacao)
        {
            //int pontosAvaliacao = Convert.ToInt16(TopicosSelc);

            AvaliacaoBLL avaliacaobussiness = new AvaliacaoBLL();
            avaliacao = await avaliacaobussiness.inserirPontosLikeDeslike(avaliacao);

            return Json(avaliacao, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Apagar avaliacao inserida
        [HttpPost]
        public JsonResult ApagarAvaliacao(Avaliacao avaliacao)
        {
            AvaliacaoBLL deletabll = new AvaliacaoBLL();
            bool resposta = deletabll.ApagarAvaliacao(avaliacao);
            return Json(resposta, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        public async Task<JsonResult> consultarAvaliacao(Avaliacao avaliacao)
        {
            AvaliacaoBLL consultarbll = new AvaliacaoBLL();
            avaliacao = await consultarbll.consultaAvaliacao(avaliacao, avaliacao.idUsuario);
            return Json(avaliacao, JsonRequestBehavior.AllowGet);
        }


    }
}