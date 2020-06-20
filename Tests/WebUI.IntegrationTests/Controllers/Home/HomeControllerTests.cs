using EventsCore.WebUI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.WebUI.IntegrationTests.Controllers.Home
{
    public class HomeControllerTests : ControllerTestBase
    {        
        public HomeControllerTests() : base()
        {            
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/About")]
        [InlineData("/Home/Index")]
        public async Task Get_Endpoints_Returns_Success_And_Correct_ContentType(string url)
        {
            // Arrange/Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
