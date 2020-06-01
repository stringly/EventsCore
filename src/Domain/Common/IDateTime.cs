using System;

namespace EventsCore.Domain.Common
{
    /// <summary>
    /// Interface that defines a DateTime provider
    /// </summary>
    public interface IDateTime    
    {
        /// <summary>
        /// Returns the current System Time
        /// </summary>
        DateTime Now { get; }
    }
}
