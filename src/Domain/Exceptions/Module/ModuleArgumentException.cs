using System;

namespace EventsCore.Domain.Exceptions.Module
{
    /// <summary>
    /// Implementation of <see cref="ArgumentException"></see> used in the <see cref="Module"></see> entity
    /// </summary>
    public class ModuleArgumentException : ArgumentException
    {
        /// <summary>
        /// Creates a new instance of the exception.
        /// </summary>
        /// <param name="message">A string containing the message.</param>
        /// <param name="paramName">A string containing the name of the parameter that threw the exception.</param>
        public ModuleArgumentException(string message, string paramName) : base(message: message, paramName: paramName) { }
    
    }
}
