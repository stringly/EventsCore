using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventType;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Entity class that represents an Event Type.
    /// </summary>
    public class EventType : BaseEntity, IAggregateRoot
    {
        private EventType() { }
        /// <summary>
        /// The name of the Event Type.
        /// </summary>
        /// <param name="typeName">A string containing the name of the Event Type.</param>
        public EventType(string typeName)
        {
            UpdateName(typeName);
        }
        private string _name;
        /// <summary>
        /// Returns the name of the Event Type.
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// Updates the name of the Event Type.
        /// </summary>
        /// <param name="newName">A string containing the new Event Type.</param>
        /// <exception cref="EventTypeArgumentException">Thrown when the newName parameter is empty/whitespace.</exception>
        public void UpdateName(string newName)
        {
            _name = !string.IsNullOrWhiteSpace(newName) ? newName : throw new EventTypeArgumentException("Cannot update Event Type name: parameter cannot be null/empty string", nameof(newName));
        }
    }
}
