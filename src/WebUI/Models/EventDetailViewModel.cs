using EventsCore.Application.Events.Queries.GetEventDetail;
using System.ComponentModel.DataAnnotations;

namespace EventsCore.WebUI.Models
{
    public class EventDetailViewModel
    {
        public EventDetailViewModel()
        {
        }
        public EventDetailViewModel(EventDetailDto e)
        {           
            
                Id = e.Id;
                EventTypeName = e.EventTypeTitle;
                EventSeriesId = e?.EventSeriesId ?? 0;
                EventSeriesName = e?.EventSeriesName ?? "";
                CreatorName = e.OwnerDisplayName;
                CreatorEmail = e.OwnerEmail;
                Title = e.Title;
                Description = e.Description;
                //FundCenter = e.FundCenter;
                StartDate = e.StartDate;
                EndDate = e.EndDate;
                RegistrationOpenDate = e.RegStartDate;
                RegistrationClosedDate = e.RegEndDate;
                MinRegistrationCount = e.MinRegs;
                MaxRegistrationCount = e.MaxRegs;
                AllowStandby = e.MaxStandByRegs < 0;
                MaxStandbyRegistrationCount = e.MaxStandByRegs;
                AddressLine1 = e.Street;
                AddressLine2 = e.Suite;
                City = e.City;
                State = e.State;
                Zip = e.Zip;
                TotalRegistrations = e.CurrentAcceptedRegistrationsCount + e.CurrentPendingRegistrationsCount + e.CurrentRejectedRegistrationsCount + e.CurrentStandbyRegistrationsCount;
                AcceptedRegistrations = e.CurrentAcceptedRegistrationsCount;
                PendingRegistrations = e.CurrentPendingRegistrationsCount;
                StandbyRegistrations = e.CurrentStandbyRegistrationsCount;
                RejectedRegistrations = e.CurrentRejectedRegistrationsCount;
            
        }
        [Display(Name = "Event Id")]
        public int Id { get; set; }
        [Display(Name = "Event Type")]
        public string EventTypeName { get; set; }
        [Display(Name = "Event Series")]
        public int EventSeriesId { get; set; }
        [Display(Name = "Event Series")]
        public string EventSeriesName { get; set; }
        [Display(Name = "Created By")]
        public string CreatorName { get; set; }
        [Display(Name = "Email")]
        public string CreatorEmail { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Fund Center")]
        public string FundCenter { get; set; }
        [Display(Name = "Start Date/Time")]
        public string StartDate { get; set; }
        [Display(Name = "End Date/Time")]
        public string EndDate { get; set; }
        [Display(Name = "Registration Open Date")]
        public string RegistrationOpenDate { get; set; }
        [Display(Name = "Registration Closed Date")]
        public string RegistrationClosedDate { get; set; }
        [Display(Name = "Min Registrations")]
        public int MinRegistrationCount { get; set; }
        [Display(Name = "Max Registrations")]
        public int MaxRegistrationCount { get; set; }
        [Display(Name = "Allow Standy Registrations")]
        public bool AllowStandby { get; set; }
        [Display(Name = "Max Standy Registrations")]
        public int MaxStandbyRegistrationCount { get; set; }
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
        [Display(Name = "Total")]
        public int TotalRegistrations { get; set; }
        [Display(Name = "Accepted")]
        public int AcceptedRegistrations { get; set; }
        [Display(Name = "Pending")]
        public int PendingRegistrations { get; set; }
        [Display(Name = "Standy")]
        public int StandbyRegistrations { get; set; }
        [Display(Name = "Rejected")]
        public int RejectedRegistrations { get; set; }
    }
}
