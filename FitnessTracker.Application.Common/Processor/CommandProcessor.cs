using FitnessTracker.Application.Processor;
using FitnetssTracker.Common.Helpers;
using System.Threading.Tasks;

namespace FitnetssTracker.Application.Common.Processor
{
    /// <inheritdoc />
    public class CommandProcessor : ICommandProcessor
    {
        /// <summary>
        /// Delegate to the GetInstance (or similar) method used by the dependency resolver/container.
        /// </summary>
        private readonly DependencyResolverGetInstance _getInstance;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandProcessor"/> class.
        /// </summary>
        /// <param name="getInstance">Delegate to the GetInstance (or similar) method used by the dependency resolver/container.</param>
        public CommandProcessor(DependencyResolverGetInstance getInstance)
        {
            _getInstance = getInstance;
        }

        /// <inheritdoc />
        public async Task<TResult> ProcessAsync<TResult>(ICommand command)
        {
            Check.Require(command != null, Messages.exception_command_cannot_be_null);

            var commandHandlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));

            var commandHandler = _getInstance(commandHandlerType);

            return await commandHandler.HandleAsync((dynamic)command);
        }
    }
}