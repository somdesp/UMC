using PFC.Model;
using System;
using System.Web.Mvc;
using PFC.Business.Business;

namespace PFC.Controllers
{
    public class AvaliacaoController : Controller
    {
        #region Método de avaliacao de pontos retornar todos os pontos correspondentes
        [HttpPost]
        public JsonResult AvaliacaoPontos(Avaliacao avaliacao)
        {
            //int pontosAvaliacao = Convert.ToInt16(TopicosSelc);

            AvaliacaoBLL avaliacaobussiness = new AvaliacaoBLL();
            avaliacao = avaliacaobussiness.inserirPontos(avaliacao);

            return Json(avaliacao, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
        #region Método de avaliacao de pontos retornar todos os pontos correspondentes LikeDeslike
        [HttpPost]
        public JsonResult AvaliacaoPontosDeslike(Avaliacao avaliacao)
        {
            //int pontosAvaliacao = Convert.ToInt16(TopicosSelc);

            AvaliacaoBLL avaliacaobussiness = new AvaliacaoBLL();
            avaliacao = avaliacaobussiness.inserirPontosLikeDeslike(avaliacao);

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
        public JsonResult consultarAvaliacao(Avaliacao avaliacao)
        {
            AvaliacaoBLL consultarbll = new AvaliacaoBLL();
            avaliacao = consultarbll.consultaAvaliacao(avaliacao,avaliacao.idUsuario);
            return Json(avaliacao, JsonRequestBehavior.AllowGet);
        }


    }
}