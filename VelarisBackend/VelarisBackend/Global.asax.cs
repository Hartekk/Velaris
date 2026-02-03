using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using VelarisBackend.App_Start;

namespace VelarisBackend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas(); 
            GlobalConfiguration.Configure(AutofacConfig.RegisterDependencies);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
           
        }
    }

}
