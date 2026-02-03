using System;
using System.Web.Http;

namespace VelarisBackend.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies(HttpConfiguration config) // Change parameter type
        {
            // Web Api Configuration and Services
            Autofac.Config.Configure(config);

            // Web API Routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}