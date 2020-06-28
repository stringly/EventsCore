using EventsCore.Application.Common.Interfaces;

namespace EventsCore.Application.UnitTests.Common
{
    public class CurrentUserServiceTesting : ICurrentUserService
    {
        public CurrentUserServiceTesting()
        {
            UserId = "user123";
            IsAuthenticated = true;
        }
        public CurrentUserServiceTesting(string LDAPName, bool isAuthenticated)
        {
            UserId = LDAPName;
            IsAuthenticated = isAuthenticated;
        }
        public string UserId {get; private set; }

        public bool IsAuthenticated {get; private set; }
    }
}
