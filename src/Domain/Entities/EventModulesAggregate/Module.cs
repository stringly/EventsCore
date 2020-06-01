using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventModulesAggregate;

namespace EventsCore.Domain.Entities.EventModulesAggregate
{
    /// <summary>
    /// Class that represents a module of an event
    /// </summary>
    public class Module : IEntity
    {
        private Module() { }
        /// <summary>
        /// Creates a new instance of the <see cref="Module"></see> class.
        /// </summary>
        /// <param name="moduleName">A string containing the name of the module.</param>
        /// <param name="moduleDescription">A string containing the name of the description.</param>
        /// <exception cref="EventModulesAggregateArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The provided moduleName parameter is empty/whitespace.</description></item>
        /// <item><description>The provided moduleDescription parameter is empty/whitespace.</description></item>
        /// </list>
        /// </exception>
        public Module(string moduleName, string moduleDescription)
        {
            _moduleName = !string.IsNullOrWhiteSpace(moduleName) ? moduleName : throw new EventModulesAggregateArgumentException("Cannot create Event Module: parameter cannot be null/whitespace string.", nameof(moduleName));
            _description = !string.IsNullOrWhiteSpace(moduleDescription) ? moduleDescription : throw new EventModulesAggregateArgumentException("Cannot create Event Module: parameter cannot be null/whitespace string.", nameof(moduleName));
        }
        /// <summary>
        /// The Id of this instance
        /// </summary>
        public int Id { get; private set; }
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
    }
}
