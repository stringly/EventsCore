using EventsCore.Application.Common.Exceptions;
using EventsCore.Application.Events.Commands.CreateEvent;
using EventsCore.Application.Events.Commands.UpdateEvent;
using EventsCore.Application.Events.Queries.GetEventDetail;
using EventsCore.Application.Events.Queries.GetEventEdit;
using EventsCore.Application.Events.Queries.GetEventsList;
using EventsCore.Application.EventSerieses.Queries.GetEventSeriesesList;
using EventsCore.Application.EventTypes.Queries.GetEventTypesList;
using EventsCore.Common;
using EventsCore.WebUI.Common;
using EventsCore.WebUI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventsCore.WebUI.Controllers
{
    [Authorize]
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class EventController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IDateTime _dateTime;
        public EventController(IMediator mediator, IDateTime dateTime)
        {
            _mediator = mediator;
            _dateTime = dateTime;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? SelectedEventTypeId = null, int? SelectedEventSeriesId = null, int? SelectedUserId = null, int page = 1)
        {            
            var result = await _mediator.Send(new GetEventListQuery { EventTypeId = SelectedEventTypeId, SearchString = searchString, CurrentPage = page });
            var eventTypes = await _mediator.Send(new GetEventTypeListQuery());
            EventIndexViewModel vm = new 
                EventIndexViewModel(
                    result.Events.ToList(), 
                    eventTypes.EventTypes.ToList(), 
                    sortOrder, 
                    searchString, 
                    result.Count, 
                    page, 
                    25, 
                    SelectedUserId, 
                    SelectedEventTypeId);
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Create(string returnUrl)
        {
            var eventTypeResult = await _mediator.Send(new GetEventTypeListQuery());
            var eventSeriesResult = await _mediator.Send(new GetEventSeriesesListQuery());
            EventCreateViewModel vm = new EventCreateViewModel(eventTypeResult.EventTypes.ToList(), eventSeriesResult.EventSerieses.ToList(), _dateTime);
            ViewBag.ReturnUrl = returnUrl;
            ViewData["Title"] = "Create Event";
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(
            "EventTypeId," +
            "EventSeriesId," +
            "Title," +
            "Description,"+
            "FundCenter," +
            "StartDate," +
            "EndDate," +
            "RegistrationOpenDate," +
            "RegistrationClosedDate," +
            "MinRegistrationCount," +
            "MaxRegistrationCount," +
            "AllowStandby," +
            "MaxStandbyRegistrationCount,"+
            "AddressLine1," +
            "AddressLine2," +
            "City," +
            "State," +
            "Zip"
            )] EventCreateViewModel form, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                var eventTypeResult = await _mediator.Send(new GetEventTypeListQuery());
                var eventSeriesResult = await _mediator.Send(new GetEventSeriesesListQuery());
                form.EventTypes = eventTypeResult.EventTypes.ToList().ConvertAll(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString() });
                form.EventSerieses = eventSeriesResult.EventSerieses.ToList().ConvertAll(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }); 
                ViewBag.ReturnUrl = returnUrl;
                ViewData["Title"] = "Create Event: Error";                
                return View(form);
            }
            try
            {
                var command = new CreateEventCommand
                {
                    EventTypeId = form.EventTypeId,
                    EventSeriesId = form.EventSeriesId,
                    Title = form.Title,
                    Description = form.Description,
                    StartDate = form.StartDate,
                    EndDate = form.EndDate,
                    RegStartDate = form.RegistrationOpenDate,
                    RegEndDate = form.RegistrationClosedDate,
                    MaxRegsCount = form.MaxRegistrationCount,
                    MinRegsCount = form.MinRegistrationCount,
                    MaxStandbyCount = form.MaxStandbyRegistrationCount,
                    Street = form.AddressLine1,
                    Suite = form.AddressLine2,
                    City = form.City,
                    State = form.State,
                    Zip = form.Zip
                };
                await _mediator.Send(command);
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction(nameof(Index));

            }
            catch(ValidationException ex)
            {
                foreach (var failure in ex.Failures)
                {
                    foreach (var message in failure.Value)
                    {
                        ModelState.AddModelError(failure.Key, message);
                    }
                }
                var eventTypeResult = await _mediator.Send(new GetEventTypeListQuery());
                var eventSeriesResult = await _mediator.Send(new GetEventSeriesesListQuery());
                form.EventTypes = eventTypeResult.EventTypes.ToList().ConvertAll(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                form.EventSerieses = eventSeriesResult.EventSerieses.ToList().ConvertAll(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
                ViewBag.ReturnUrl = returnUrl;
                ViewData["Title"] = "Create Event: Error";
                return View(form);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id, string returnUrl)
        {
            var result = await _mediator.Send(new GetEventDetailQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            EventDetailViewModel vm = new EventDetailViewModel(result);
            ViewData["Title"] = "Event Detail";
            ViewBag.ReturnUrl = returnUrl;
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id, string returnUrl)
        {
            var result = await _mediator.Send(new GetEventEditQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            var eventTypeResult = await _mediator.Send(new GetEventTypeListQuery());
            var eventSeriesResult = await _mediator.Send(new GetEventSeriesesListQuery());
            EventEditViewModel vm = new EventEditViewModel(result, eventTypeResult.EventTypes.ToList(), eventSeriesResult.EventSerieses.ToList());
            ViewData["Title"] = "Edit Event";
            ViewBag.ReturnUrl = returnUrl;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind(
            "Id," +
            "EventTypeId," +
            "EventSeriesId," +
            "Title," +
            "Description,"+
            "FundCenter," +
            "StartDate," +
            "EndDate," +
            "RegistrationOpenDate," +
            "RegistrationClosedDate," +
            "MinRegistrationCount," +
            "MaxRegistrationCount," +
            "AllowStandby," +
            "MaxStandbyRegistrationCount,"+
            "AddressLine1," +
            "AddressLine2," +
            "City," +
            "State," +
            "Zip"
            )] EventEditViewModel form, string returnUrl)
        {
            if(id != form.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var eventTypeResult = await _mediator.Send(new GetEventTypeListQuery());
                var eventSeriesResult = await _mediator.Send(new GetEventSeriesesListQuery());
                form.EventTypes = eventTypeResult.EventTypes.ToList().ConvertAll(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                form.EventSerieses = eventSeriesResult.EventSerieses.ToList().ConvertAll(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
                ViewBag.ReturnUrl = returnUrl;
                ViewData["Title"] = "Edit Event: Error";
                return View(form);
            }
            try
            {
                var command = new UpdateEventCommand
                {
                    Id = form.Id,
                    EventTypeId = form.EventTypeId,
                    EventSeriesId = form.EventSeriesId,
                    Title = form.Title,
                    Description = form.Description,
                    StartDate = form.StartDate,
                    EndDate = form.EndDate,
                    RegStartDate = form.RegistrationOpenDate,
                    RegEndDate = form.RegistrationClosedDate,
                    MaxRegsCount = form.MaxRegistrationCount,
                    MinRegsCount = form.MinRegistrationCount,
                    MaxStandbyCount = form.MaxStandbyRegistrationCount,
                    Street = form.AddressLine1,
                    Suite = form.AddressLine2,
                    City = form.City,
                    State = form.State,
                    Zip = form.Zip
                };
                await _mediator.Send(command);
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(ValidationException ex)
            {
                foreach (var failure in ex.Failures)
                {
                    foreach(var message in failure.Value)
                    {
                        ModelState.AddModelError(failure.Key, message);
                    }                    
                }
                var eventTypeResult = await _mediator.Send(new GetEventTypeListQuery());
                var eventSeriesResult = await _mediator.Send(new GetEventSeriesesListQuery());
                form.EventTypes = eventTypeResult.EventTypes.ToList().ConvertAll(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                form.EventSerieses = eventSeriesResult.EventSerieses.ToList().ConvertAll(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
                ViewBag.ReturnUrl = returnUrl;
                ViewData["Title"] = "Create Event: Error";
                return View(form);
            }
        }
    }
}
