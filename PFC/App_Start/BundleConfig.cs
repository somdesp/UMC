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
                "~/Content/js/scripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js"));

        }
    }
}