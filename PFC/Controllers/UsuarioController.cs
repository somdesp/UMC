using Microsoft.AspNet.Identity;
using PFC.DAO;
using PFC.Model;
using System;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using PFC.Business.Business;


namespace PFC.Controllers
{

    [Authorize]
    public class UsuarioController : Controller
    {
        private static string CaminhoImage;

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Usuario,Master")]
        public ActionResult EditarUsuario()
        {
           ViewBag.IdUsuario = User.Identity.GetUserId<int>();
            return View();
        }




        #region Metodo listar Usuario READ   
        //Get Usuario/GetUsuario
        public JsonResult ListarUsuarios()
        {
            UsuarioDAO db = new UsuarioDAO();
            return Json(db.ListarUsuarios(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Metodo Adicionar Usuario Create   
        //Usuario/AdicionarUsuario
        [AllowAnonymous]
        [HttpPost]
        public JsonResult AdicionarUsuario(Usuario usuario)
        {
            UsuarioBLL usuarioBll = new UsuarioBLL();
            usuario.UploadArquivo.Caminho = CaminhoImage;
            if (usuarioBll.AdicionarUsuario(usuario) == true)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion

        #region Metodo Editar Usuario Update   
        //Usuario/Editar
        [Authorize(Roles = "Admin,Usuario,Master")]
        [HttpPost]
        public ActionResult AtualizarUsuario(Usuario usuario)
        {
            UsuarioBLL usuarioBll = new UsuarioBLL();
            return Json(usuarioBll.AtualizarUsuario(usuario), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Metodo Que salva a imagem no caminho /Upload/ e retorna para o JS o caminho salvo

        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        public JsonResult UploadImage()
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
        #endregion

        #region Metodo Controller de  Inativar Usuario
        [HttpPost]
        public ActionResult InativarUsuario(Usuario usuario)
        {
            UsuarioBLL bll = new UsuarioBLL();
            return Json(bll.InativarUsuario(usuario)); 
        }
        #endregion

        [AllowAnonymous]
        public ActionResult ConsultaUnico(Usuario cad)
        {

            Usuario retornoCad = new Usuario();
            UsuarioDAO dao = new UsuarioDAO();
            retornoCad = dao.ConsultaUsuario(cad);
            try
            {

                bool retorno = true;
                if ((retornoCad.Email != null))
                {
                    retorno = false;
                }
                return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //Método chamado para carregar Usuario  no Post vem todas as informações
        [HttpPost]
        public ActionResult ConsultarUsuario()
        {
            UsuarioBLL usuarioBll = new UsuarioBLL();
            Usuario usuario = new Usuario();
            usuario.Id = User.Identity.GetUserId<int>();
            usuario = usuarioBll.ConsultaUsuarioInt(usuario);
            return Json(usuario);
        }





        public JsonResult ConsultaData(Usuario cad)
        {
            int year = cad.DataNasci.Date.Year;
            int yearNow = DateTime.Now.Year;

            int result = yearNow - year;
            if (result < 16)
            {
               return Json(false,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet); ;
            }


        }


       


    }
}