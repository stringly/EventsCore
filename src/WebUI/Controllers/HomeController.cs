using EventsCore.Application.Events.Queries.GetEventsList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }        
        public IActionResult About()
        {
            return View();
        }        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public async Task<IActionResult> GetUpcomingEvents()
        {
            var vm = await _mediator.Send(new GetEventListQuery());
            if (vm == null)
            {
                return NotFound();
            }
            return ViewComponent("UpcomingEvents", vm);

        }

    }
}
