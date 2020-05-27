using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventModulesAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsCore.Domain.Entities.EventModulesAggregate
{
    public class Module : IEntity
    {
        private Module() { }
        public Module(string moduleName, string moduleDescription)
        {
            _moduleName = !string.IsNullOrWhiteSpace(moduleName) ? moduleName : throw new EventModulesAggregateArgumentException("Cannot create Event Module: parameter cannot be null/whitespace string.", nameof(moduleName));
            _description = !string.IsNullOrWhiteSpace(moduleDescription) ? moduleDescription : throw new EventModulesAggregateArgumentException("Cannot create Event Module: parameter cannot be null/whitespace string.", nameof(moduleName));
        }
        public int Id { get; private set; }
        private string _moduleName;
        public string ModuleName => _moduleName;
        private string _description;
        public string Description => _description;
    }
}
