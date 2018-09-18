using FitnessTracker.Application.Processor;
using FitnetssTracker.Common.Helpers;
using System.Threading.Tasks;

namespace FitnetssTracker.Application.Common.Processor
{
    /// <inheritdoc />
    public class QueryProcessor : IQueryProcessor
    {
        /// <summary>
        /// Delegate to the GetInstance (or similar) method used by the dependency resolver/container.
        /// </summary>
        private readonly DependencyResolverGetInstance _getInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryProcessor"/> class.
        /// </summary>
        /// <param name="getInstance">Delegate to the GetInstance (or similar) method used by the dependency resolver/container.</param>
        public QueryProcessor(DependencyResolverGetInstance getInstance)
        {
            _getInstance = getInstance;
        }

        public async Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            Check.Require(query != null, Messages.exception_query_cannot_be_null);

            var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));

            var queryHandler = _getInstance(queryHandlerType);

            return await queryHandler.HandleAsync((dynamic)query);
        }
    }
}