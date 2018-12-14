using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PFC.App_Start
{
    public class FiltroConfig
    {
        public static void RegistroGlobalFiltros(GlobalFilterCollection filtro)
        {
            filtro.Add(new HandleErrorAttribute());
            filtro.Add(new AuthorizeAttribute());
        }
    }
}