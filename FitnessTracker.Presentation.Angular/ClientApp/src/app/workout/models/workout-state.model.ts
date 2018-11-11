import { Workout } from ".";

export interface WorkoutStateModel {
  workouts?: Array<Workout>;
  currentWorkout?: Workout;
}
