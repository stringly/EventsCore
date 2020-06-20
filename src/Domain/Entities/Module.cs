using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.Module;
using System;
using System.Collections.Generic;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Class that represents a module of an event
    /// </summary>
    public class Module : BaseEntity
    {
        private Module() { }
        /// <summary>
        /// Creates a new instance of the <see cref="Module"></see> class.
        /// </summary>
        /// <param name="moduleName">A string containing the name of the module.</param>
        /// <param name="moduleDescription">A string containing the name of the description.</param>
        /// <exception cref="ModuleArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The provided moduleName parameter is empty/whitespace.</description></item>
        /// <item><description>The provided moduleDescription parameter is empty/whitespace.</description></item>
        /// </list>
        /// </exception>
        public Module(string moduleName, string moduleDescription)
        {
            _moduleName = !string.IsNullOrWhiteSpace(moduleName) ? moduleName : throw new ModuleArgumentException("Cannot create Event Module: parameter cannot be null/whitespace string.", nameof(moduleName));
            _description = !string.IsNullOrWhiteSpace(moduleDescription) ? moduleDescription : throw new ModuleArgumentException("Cannot create Event Module: parameter cannot be null/whitespace string.", nameof(moduleName));
            _attendance = new List<Attendance>();
        }
        /// <summary>
        /// The Id of the <see cref="Event"></see>
        /// </summary>
        public int EventId { get; set; }
        private readonly string _moduleName;
        /// <summary>
        /// The name of the module
        /// </summary>
        public string ModuleName => _moduleName;
        private readonly string _description;
        /// <summary>
        /// The description of the module
        /// </summary>
        public string Description => _description;
        private List<Attendance> _attendance;
        /// <summary>
        /// A list of <see cref="Attendance"></see> records associated with the Module.
        /// </summary>
        public IEnumerable<Attendance> Attendance => _attendance.AsReadOnly();
    }
}
