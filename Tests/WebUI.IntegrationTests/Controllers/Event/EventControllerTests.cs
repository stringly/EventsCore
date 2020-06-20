using AngleSharp.Html.Dom;
using EventsCore.WebUI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Net;
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
        public async Task Get_Index_When_Called_Returns_Index()
        {
            // Arrange/Act
            var response = await _client.GetAsync("/Event/Index");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
        [Fact]
        public async Task Get_Create_When_Called_Returns_Create()
        {
            // Arrange/Act
            var response = await _client.GetAsync("/Event/Create");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
        [Fact]
        public async Task Get_Detail_When_Called_Returns_Detail()
        {
            // Arrange/Act
            var response = await _client.GetAsync("/Event/Detail/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            // Assert

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("Test Event 1", stringResponse);
        }
        [Fact]
        public async Task Get_Edit_When_Called_Returns_Edit()
        {
            // Arrange/Act
            var response = await _client.GetAsync("/Event/Edit/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            // Assert

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("Test Event 1", stringResponse);
        }
        [Fact]
        public async Task Post_Edit_Returns_RedirectToRoot()
        {
            // Arrange
            var defaultPage = await _client.GetAsync("/Event/Edit/1");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            Dictionary<string,string> formValues = new Dictionary<string, string>
            {
                ["Id"] = "1",
                ["Title"] = "Test Event 1",
                ["Description"] = "This is a test event description",
                ["StartDate"] = "07/01/2020 1:07 PM",
                ["EndDate"] = "07/02/2020 11:07 PM",
                ["EventTypeId"] = "1",
                ["EventSeriesId"] = null,
                ["FundCenter"] = null,
                ["RegistrationOpenDate"] = "06/21/2020 1:07 PM",
                ["RegistrationClosedDate"] = "06/30/2020 1:07 PM",
                ["MinRegistrationCount"] = "1",
                ["MaxRegistrationCount"] = "10",
                ["MaxStandbyRegistrationCount"] = null,
                ["AddressLine1"] = "123 Anywhere St.",
                ["AddressLine2"] = null,
                ["City"] = "Yourtown",
                ["State"] = "MD",
                ["Zip"] = "12345",
                ["AllowStandby"] = "false"
            };

            //Act
            var response = await _client.SendAsync(
                (IHtmlFormElement)content.QuerySelector("form[id='editEventForm']"),
                (IHtmlButtonElement)content.QuerySelector("button[id='editEventSubmitButton']"),
                formValues);

            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Event", response.Headers.Location.OriginalString);
        }
    }
}
