using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventModulesAggregate;
using System.Collections.Generic;
using System.Linq;

namespace EventsCore.Domain.Entities.EventModulesAggregate
{
    public class EventModules : IAggregateRoot
    {
        public int EventId { get; private set;}
        private readonly List<Module> _modules;
        public IEnumerable<Module> Modules => _modules.AsReadOnly();

        protected EventModules()
        {
            _modules = new List<Module>();
        }
        public EventModules(int eventId) : this()
        {
            EventId = eventId != 0 ? eventId : throw new EventModulesAggregateArgumentException("Event Id cannot be 0.", nameof(eventId));
        }
        public void AddModule(string moduleName, string moduleDescription)
        {
            if (_modules.Exists(x => x.ModuleName == moduleName))
            {
                throw new EventModulesAggregateArgumentException($"Cannot add Module to Event: module with Name {moduleName} already exists.", nameof(moduleName));
            }
            var newModule = new Module(moduleName, moduleDescription);
            _modules.Add(newModule);
        }
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
