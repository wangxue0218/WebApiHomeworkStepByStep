using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using MyApplication;

namespace Application.Test
{
    public class ResourceTestBase : IDisposable
    {
        readonly HttpServer httpServer;
        static readonly Uri WebApiUri = new Uri("http://www.baidu.com");
        protected HttpClient Client { get; }
        public IContainer Scope { get; set; }

        public ResourceTestBase()
        {
            httpServer = CreateHttpServer();
            Client = CreateHttpClient(httpServer);
        }

        static HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return new HttpClient(handler)
            {
                BaseAddress = WebApiUri
            };
        }

        HttpServer CreateHttpServer()
        {
            HttpConfiguration config = CreateContainer();
            return new HttpServer(config);
        }

        HttpConfiguration CreateContainer()
        {
            var config = new HttpConfiguration();
            var bootstrapper = new Bootstrapper() {OnContainerBuild = MockDepedency()};
            Scope = bootstrapper.Init(config);
            return config;
        }

        public virtual Action<ContainerBuilder> MockDepedency()
        {
            return builder => { };
        }

        public void Dispose()
        {
            Scope?.Dispose();
            httpServer?.Dispose();
            Client?.Dispose();
        }
    }
}
