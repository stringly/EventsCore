using System;


namespace EventsCore.Domain.Exceptions.User
{
    public class UserArgumentException : ArgumentException
    {
        public UserArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
