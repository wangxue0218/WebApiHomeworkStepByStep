using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using MyApplication;
using Xunit;

namespace Application.Test
{
    public class ApplicationTest : ResourceTestBase
    {
        [Fact]
        public async Task should_return_ok_and_get_message()
        {
            var fakeLogger = Scope.Resolve<IMyLogger>() as FakeLogger;
            HttpResponseMessage response = await Client.GetAsync("message");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string payload = await response.Content.ReadAsStringAsync();
            Assert.Equal("I am dependency injection", payload);

            Assert.Equal(2, fakeLogger.storage.Count);
            Assert.Contains("Record start time for request.", fakeLogger.storage[0]);
            Assert.Contains("After request: The request execute", fakeLogger.storage[1]);
        }

        [Fact]
        public async Task should_return_ok_and_get_id()
        {
            var fakeLogger = Scope.Resolve<IMyLogger>() as FakeLogger;
            HttpResponseMessage response = await Client.GetAsync("id");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string payload = await response.Content.ReadAsStringAsync();
            Assert.Equal("123", payload);

            Assert.Equal(2, fakeLogger.storage.Count);
            Assert.Contains("Record start time for request.", fakeLogger.storage[0]);
            Assert.Contains("After request: The request execute", fakeLogger.storage[1]);
        }

        public override Action<ContainerBuilder> MockDepedency()
        {
            return builder =>
            {
                builder.RegisterInstance(new FakeLogger()).As<IMyLogger>();
            };
        }
    }
}
