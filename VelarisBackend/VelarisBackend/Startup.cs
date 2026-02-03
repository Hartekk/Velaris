using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;
using VelarisBackend.Services;
using VelarisBackend.Repositories;
using VelarisBackend.Infrastructure;
using Autofac.Integration.Owin; // Add this using directive for Autofac OWIN integration

[assembly: OwinStartup(typeof(VelarisBackend.Startup))]

namespace VelarisBackend
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var builder = new ContainerBuilder();

            // Register Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register services
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<DatabaseContext>().InstancePerRequest();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.MapHttpAttributeRoutes();

            // Fix: Use the correct extension method for Autofac OWIN integration
            app.UseAutofacLifetimeScopeInjector(container);

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}
