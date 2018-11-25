using FitnessTracker.Application.Common;
using FitnessTracker.Application.Model.Workout;

namespace FitnessTracker.Application.Command
{
    public class SaveBodyInfoCommand : ICommand
    {
        public BodyInfoDTO BodyInfo { get; set; }
    }
}