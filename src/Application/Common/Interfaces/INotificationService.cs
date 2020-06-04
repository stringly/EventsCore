using EventsCore.Application.Notifications.Models;
using System.Threading.Tasks;

namespace EventsCore.Application.Common.Interfaces
{
    /// <summary>
    /// Defines an interface used to send messages
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="message">A <see cref="MessageDto"></see> containing the message information.</param>
        /// <returns></returns>
        Task SendAsync(MessageDto message);
    }
}
