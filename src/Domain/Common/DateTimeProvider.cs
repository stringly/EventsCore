using System;

namespace EventsCore.Domain.Common
{
    /// <summary>
    /// An implementation of <see cref="IDateTime">IDateTime</see> that is used to inject the System time.
    /// </summary>
    public class DateTimeProvider : IDateTime
    {
        /// <summary>
        /// Returns the current Date
        /// </summary>
        public DateTime Now {
            get {
                return System.DateTime.Now;
            }
        }
    }
}
