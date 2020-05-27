using EventsCore.Domain.Common;
using EventsCore.Domain.Exceptions.EventSeries;

namespace EventsCore.Domain.Entities
{
    public class EventSeries: IEntity, IAggregateRoot
    {
        private EventSeries() { }
        public EventSeries(string title, string description)
        {
            UpdateTitle(title);
            UpdateDescription(description);
        }
        public int Id { get; private set;}
        private string _title;
        public string Title => _title;
        private string _description;
        public string Description => _description;

        public void UpdateTitle(string newTitle)
        {
            _title = !string.IsNullOrWhiteSpace(newTitle) ? newTitle : throw new EventSeriesArgumentException("Cannot update Event Series title: parameter cannot be null/empty string.", nameof(newTitle));
        }
        public void UpdateDescription(string newDescription)
        {
            _description = !string.IsNullOrWhiteSpace(newDescription) ? newDescription : throw new EventSeriesArgumentException("Cannot update Event Series title: parameter cannot be null/empty string.", nameof(newDescription));
        }
    }
}
