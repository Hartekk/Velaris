using Autofac.Integration.WebApi;
using VelarisBackend.Infrastructure;
using VelarisBackend.Repositories;
using VelarisBackend.Services;

namespace Autofac
{

    public static class Config
    {
        public static void Configure(System.Web.Http.HttpConfiguration config)
        {
            // Register your Web API controllers.
            var builder = new ContainerBuilder();

            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(System.Reflection.Assembly.GetExecutingAssembly());

            // Register Repositories
            builder.RegisterType <TestService>().As<ITestService>().InstancePerRequest();
            builder.RegisterType<DatabaseContext>().InstancePerRequest();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();

            // Build the container.
            var container = builder.Build();

            // Set the dependency resolver for Web API.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
