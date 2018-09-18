using AutoMapper;
using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Domain.Workout;

namespace FitnessTracker.Application.MappingProfile
{
    public class WorkoutMappingProfile : Profile
    {
        public WorkoutMappingProfile()
        {

            CreateMap<BodyInfo, BodyInfoDTO>();
            CreateMap<BodyInfoDTO, BodyInfo>();

            CreateMap<DailyWorkout, DailyWorkoutDTO>();
            CreateMap<DailyWorkoutDTO, DailyWorkout>();

            CreateMap<DailyWorkoutInfo, DailyWorkoutInfoDTO>();
            CreateMap<DailyWorkoutInfoDTO, DailyWorkoutInfo>();            

            CreateMap<SetName, SetNameDTO>();
            CreateMap<SetNameDTO, SetName>();

            CreateMap<ExerciseName, ExerciseNameDTO>();
            CreateMap<ExerciseNameDTO, ExerciseName>();

            CreateMap<RepsName, RepsNameDTO>();
            CreateMap<RepsNameDTO, RepsName>();

            CreateMap<Reps, RepsDTO>();
            CreateMap<RepsDTO, Reps>();

            CreateMap<Set, SetDTO>();
            CreateMap<SetDTO, Set>();

            CreateMap<Exercise, ExerciseDTO>();
            CreateMap<ExerciseDTO, Exercise>();

            CreateMap<FitnessTracker.Domain.Workout.Workout, WorkoutDTO>();
            CreateMap<WorkoutDTO, FitnessTracker.Domain.Workout.Workout>();
            
        }
    }
}
