using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;
using System.Linq;

namespace EventsCore.Application.Events.Queries.GetEventEdit
{
    /// <summary>
    /// Data Transfer class used to respond to requests to edit an event.
    /// </summary>
    public class EventEditDto : IMapFrom<Event>
    {
        /// <summary>
        /// The entity Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Event's title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The Event's description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The Event's <see cref="Domain.Entities.EventType"/> Id.
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// The Event's <see cref="Domain.Entities.EventType"/> name.
        /// </summary>
        public string EventTypeTitle { get; set; }
        /// <summary>
        /// The Event's Series Id, if any.
        /// </summary>
        public int? EventSeriesId { get; set; }
        /// <summary>
        /// The Event's Series Name, if any
        /// </summary>
        public string EventSeriesName { get; set; }
        /// <summary>
        /// The UserId of the Event's Owner
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// The Display Name of the Event's Owner
        /// </summary>
        public string OwnerDisplayName { get; set; }
        /// <summary>
        /// The email address of the Owner
        /// </summary>
        public string OwnerEmail { get; set; }
        /// <summary>
        /// The Event's Start Date
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// The Event's End Date
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// The Event's Registration Period Start Date
        /// </summary>
        public string RegStartDate { get; set; }
        /// <summary>
        /// The Event's Registration Period End Date
        /// </summary>
        public string RegEndDate { get; set; }
        /// <summary>
        /// The Max number of attendees allowed for the event
        /// </summary>
        public int MaxRegs { get; set; }
        /// <summary>
        /// The Minimum number of attendees required for the event.
        /// </summary>
        public int MinRegs { get; set; }
        /// <summary>
        /// The Maximum number of registrations allowed for the event.
        /// </summary>
        public int MaxStandByRegs { get; set; }
        /// <summary>
        /// The number of Registrations for the event with the status "Accepted."
        /// </summary>
        public int CurrentAcceptedRegistrationsCount { get; set; }
        /// <summary>
        /// The number of Registrations for the event with the status "Rejected."
        /// </summary>
        public int CurrentRejectedRegistrationsCount { get; set; }
        /// <summary>
        /// The number of Registrations for the event with the status "Pending."
        /// </summary>
        public int CurrentPendingRegistrationsCount { get; set; }
        /// <summary>
        /// The number of Registrations for the event with the status "Standby."
        /// </summary>
        public int CurrentStandbyRegistrationsCount { get; set; }
        /// <summary>
        /// The Event's location street address
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// The Event's location suite/apt/room number
        /// </summary>
        public string Suite { get; set; }
        /// <summary>
        /// The Event's location City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// The Event's location State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// The Event's location Zip
        /// </summary>
        public string Zip { get; set; }


        /// <summary>
        /// Creates a mapping between the base <see cref="Event"/> and the Dto <see cref="EventEditDto"/>
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, EventEditDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(e => e.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(e => e.EventTypeId, opt => opt.MapFrom(s => s.EventTypeId))
                .ForMember(e => e.EventTypeTitle, opt => opt.MapFrom(s => s.EventType.Name))
                .ForMember(e => e.EventSeriesId, opt => opt.MapFrom(s => s.EventSeriesId))
                .ForMember(e => e.EventSeriesName, opt => opt.MapFrom(s => s.EventSeries == null ? "None" : s.EventSeries.Title))
                .ForMember(e => e.OwnerId, opt => opt.MapFrom(s => s.OwnerId))
                .ForMember(e => e.OwnerEmail, opt => opt.MapFrom(s => s.Owner.Email))
                .ForMember(e => e.OwnerDisplayName, opt => opt.MapFrom(s => s.Owner.DisplayName))
                .ForMember(e => e.StartDate, opt => opt.MapFrom(s => $"{s.Dates.StartDate:MM/dd/yy HH:mm}"))
                .ForMember(e => e.EndDate, opt => opt.MapFrom(s => $"{s.Dates.EndDate:MM/dd/yy HH:mm}"))
                .ForMember(e => e.RegStartDate, opt => opt.MapFrom(s => $"{s.Dates.RegistrationStartDate:MM/dd/yy HH:mm}"))
                .ForMember(e => e.RegEndDate, opt => opt.MapFrom(s => $"{s.Dates.RegistrationEndDate:MM/dd/yy HH:mm}"))
                .ForMember(e => e.Street, opt => opt.MapFrom(s => s.Address.Street))
                .ForMember(e => e.Suite, opt => opt.MapFrom(s => s.Address.Suite))
                .ForMember(e => e.City, opt => opt.MapFrom(s => s.Address.City))
                .ForMember(e => e.State, opt => opt.MapFrom(s => s.Address.State))
                .ForMember(e => e.Zip, opt => opt.MapFrom(s => s.Address.ZipCode))
                .ForMember(e => e.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(e => e.MaxRegs, opt => opt.MapFrom(s => s.Rules.MaxRegistrations))
                .ForMember(e => e.MinRegs, opt => opt.MapFrom(s => s.Rules.MinRegistrations))
                .ForMember(e => e.MaxStandByRegs, opt => opt.MapFrom(s => s.Rules.MaxStandbyRegistrations))
                .ForMember(e => e.CurrentAcceptedRegistrationsCount, opt => opt.MapFrom(s => s.Registrations.Count(x => x.Status == RegistrationStatus.Accepted)))
                .ForMember(e => e.CurrentRejectedRegistrationsCount, opt => opt.MapFrom(s => s.Registrations.Count(x => x.Status == RegistrationStatus.Rejected)))
                .ForMember(e => e.CurrentPendingRegistrationsCount, opt => opt.MapFrom(s => s.Registrations.Count(x => x.Status == RegistrationStatus.Pending)))
                .ForMember(e => e.CurrentStandbyRegistrationsCount, opt => opt.MapFrom(s => s.Registrations.Count(x => x.Status == RegistrationStatus.Standby)))
                ;
        }
    }
}
