using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PFC.Business;
using PFC.Model;

namespace PFC.Controllers
{
    [Authorize]
    public class TopicoController : Controller
    {
        public static Topico topicoSalva = new Topico();
        // GET: Topico
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Index(Topico Topico)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            topicoSalva = await topicoBll.DetalheTopico(Topico);
            return View(await topicoBll.DetalheTopico(Topico));
        }

        public ActionResult Topico()
        {
            return View();
        }

        [Authorize]
        public ActionResult Ranking()
        {
            return View();
        }

        [Authorize(Roles = "Master,Master")]
        // GET: Temas/Create
        public ActionResult Gerenciamento()
        {
            return View();
        }

        [Authorize(Roles = "Master,Usuario,Master")]
        // GET: Temas/Create
        public ActionResult Create()
        {

            return View();
        }

        #region Resposta Topico
        [HttpPost]
        [Authorize(Roles = "Master,Usuario,Master")]
        public ActionResult RespostaTopico(Topico topico)
        {

            return View();
        }
        #endregion

        #region Criação de topicos
        [HttpPost]
        [Authorize(Roles = "Master,Usuario,Master")]
        public async Task<ActionResult> AdicionarTopico(Topico topico)
        {
            Boolean retorno = false;
            TopicoBLL topicoBll = new TopicoBLL();

            topico.usuario.Id = User.Identity.GetUserId<int>();
            if (ModelState.IsValid)
            {

                 await topicoBll.AdicionarTopico(topico);
                retorno = true;
            }
            return Json(retorno);
        }
        #endregion

        #region Topico escolhido

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> TopicoSelecionadoJson(Topico topico)
        {
            topico.avaliacao.idUsuario = User.Identity.GetUserId<int>();
            TopicoBLL topicoBll = new TopicoBLL();
            topico = await topicoBll.DetalheTopico(topico);
            topicoSalva = topico;
            return Json(topico, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Listar Todos os Topicos
        //Listar Topicos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ListarTopico()
        {
            TopicoBLL topicoBll = new TopicoBLL();           
            return Json(await topicoBll.ListarTopico(), JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Listar topico conforme pesquisa
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ListarTopicoPesquisa(string pesquisa)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            List<Topico> topico = new List<Topico>();

            topico = await topicoBll.ListarTopico(pesquisa);
            return Json(topico, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Adicionar Posts (Repostas Topico)
        [HttpPost]
        [Authorize(Roles = "Master,Usuario,Master")]
        public ActionResult AdicionarResposta(Topico post)
        {
            TopicoBLL topicoBll = new TopicoBLL();

            post.Id = topicoSalva.Id;
            post.Titulo = topicoSalva.Titulo;
            post.Tema = topicoSalva.Tema;

            post.TopicoFilho.Tema = topicoSalva.Tema;
            post.usuario.Id = topicoSalva.usuario.Id;

            post.TopicoFilho.DataCria = DateTime.Now;

            post.TopicoFilho.usuario.Id = User.Identity.GetUserId<int>();

            bool retorno = topicoBll.AdicionarPosts(post);

            return Json(retorno);
        }
        #endregion

        #region Adicionar Anexos
        [HttpPost]
        public ActionResult AnexoArquivos(Anexos anexos)
        {
            bool retorno = false;
            ArquivoBLL arquivoBll = new ArquivoBLL();
            // Extrai apenas o nome do arquivo

            anexos.Caminho = Path.GetFileName(anexos.ArquivoBase.FileName);
            // Armazena o arquivo dentro da pasta ~/Arquivo
            var path = Path.Combine(Server.MapPath("~/Upload"), anexos.Caminho);
            anexos.ArquivoBase.SaveAs(path);

            if (arquivoBll.AnexoArquivos(anexos) == true)
            {
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Arquivo nao salvo", JsonRequestBehavior.AllowGet);

            }

        }



        #endregion


        [AllowAnonymous]
        [HttpPost]
        public JsonResult verificarIdUsuario()
        {
            int idusuario = User.Identity.GetUserId<int>();
            return Json(idusuario, JsonRequestBehavior.AllowGet);
        }



        public int consultaridUsuarioTopico()
        {
            int idusuario = User.Identity.GetUserId<int>();
            return idusuario;
        }


        #region Topico Escolhido
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> TopicoSelecionado(string topicoId)
        {
            ViewBag.TopicoId = topicoId;
            ViewBag.Usuario = User.Identity.GetUserId<int>();
            Topico topico = new Topico();
            TopicoBLL topicoBll = new TopicoBLL();
            topico.Id = Convert.ToInt16(topicoId);

            if (await topicoBll.ValTopico(topico) == true)
            {
                topico.Id = Convert.ToInt16(topicoId);
               // ViewBag.TopicoId = topicoId;
                return View(topico);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        [HttpPost]
        public JsonResult PesquisarTopico(string pesquisar)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

        #region FeCharTopico

        [HttpPost]
        public JsonResult FecharTopico(Topico topico)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            if (topico.usuario.Id == User.Identity.GetUserId<int>())
            {
                if (topicoBll.FechaTopico(topico) == true)
                {
                    //  topicoBll.EnviarEmail(topico);
                    return Json(true, JsonRequestBehavior.AllowGet);
                };
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region FeCharTopico

        [HttpPost]
        public JsonResult FechaTopico(Topico topico)
        {
            TopicoBLL topicoBll = new TopicoBLL();
            if (topico.usuario.Id == User.Identity.GetUserId<int>())
            {
                if (topicoBll.FechaTopico(topico) == true)
                {
                    //  topicoBll.EnviarEmail(topico);
                    return Json(true, JsonRequestBehavior.AllowGet);
                };
            }
            return Json(false, JsonRequestBehavior.AllowGet);

        }

        #endregion


        #region Upload Arquivos


        #endregion


    }


}