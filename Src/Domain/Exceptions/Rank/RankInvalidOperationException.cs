using System;


namespace EventsCore.Domain.Exceptions.Rank
{
    public class RankInvalidOperationException : InvalidOperationException
    {
        public RankInvalidOperationException(string message) : base(message: message) { }
    }
}
