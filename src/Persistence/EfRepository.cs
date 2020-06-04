using EventsCore.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using EventsCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EventsCore.Persistence
{
    /// <summary>
    /// Implementation of <see cref="IAsyncRepository{T}"></see> for <see cref="BaseEntity"></see> derived classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        private readonly EventsCoreDbContext _context;
        /// <summary>
        /// Creates a new instance of the Repository
        /// </summary>
        /// <param name="context">An <see cref="EventsCoreDbContext"></see></param>
        public EfRepository(EventsCoreDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieves and Entity by it's Id.
        /// </summary>
        /// <param name="id">The integer Id of the Entity</param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        /// <summary>
        /// Retrieves a Read-only list of the Entities in the repository.
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        /// <summary>
        /// Retrieves a read-only list of the Entities in the repository based on a specification.
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        /// <summary>
        /// Returns a count of the entities that match the specification
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see> containing the specifications for the count.</param>
        /// <returns></returns>
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        /// <summary>
        /// Adds an entity to the Repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns></returns>
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns></returns>
        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns></returns>
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Returns the first entity that matches the specification.
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see> containing the specifications.</param>
        /// <returns></returns>
        public async Task<T> FirstAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstAsync();
        }
        /// <summary>
        /// Returns the first entity that matches the specification, or null if none is found.
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see> containing the specifications.</param>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Applies the specifications.
        /// </summary>
        /// <param name="spec">An implementation of <see cref="ISpecification{T}"></see></param>
        /// <returns></returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
