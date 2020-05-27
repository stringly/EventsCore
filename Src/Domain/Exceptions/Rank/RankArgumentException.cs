using System;


namespace EventsCore.Domain.Exceptions.Rank
{
    public class RankArgumentException : ArgumentException
    {
        public RankArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    }
}
