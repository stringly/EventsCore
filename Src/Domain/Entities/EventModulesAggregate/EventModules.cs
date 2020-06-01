using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventModulesAggregate;
using System.Collections.Generic;
using System.Linq;

namespace EventsCore.Domain.Entities.EventModulesAggregate
{
    /// <summary>
    /// Aggregate root Entity that controls adding/removing/updating an <see cref="Event"></see>'s <see cref="EventModules"></see>
    /// </summary>
    public class EventModules : IAggregateRoot
    {
        /// <summary>
        /// The Id of the <see cref="Event"></see> associated with this aggregate.
        /// </summary>
        public int EventId { get; private set;}
        private readonly List<Module> _modules;
        /// <summary>
        /// A readonly list of <see cref="Module"></see>s associated with the <see cref="Event"></see>
        /// </summary>
        public IEnumerable<Module> Modules => _modules.AsReadOnly();
        /// <summary>
        /// EF Constructor
        /// </summary>
        private EventModules()
        {
            _modules = new List<Module>();
        }
        /// <summary>
        /// Creates a new Instance of <see cref="EventModules"></see>
        /// </summary>
        /// <param name="eventId">The Id of the <see cref="Event"></see> associated with this aggregate.</param>
        /// <exception cref="EventModulesAggregateArgumentException">Thrown when the eventId parameter is 0 or out of range.</exception>
        public EventModules(int eventId) : this()
        {
            EventId = eventId != 0 ? eventId : throw new EventModulesAggregateArgumentException("Event Id cannot be 0.", nameof(eventId));
        }
        /// <summary>
        /// Creates a new <see cref="Module"></see> and adds it to the aggregate's <see cref="Modules"></see> collection.
        /// </summary>
        /// <param name="moduleName">A string containing the name of the module to be added.</param>
        /// <param name="moduleDescription">A string containing the description of the module to be added.</param>
        /// <exception cref="EventModulesAggregateArgumentException">Thrown when a Module already exists with the given moduleName parameter.</exception>
        public void AddModule(string moduleName, string moduleDescription)
        {
            if (_modules.Exists(x => x.ModuleName == moduleName))
            {
                throw new EventModulesAggregateArgumentException($"Cannot add Module to Event: module with Name {moduleName} already exists.", nameof(moduleName));
            }
            var newModule = new Module(moduleName, moduleDescription);
            _modules.Add(newModule);
        }
        /// <summary>
        /// Removes a module by Id
        /// </summary>
        /// <param name="moduleId">The Id of the module to be removed.</param>
        /// <exception cref="EventModulesAggregateArgumentException">Thrown when no module with the given moduleId parameter exists in the aggregate's <see cref="Modules"></see> collection.</exception>
        public void RemoveModuleById(int moduleId)
        {
            var moduleWithGivenId = _modules.FirstOrDefault(x => x.Id == moduleId);
            if(moduleWithGivenId == null)
            {
                throw new EventModulesAggregateArgumentException($"Cannot remove Module from Event: no module with id {moduleId} exists.", nameof(moduleId));
            }
            _modules.Remove(moduleWithGivenId);
        }
    }
}
