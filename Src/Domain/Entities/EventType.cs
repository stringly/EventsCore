using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventType;

namespace EventsCore.Domain.Entities
{
    public class EventType : IEntity, IAggregateRoot
    {
        private EventType() { }
        public EventType(string typeName)
        {
            UpdateName(typeName);
        }
        public int Id { get; private set;}
        private string _name;
        public string Name => _name;
        public void UpdateName(string newName)
        {
            _name = !string.IsNullOrWhiteSpace(newName) ? newName : throw new EventTypeArgumentException("Cannot update Event Type name: parameter cannot be null/empty string", nameof(newName));
        }
    }
}
