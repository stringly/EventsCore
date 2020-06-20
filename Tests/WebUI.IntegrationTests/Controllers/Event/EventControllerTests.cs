using EventsCore.WebUI.IntegrationTests.Common;
using System.Threading.Tasks;
using Xunit;

namespace EventsCore.WebUI.IntegrationTests.Controllers.Event
{
    public class EventControllerTests : ControllerTestBase
    {        
        public EventControllerTests() : base()
        {
        }
        [Fact]
        public async Task Index_When_Called_Returns_Index()
        {
            // Arrange/Act
            var response = await _client.GetAsync("/Event/Index");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
