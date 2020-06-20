using EventsCore.Application.Events.Queries.GetEventsList;
using EventsCore.Application.EventTypes.Queries.GetEventTypesList;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EventsCore.WebUI.Models
{
    public class EventIndexViewModel : IndexViewModel
    {
        public EventIndexViewModel(List<EventDto> events, List<EventTypeDto> eventTypes, string currentSort, string currentFilter, int totalItems, int page, int pageSize, int? selectedUserId = null, int? selectedEventTypeId = null)
        {
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = totalItems
            };
            Events = events;
            EventTypes = eventTypes.ConvertAll(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()});
            CurrentFilter = currentFilter;
            CurrentSort = currentSort;
            // TODO: Implement User select List
            Users = new List<SelectListItem>();
            SelectedEventTypeId = selectedEventTypeId;
            SelectedUserId = selectedUserId;
        }
        public List<EventDto> Events { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> EventTypes { get; set; }
        public int? SelectedUserId { get; set; }
        public int? SelectedEventTypeId { get; set; }
    }
}
