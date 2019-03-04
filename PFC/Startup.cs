using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Web.Http;
using Microsoft.Owin;
using Hangfire;


[assembly: OwinStartup(typeof(PFC.Startup))]

namespace PFC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            app.MapSignalR();
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Home/Error")
            });

           
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
           
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            ExecutarJobs(app);

        }

        public void ExecutarJobs(IAppBuilder app)
        {
            //Serviço para executar Jobs de forma automatica.
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("Data Source=mehelp.c5jiatqwcxsd.sa-east-1.rds.amazonaws.com;Initial Catalog=MeHelp;User Id=mehelp;Password=TCCUMC2018");
            
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            
        }

    }
}