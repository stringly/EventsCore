namespace EventsCore.Application.Notifications.Models
{
    /// <summary>
    /// Data Transfer object used to send Notification Messages
    /// </summary>
    public class MessageDto
    {
        /// <summary>
        /// The message sender
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// The message recipient address
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// The message subject line
        /// </summary>
        public string Subject { get;set; }
        /// <summary>
        /// The message body.
        /// </summary>
        public string Body { get; set; }
    }
}
