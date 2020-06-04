using System;

namespace EventsCore.Domain.Exceptions.Module
{
    /// <summary>
    /// Implementation of <see cref="InvalidOperationException"></see> used in the <see cref="Module"></see> entity
    /// </summary>
    public class ModuleInvalidOperationException : InvalidOperationException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        public ModuleInvalidOperationException(string message) : base(message: message) { }    
    }
}
