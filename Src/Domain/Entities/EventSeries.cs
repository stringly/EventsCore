using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventSeries;

namespace EventsCore.Domain.Entities
{
    /// <summary>
    /// Entity class that represents a series of <see cref="Event"></see> objects.
    /// </summary>
    public class EventSeries: IEntity, IAggregateRoot
    {
        private EventSeries() { }
        /// <summary>
        /// Creates a new instance of the <see cref="EventSeries"></see> object.
        /// </summary>
        /// <param name="title">A string containing the name of the Event Series</param>
        /// <param name="description">A string containing the description of the Event Series</param>
        public EventSeries(string title, string description)
        {
            UpdateTitle(title);
            UpdateDescription(description);
        }
        /// <summary>
        /// The Id of the EventSeries instance.
        /// </summary>
        public int Id { get; private set;}
        private string _title;
        /// <summary>
        /// Returns the title of the Event Series.
        /// </summary>
        public string Title => _title;
        private string _description;
        /// <summary>
        /// Returns the description of the Event Series.
        /// </summary>
        public string Description => _description;
        /// <summary>
        /// Updates the title of the Event Series
        /// </summary>
        /// <param name="newTitle">A string containing the new Event Title.</param>
        /// <exception cref="EventSeriesArgumentException">Thrown when the newTitle parameter is empty/whitespace.</exception>
        public void UpdateTitle(string newTitle)
        {
            _title = !string.IsNullOrWhiteSpace(newTitle) ? newTitle : throw new EventSeriesArgumentException("Cannot update Event Series title: parameter cannot be null/empty string.", nameof(newTitle));
        }
        /// <summary>
        /// Updates the description of the Event Series
        /// </summary>
        /// <param name="newDescription">A string containing the new description of the Event.</param>
        /// <exception cref="EventSeriesArgumentException">Thrown when the newDescription parameter is empty/whitespace.</exception>
        public void UpdateDescription(string newDescription)
        {
            _description = !string.IsNullOrWhiteSpace(newDescription) ? newDescription : throw new EventSeriesArgumentException("Cannot update Event Series title: parameter cannot be null/empty string.", nameof(newDescription));
        }
    }
}
