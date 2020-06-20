using EventsCore.Application.Common.Interfaces;

namespace EventsCore.Application.UnitTests.Common
{
    public class CurrentUserServiceTesting : ICurrentUserService
    {
        public string UserId => "user123";

        public bool IsAuthenticated => true;
    }
}
