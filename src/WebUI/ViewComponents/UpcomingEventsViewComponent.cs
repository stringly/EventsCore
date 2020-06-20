using EventsCore.Application.Events.Queries.GetEventsList;
using Microsoft.AspNetCore.Mvc;

namespace EventsCore.WebUI.ViewComponents
{
    public class UpcomingEventsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(EventListVm vm)
        {
            return View(vm);
        }
    }
}
