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
        [Fact]
        public async Task Get_GetUpcomingEvents_Returns_Success_And_Events()
        {
            // Arrange/Act
            var response = await _client.GetAsync("/Home/GetUpcomingEvents");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            // Assert

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("Upcoming Events", stringResponse);
        }
        // TODO: Test ViewComponent methods: GetUpcomingEvents, GetMyRegistrations, GetMyOwnedEvents
    }
}
