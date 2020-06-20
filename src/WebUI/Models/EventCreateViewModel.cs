using EventsCore.Application.EventSerieses.Queries.GetEventSeriesesList;
using EventsCore.Application.EventTypes.Queries.GetEventTypesList;
using EventsCore.Common;
using EventsCore.WebUI.Models.Enums;
using EventsCore.WebUI.Models.Validators;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsCore.WebUI.Models
{
    public class EventCreateViewModel
    {
        public EventCreateViewModel() { }
        public EventCreateViewModel(List<EventTypeDto> eventTypes, List<EventSeriesDto> eventSeries, IDateTime dateTime)
        {
            DateTime defaultDate = dateTime.Now;
            StartDate = defaultDate;
            EndDate = defaultDate.AddHours(10);
            RegistrationOpenDate = defaultDate;
            RegistrationClosedDate = defaultDate;
            EventTypes = eventTypes.ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            EventSerieses = eventSeries.ConvertAll(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Title });            
        }
        [Display(Name = "Event Id")]
        public int Id { get; set; }
        [Display(Name = "Event Type"), Required]
        public int EventTypeId { get; set; }
        [Display(Name = "Event Series")]
        public int? EventSeriesId { get; set; }
        [Display(Name = "Title"),
            Required(ErrorMessage = "Event Title is required"),
            StringLength(50, ErrorMessage = "Event Title cannot be longer than 50 characters")]
        public string Title { get; set; }
        [Display(Name = "Description"), Required]
        public string Description { get; set; }
        [Display(Name = "Fund Center"),
            StringLength(25, ErrorMessage = "Fund Center cannot be longer than 25 characters.")]
        public string FundCenter { get; set; }
        [Display(Name = "Start Date/Time"),
            Required,
            DateMustBeFuture]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date/Time"),
            Required,
            MustBeAfterDate("StartDate", ErrorMessage = "Event End Date/Time must be after Start Date/Time")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Registration Open Date"),
            Required,
            DateMustBeFuture]
        public DateTime RegistrationOpenDate { get; set; }
        [Display(Name = "Registration Closed Date"),
            Required,
            MustBeBeforeDate("StartDate", ErrorMessage = "Registration period Start Date cannot be after the Event's Start Date"),
            MustBeAfterDate("RegistrationOpenDate", ErrorMessage = "Registration Period End Date cannot be before the Registration Period End Date")]
        public DateTime RegistrationClosedDate { get; set; }
        [Display(Name = "Min Registrations"), NumberMustBeLessThanNumber("MaxRegistrationCount", ErrorMessage = "Min Registrations must be less than Max Registrations")]
        public int? MinRegistrationCount { get; set; }
        [Display(Name = "Max Registrations"), Required]
        public int MaxRegistrationCount { get; set; }
        [Display(Name = "Allow Standy Registrations")]
        public bool AllowStandby { get; set; }
        [Display(Name = "Max Standy Registrations"),
            RequireIfRelatedFieldTrue("AllowStandby", ErrorMessage = "You must provide the maximum number of allowed standby registrations to allow standby")]
        public int? MaxStandbyRegistrationCount { get; set; }
        [Display(Name = "Street Address"), Required, StringLength(50)]
        public string AddressLine1 { get; set; }
        [Display(Name = "Ste/Apt/Room#"), StringLength(25)]
        public string AddressLine2 { get; set; }
        [Display(Name = "City"), Required, StringLength(50)]
        public string City { get; set; }
        [Display(Name = "State"), Required]
        public string State { get; set; }
        [Display(Name = "ZIP Code"), Required, StringLength(5)]
        public string Zip { get; set; }
        public List<SelectListItem> EventTypes { get; set; }
        public List<SelectListItem> States => StaticData.StatesList;
        public List<SelectListItem> EventSerieses { get; set; }
    }
}
