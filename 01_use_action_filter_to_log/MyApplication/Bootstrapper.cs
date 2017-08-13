using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;

namespace MyApplication
{
    public class Bootstrapper
    {
        public Action<ContainerBuilder> OnContainerBuild = builder => { };
        public IContainer Init(HttpConfiguration config)
        {
            RegisterRoutes(config);
            config.Filters.Add(new LogActionFilter());

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterType<MyService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<MyLogger>().As<IMyLogger>().InstancePerLifetimeScope();
            OnContainerBuild(containerBuilder);

            IContainer container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }

        public void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "get message",
                "message",
                new
                {
                    controller = "message",
                    action = "GetMessageInfo"
                },
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});

            config.Routes.MapHttpRoute(
                "get id",
                "id",
                new
                {
                    controller = "message",
                    action = "GetId"
                },
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});
        }
    }
}