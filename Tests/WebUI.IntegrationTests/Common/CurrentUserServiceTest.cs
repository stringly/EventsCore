using EventsCore.Application.Common.Interfaces;

namespace EventsCore.WebUI.IntegrationTests.Common
{
    public class CurrentUserServiceTest : ICurrentUserService
    {
        public string UserId => "user123";

        public bool IsAuthenticated => true;
    }
}
