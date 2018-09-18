using System.Threading.Tasks;

namespace FitnetssTracker.Application.Common
{
    public interface ICommandHandler<in TQuery, TResult> where TQuery : ICommand
    {
        TResult Handle(TQuery query);
        Task<TResult> HandleAsync(TQuery query);
    }
}

