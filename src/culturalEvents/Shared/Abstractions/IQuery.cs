using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Shared.Abstractions
{
    /// <summary>
    /// Marker interface for queries.
    /// </summary>
    public interface IQuery{}

    /// <summary>
    /// Interface for handling queries. It takes a query of type TQuery and returns a result of type TResult.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
    {
        /// <summary>
        /// Handles the query and returns the result asynchronously.
        /// </summary>
        /// <param name="query">The query to handle.</param>
        /// <returns>The result of the query.</returns>
        Task<TResult> HandleAsync(TQuery query);
    }
}