using System.Web.Http;
using Swashbuckle.Application;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(VelarisBackend.SwaggerConfig), "Register")]

namespace VelarisBackend
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Velaris API");
                })
                .EnableSwaggerUi();
        }
    }
}
