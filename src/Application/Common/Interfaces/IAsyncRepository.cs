using EventsCore.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsCore.Application.Common.Interfaces
{
    /// <summary>
    /// Defines an async Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        /// <summary>
        /// Retrieves an entity by Id
        /// </summary>
        /// <param name="id">The Id of the entity</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);
        /// <summary>
        /// Returns a readonly list of all of the Entities
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<T>> ListAllAsync();
        /// <summary>
        /// Returns a readonly list of the Entities that match the specification
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        /// <summary>
        /// Adds an entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// Updates and entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns></returns>
        Task UpdateAsync(T entity);
        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns></returns>
        Task DeleteAsync(T entity);
        /// <summary>
        /// Counts the number of entities that match the specification
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see></param>
        /// <returns></returns>
        Task<int> CountAsync(ISpecification<T> spec);
        /// <summary>
        /// Returns the first entity that matches the specification
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see></param>
        /// <returns></returns>
        Task<T> FirstAsync(ISpecification<T> spec);
        /// <summary>
        /// Returns the first entity that matches the specification, or null if none is found
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);

    }
}
