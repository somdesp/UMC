using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PFC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/bundles/css")
                .Include(
                    "~/Content/css/plugin.css",
                    "~/Content/css/style.css"

                ));



            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include(
       
                "~/Content/js/jquery.min.js",
                "~/Content/js/plugin.js",
                "~/Content/js/scripts.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/ui-bootstrap-tpls-2.5.0.min.js",
               "~/Scripts/angular-flash.js",
                "~/Scripts/angular-signalr-hub.js",
                "~/Scripts/toaster.js",
                         "~/Scripts/jquery.signalR-2.2.0.min.js",
                "~/Scripts/Chat/chat.controller.js",
                "~/App/Module.js",
                "~/App/Service.js",
                "~/App/Services/hank.service.js",
                "~/App/Services/avalicacao.service.js",
                "~/App/Services/notificacao.service.js",
                "~/App/Services/solicitacao.service.js",
                "~/App/Services/login.service.js",
                "~/App/Services/topico.service.js",
                "~/Scripts/jk-rating-stars.js",
                "~/App/Controller/avaliacao.controller.js",
                "~/App/Controller/FileUploadController.js",
               "~/App/Controller/usuario.controller.js",
               "~/App/Controller/topico.controller.js",
               "~/App/Controller/topicoSelecionado.controller.js",
               "~/App/Controller/usuarioSelecionado.controller.js",
               "~/App/Controller/tema.controller.js",
               "~/App/Controller/login.controller.js",
               "~/App/Controller/chat.controller.js",
               "~/App/Controller/hank.controller.js",
                "~/App/Controller/solicitacao.controller.js",
                "~/App/Controller/notificacao.controller.js",


               "~/App/Factories.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js"
            ));

        }
    }
}