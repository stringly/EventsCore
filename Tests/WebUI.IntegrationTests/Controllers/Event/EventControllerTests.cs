using AngleSharp.Html.Dom;
using EventsCore.WebUI.IntegrationTests.Common;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        public async Task Post_Create_Returns_Redirect_To_Root()
        {
            // Arrange
            var initResponse = await _client.GetAsync("/Event/Create");
            var antiForgeryValues = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Event/Create");

            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, antiForgeryValues.cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                [AntiForgeryTokenExtractor.AntiForgeryFieldName] = antiForgeryValues.fieldValue,
                ["Title"] = "Test Created Event",
                ["Description"] = "This is a test event description",
                ["StartDate"] = "07/01/3000 1:07 PM",
                ["EndDate"] = "07/02/3000 11:07 PM",
                ["EventTypeId"] = "1",
                ["EventSeriesId"] = null,
                ["FundCenter"] = null,
                ["RegistrationOpenDate"] = "06/21/3000 1:07 PM",
                ["RegistrationClosedDate"] = "06/30/3000 1:07 PM",
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
            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);


            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, initResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Event", response.Headers.Location.OriginalString);
        }
        [Fact]
        public async Task Post_Create_Invalid_Returns_Edit()
        {
            var initResponse = await _client.GetAsync("/Event/Create");
            var antiForgeryValues = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Event/Create");

            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, antiForgeryValues.cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                [AntiForgeryTokenExtractor.AntiForgeryFieldName] = antiForgeryValues.fieldValue,
                ["Title"] = "", // empty string title should cause invalid
                ["Description"] = "This is a test event description",
                ["StartDate"] = "07/01/3000 1:07 PM",
                ["EndDate"] = "07/02/3000 11:07 PM",
                ["EventTypeId"] = "1",
                ["EventSeriesId"] = null,
                ["FundCenter"] = null,
                ["RegistrationOpenDate"] = "06/21/3000 1:07 PM",
                ["RegistrationClosedDate"] = "06/30/3000 1:07 PM",
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

            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);


            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, initResponse.StatusCode);
            Assert.Contains("Create Event: Error", responseString);
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
        public async Task Post_Edit_Returns_Redirect_To_Root()
        {
            var initResponse = await _client.GetAsync("/Event/Edit/1");
            var antiForgeryValues = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Event/Edit/1");

            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, antiForgeryValues.cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                [AntiForgeryTokenExtractor.AntiForgeryFieldName] = antiForgeryValues.fieldValue,
                ["Id"] = "1",
                ["Title"] = "Test Event 1",
                ["Description"] = "This is a test event description",
                ["StartDate"] = "07/01/3000 1:07 PM",
                ["EndDate"] = "07/02/3000 11:07 PM",
                ["EventTypeId"] = "1",
                ["EventSeriesId"] = null,
                ["FundCenter"] = null,
                ["RegistrationOpenDate"] = "06/21/3000 1:07 PM",
                ["RegistrationClosedDate"] = "06/30/3000 1:07 PM",
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

            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);
            

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, initResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Event", response.Headers.Location.OriginalString);
        }
        [Fact]
        public async Task Post_Edit_Invalid_Returns_Edit()
        {
            var initResponse = await _client.GetAsync("/Event/Edit/1");
            var antiForgeryValues = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Event/Edit/1");

            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, antiForgeryValues.cookieValue).ToString());

            var formModel = new Dictionary<string, string>
            {
                [AntiForgeryTokenExtractor.AntiForgeryFieldName] = antiForgeryValues.fieldValue,
                ["Id"] = "1",
                ["Title"] = "", // empty string title should cause invalid
                ["Description"] = "This is a test event description",
                ["StartDate"] = "07/01/3000 1:07 PM",
                ["EndDate"] = "07/02/3000 11:07 PM",
                ["EventTypeId"] = "1",
                ["EventSeriesId"] = null,
                ["FundCenter"] = null,
                ["RegistrationOpenDate"] = "06/21/3000 1:07 PM",
                ["RegistrationClosedDate"] = "06/30/3000 1:07 PM",
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

            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);


            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, initResponse.StatusCode);
            Assert.Contains("Edit Event: Error", responseString);
        }
        [Fact]
        public async Task Get_Delete_Returns_Delete()
        {
            // Arrange/Act
            var response = await _client.GetAsync("/Event/Delete/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            // Assert
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains("Test Event 1", stringResponse);
        }
        [Fact]
        public async Task Post_DeleteConfirmed_Returns_Redirect_To_Root()
        {
            // Arrange
            var initResponse = await _client.GetAsync("/Event/Delete/1");
            var antiForgeryValues = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Event/DeleteConfirmed/1");

            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, antiForgeryValues.cookieValue).ToString());
            var formModel = new Dictionary<string, string>
            {
                [AntiForgeryTokenExtractor.AntiForgeryFieldName] = antiForgeryValues.fieldValue,
                ["Id"] = "1"
            };

            //Act
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, initResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Event", response.Headers.Location.OriginalString);
        }
    }
}
