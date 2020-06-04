using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EventsCore.Application.Common.Interfaces
{
    /// <summary>
    /// Defines a Specification used to filter queries
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// An expression containing filtering criteria
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }
        /// <summary>
        /// An expression containing Includes
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }
        /// <summary>
        /// A List of strings containing include specifications
        /// </summary>
        List<string> IncludeStrings { get; }
        /// <summary>
        /// An expression containing ordering criteria
        /// </summary>
        Expression<Func<T, object>> OrderBy { get; }
        /// <summary>
        /// An expression containing descending ordering criteria
        /// </summary>
        Expression<Func<T, object>> OrderByDescending {  get; }
        /// <summary>
        /// An expression containing grouping criteria
        /// </summary>
        Expression<Func<T, object>> GroupBy { get; }
        /// <summary>
        /// Integer used to determine the number of results to return in a paging operation
        /// </summary>
        int Take { get;}
        /// <summary>
        /// Integer used to determine how many items to skip as part of a paging operation
        /// </summary>
        int Skip { get;}
        /// <summary>
        /// Indicates whether paging is enabled.
        /// </summary>
        bool IsPagingEnabled { get;}
    }
}
