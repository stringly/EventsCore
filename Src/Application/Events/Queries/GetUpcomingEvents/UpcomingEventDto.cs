using AutoMapper;
using EventsCore.Application.Common.Mappings;
using EventsCore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EventsCore.Application.Events.Queries.GetUpcomingEvents
{
    /// <summary>
    /// Data Transfer Class used in the <see cref="GetUpcomingEventsQuery"/> 
    /// </summary>
    public class UpcomingEventDto : IMapFrom<Event>
    {
        /// <summary>
        /// The entity Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Event's <see cref="EventType"/> name.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// The Event's <see cref="EventSeries"/> name.
        /// </summary>
        public string SeriesName { get; set; }
        /// <summary>
        /// The Event's <see cref="EventSeries"/> id.
        /// </summary>
        public int? SeriesId { get; set; }
        /// <summary>
        /// The Event's title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The Event's StartDate
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// The Date that the event's registration period ends.
        /// </summary>
        public string RegistrationClosedDate { get; set; }
        /// <summary>
        /// The number of vacancies.
        /// </summary>
        public int SlotsAvailable { get; set; }
        /// <summary>
        /// The Email address of the Event's owner.
        /// </summary>
        public string OwnerEmail { get; set; }
        /// <summary>
        /// The Description of the Event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Creates a mapping between the base <see cref="Event"/> and the Dto <see cref="UpcomingEventDto"/>
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Event, UpcomingEventDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(e => e.TypeName, opt => opt.MapFrom(s => s.EventType.Name))
                .ForMember(e => e.SeriesName, opt => opt.MapFrom(s => s.EventSeries == null ? "None" : s.EventSeries.Title))
                .ForMember(e => e.SeriesId, opt => opt.MapFrom(s => s.EventSeriesId))
                .ForMember(e => e.Title, opt => opt.MapFrom(s => s.Title))
                .ForMember(e => e.Date, opt => opt.MapFrom(s => $"{s.Dates.StartDate:ddd, MM/dd/yy} {s.Dates.StartDate:H:mm tt} - {s.Dates.EndDate:H:mm tt}"))
                .ForMember(e => e.SlotsAvailable, opt => opt.MapFrom(s => s.GetAvailableSlotsCount()))
                .ForMember(e => e.RegistrationClosedDate, opt => opt.MapFrom(s => s.Dates.RegistrationEndDate.ToString("ddd, MM/dd/yy")))
                .ForMember(e => e.OwnerEmail, opt => opt.MapFrom(s => s.Owner.Email))
                .ForMember(e => e.Description, opt => opt.MapFrom(s => s.Description));
        }
    }
}
