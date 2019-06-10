using Microsoft.AspNet.Identity;
using PFC.DAO;
using PFC.Model;
using System;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using PFC.Business;
using PFC.Hubs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;

namespace PFC.Controllers
{

    public class UsuarioController : Controller
    {
        private static string CaminhoImage;

        [Authorize(Roles = "Master")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Master,Usuario,Moderador")]
        public ActionResult EditarUsuario()
        {
            ViewBag.IdUsuario = User.Identity.GetUserId<int>();
            return View();
        }

        #region Metodo listar Usuario READ   
        //Get Usuario/GetUsuario
        public async Task<JsonResult> ListarUsuarios()
        {
            try
            {
                UsuarioDAO db = new UsuarioDAO();
                return Json(await db.ListarUsuarios(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }
        #endregion

        #region Metodo Adicionar Usuario Create   
        //Usuario/AdicionarUsuario
        [AllowAnonymous]
        [HttpPost]
        public JsonResult AdicionarUsuario(Usuario usuario)
        {
            UsuarioBLL usuarioBll = new UsuarioBLL();
            try
            {
                usuario.UploadArquivo.Caminho = CaminhoImage;
                if (usuarioBll.AdicionarUsuario(usuario) == true)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }
        #endregion

        #region Metodo Editar Usuario Update   
        //Usuario/Editar
        [Authorize(Roles = "Master,Usuario,Moderador")]
        [HttpPost]
        public ActionResult AtualizarUsuario(Usuario usuario)
        {
            UsuarioBLL usuarioBll = new UsuarioBLL();
            return Json(usuarioBll.AtualizarUsuario(usuario), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [Authorize(Roles = "Master,Usuario,Moderador")]
        [HttpPost]
        public JsonResult AlterarSenha(Usuario usuario)
        {
            try
            {
                UsuarioBLL usuarioBll = new UsuarioBLL();
                usuario.Id = User.Identity.GetUserId<int>();
                return Json(usuarioBll.AtualizarSenha(usuario), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }


        #region Metodo Que salva a imagem no caminho /Upload/ e retorna para o JS o caminho salvo

        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        public JsonResult UploadImage()
        {
            try
            {
                string _imgname = string.Empty;
                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                    if (pic.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(pic.FileName);
                        var _ext = Path.GetExtension(pic.FileName);

                        _imgname = Guid.NewGuid().ToString();
                        var _comPath = Server.MapPath("/Upload/FOUMC_") + _imgname + _ext;
                        _imgname = "FOUMC_" + _imgname + _ext;

                        ViewBag.Msg = _comPath;
                        var path = _comPath;

                        // Saving Image in Original Mode
                        pic.SaveAs(path);
                        CaminhoImage = _imgname.ToString();

                        // resizing image
                        MemoryStream ms = new MemoryStream();
                        WebImage img = new WebImage(_comPath);

                        if (img.Width > 200)
                            img.Resize(200, 200);
                        img.Save(_comPath);
                        // end resize
                    }
                }
                return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }
        #endregion

        #region Metodo Controller de  Inativar Usuario
        [HttpPost]
        public ActionResult InativarUsuario(Usuario usuario)
        {
            UsuarioBLL bll = new UsuarioBLL();
            return Json(bll.InativarUsuario(usuario));
        }
        #endregion

        #region UsuarioEscolhido

        [Authorize(Roles = "Master,Usuario,Moderador")]
        public async Task<ActionResult> PerfilUsuario(string usuarioId)
        {
            ViewBag.UsuarioId = usuarioId;
            ViewBag.Usuario = User.Identity.GetUserId<int>();
            Usuario usuario = new Usuario();
            UsuarioBLL usuarioBll = new UsuarioBLL();

            try
            {
                usuario.Id = Convert.ToInt16(usuarioId);
                usuario = await usuarioBll.ConsultaUsuarioInt(usuario);
                if (usuario.Nome != null)
                {
                    usuario.Id = Convert.ToInt16(usuarioId);
                    ViewBag.TopicoId = usuarioId;
                    return View(usuario);
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        [HttpPost]
        public async Task<JsonResult> VisualizarPerfil(Usuario usuario)
        {
            UsuarioBLL usuarioBll = new UsuarioBLL();
            usuario = await usuarioBll.ConsultaUsuarioInt(usuario);
            return Json(usuario);
        }

        #endregion

        [AllowAnonymous]
        public async Task<ActionResult> ConsultaUnico(Usuario cad)
        {

            Usuario retornoCad = new Usuario();
            UsuarioDAO dao = new UsuarioDAO();
            retornoCad = await dao.ConsultaUsuario(cad);
            try
            {

                bool retorno = true;
                if ((retornoCad.Email != null))
                {
                    retorno = false;
                }
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }

        //Método chamado para carregar Usuario  no Post vem todas as informações
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ConsultarUsuario()
        {
            UsuarioBLL usuarioBll = new UsuarioBLL();
            Usuario usuario = new Usuario();
            try
            {
                usuario.Id = User.Identity.GetUserId<int>();
                usuario = await usuarioBll.ConsultaUsuarioInt(usuario);
                Usuario resposta;
                if (usuario.Id == 0 || usuario.Auth.Id == 5)
                {
                    try
                    {
                        var ctx = Request.GetOwinContext();
                        var authManager = ctx.Authentication;
                        Session["Imagem"] = null;
                        authManager.SignOut("ApplicationCookie");
                        authManager.SignOut();
                        authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        bool retorno = true;
                        return Json(retorno, JsonRequestBehavior.AllowGet);
                    }
                    catch (System.Exception)
                    {

                        throw;
                    }

                    resposta = null;
                }
                else
                {
                    resposta = usuario;
                }
                return Json(resposta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }





        public JsonResult ConsultaData(Usuario cad)
        {
            int year = cad.DataNasci.Date.Year;
            int yearNow = DateTime.Now.Year;

            int result = yearNow - year;
            if (result < 16)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet); ;
            }


        }

        [Authorize(Roles = "Master,Usuario,Moderador")]
        public ActionResult NotificacaoUsuario()
        {           
            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<JsonResult> PesquisarUsuario(string pesquisa)
        {
            try
            {
                UsuarioBLL usuariopesquisa = new UsuarioBLL();
                List<Usuario> objetoPesquisa = new List<Usuario>();
                objetoPesquisa = await usuariopesquisa.ConsultaUsuario();
                var result = objetoPesquisa.Where(p => p.Nome.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) != -1);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

        }




    }
}