
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using PFC.Business;
using PFC.Model;


namespace PFC.Controllers
{
    public class HomeController : Controller
    {
        private static string CaminhoImage;
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult Chat2()
        {
            return View();
        }

        public ActionResult CreateUsuario()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }

        [Authorize(Roles = "Admin,Master")]
        public ActionResult Denuncia()
        {
            return View();
        }
        /////////////////////////////////////TRATAMENTO ERROS////////////////////////////////
        public ActionResult Error()
        {
            return View();
        }

      

        [AcceptVerbs(HttpVerbs.Post)]
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


    }
}