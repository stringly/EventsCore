using EventsCore.Application.Events.Queries.GetUpcomingEvents;
using Microsoft.AspNetCore.Mvc;

namespace EventsCore.WebUI.ViewComponents
{
    public class UpcomingEventsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UpcomingEventListVm vm)
        {
            return View(vm);
        }
    }
}
