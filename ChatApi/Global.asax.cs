using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;

namespace ChatApi
{
    public class WebApiApplication : HttpApplication
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    //EnableJSONP = true,
                    EnableDetailedErrors = true,
                    EnableJavaScriptProxies = false,

                };
                map.RunSignalR(hubConfiguration);
            });
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);

        }
    }
}
