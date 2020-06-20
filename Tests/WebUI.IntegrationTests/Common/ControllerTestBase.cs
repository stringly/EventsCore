using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace EventsCore.WebUI.IntegrationTests.Common
{
    public abstract class ControllerTestBase
    {
        protected readonly HttpClient _client;
        public ControllerTestBase()
        {
            var builder = new WebHostBuilder().UseStartup<TestStartup>();
            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }
    }
}
