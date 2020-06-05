using MediatR;
using System;

namespace EventsCore.Application.Events.Commands.CreateEvent
{    /// <summary>
     /// Implementation of <see cref="IRequest"></see> that creates/updates a <see cref="Domain.Entities.Event"></see>
     /// </summary>
    public class CreateEventCommand : IRequest<int>
    {
        /// <summary>
        /// A string containing the Event's title
        /// </summary>
        /// <remarks>
        /// This is a required field with a max length of 50 characters
        /// </remarks>
        public string Title { get; set; }
        /// <summary>
        /// A string containing the Event's Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A <see cref="DateTime"></see> containing the Event's Start Date.
        /// </summary>
        /// <remarks>
        /// This must be a future date.
        /// </remarks>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// A <see cref="EndDate"></see> containing the Event's End Date.
        /// </summary>
        /// <remarks>
        /// This must be a date after <see cref="StartDate"></see>
        /// </remarks>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// The start date of the Event's Registration Period
        /// </summary>
        /// <remarks>
        /// This must be a date before <see cref="StartDate"/>
        /// </remarks>
        public DateTime RegStartDate { get; set; }
        /// <summary>
        /// The end date of the Event's Registration Period.
        /// </summary>
        /// <remarks>
        /// This must be a date after <see cref="RegStartDate"></see>
        /// </remarks>
        public DateTime RegEndDate { get; set; }
        /// <summary>
        /// The maximum number of attendees the event can have.
        /// </summary>
        public int MaxRegsCount { get; set; }
        /// <summary>
        /// The minimum number of attendees required for the Event.
        /// </summary>
        /// <remarks>
        /// This is an optional field that will default to 0 if no value is provided. If a value is provided, it must be less than <see cref="MaxRegsCount"></see>
        /// </remarks>
        public int? MinRegsCount { get; set; }
        /// <summary>
        /// The maximum number of "Standby" registrations that will be allowed for the event.
        /// </summary>
        /// <remarks>
        /// This is an optional field that will default to 0 if no value is provided. A value must be provided to allow standby registrations.
        /// </remarks>
        public int? MaxStandbyCount { get; set; }
        /// <summary>
        /// The id of the Event's <see cref="Domain.Entities.EventType"></see>
        /// </summary>
        public int EventTypeId { get; set; }
        /// <summary>
        /// The id of the Event's <see cref="Domain.Entities.EventSeries"></see>
        /// </summary>
        /// <remarks>
        /// This is an optional field. Provide an Id for a valid <see cref="Domain.Entities.EventSeries"></see> to assign the Event to the series.
        /// </remarks>
        public int? EventSeriesId { get; set; }
    }
}
