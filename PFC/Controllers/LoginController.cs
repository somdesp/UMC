using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using PFC.Model;
using Microsoft.AspNet.Identity;
using PFC.Business;
using PFC.Business.Business;
using System.Threading.Tasks;

namespace PFC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            LoginBLL loginUsuario = new LoginBLL();
            UsuarioBLL usuariobll = new UsuarioBLL();
            model = await loginUsuario.LoginUsuario(model);

            if (model.success == true)
            {

                var identity = new ClaimsIdentity(new[]
                    {
                            new Claim(ClaimTypes.UserData, model.UploadArquivo.Caminho),
                            new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                            new Claim(ClaimTypes.Name, model.Nome),
                            new Claim(ClaimTypes.Email, model.Email),
                            new Claim(ClaimTypes.Role, model.Permissao.Permissao),

                        },
                    "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);
                
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(model, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            Session["Imagem"] = null;
            authManager.SignOut("ApplicationCookie");
            authManager.SignOut();
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            string retorno = "Deslogado Com Sucesso";
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}