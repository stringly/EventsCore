using EventsCore.Application.Common.Interfaces;
using EventsCore.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventsCore.Persistence
{
    /// <summary>
    /// Evaluates a <see cref="ISpecification{T}"></see>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        /// <summary>
        /// Assembles the Query with the provided specifications
        /// </summary>
        /// <param name="inputQuery">An <see cref="IQueryable{T}"></see></param>
        /// <param name="specification">An <see cref="ISpecification{T}"></see></param>
        /// <returns></returns>
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query, 
                (current, include) => current.Include(include));
            // Includes any string-based include statements
            query = specification.IncludeStrings.Aggregate(query, 
                (current, include) => current.Include(include));
            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }
            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                    .Take(specification.Take);
            }
            return query;
        }
    }
}
