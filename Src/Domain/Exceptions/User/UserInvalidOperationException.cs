using System;


namespace EventsCore.Domain.Exceptions.User
{
    public class UserInvalidOperationException: InvalidOperationException
    {
        public UserInvalidOperationException(string message) : base(message: message) { }
    }
}
