using FitnetssTracker.Application.Common;
using System.Threading.Tasks;

namespace FitnetssTracker.Application.Common.Processor
{
    /// <summary>
    /// Finds the correct command handler and invokes it.  This helps reduce the amount of handlers you need to have references to.
    /// </summary>
    public interface ICommandProcessor
    {
        /// <summary>
        /// Processes the specified command by finding the appropriate handler and invoking it asynchronously.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="moduleReferenceName">The name of the module reference to store when creating/updating records in the database from the command.</param>
        Task<TResult> ProcessAsync<TResult>(ICommand command);
    }
}