namespace EventsCore.Application.Common.Interfaces
{
    /// <summary>
    /// Defines a service to retrieve information about the current user.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// The current User's Id
        /// </summary>
        int UserId { get;}
        /// <summary>
        /// Whether the current user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }
    }
}
