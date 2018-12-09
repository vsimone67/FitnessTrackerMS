using EventBus.Abstractions;

namespace FitnessTracker.Common.Web.EventBusCreator
{
    public interface IEventBusInstance
    {
        IEventBus GetInstance();
    }
}