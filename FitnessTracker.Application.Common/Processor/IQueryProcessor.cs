using FitnetssTracker.Application.Common;
using System.Threading.Tasks;

namespace FitnetssTracker.Application.Common.Processor
{
    /// <summary>
    /// Finds the correct query handler and invokes it.  This helps reduce the amount of handlers you need to have references to.
    /// </summary>
    public interface IQueryProcessor
    {
        /// <summary>
        /// Processes the specified query by finding the appropriate handler and invoking it asynchronously.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="query">The query to process.</param>
        /// <returns>The query result.</returns>
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
    }
}