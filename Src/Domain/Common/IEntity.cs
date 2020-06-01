namespace EventsCore.Domain.Common
{
    /// <summary>
    /// Interface that defines an Entity object
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The Id of the Entity
        /// </summary>
        int Id { get; }
    }
}
