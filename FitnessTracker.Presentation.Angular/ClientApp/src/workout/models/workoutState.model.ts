import { Workout } from './';

export interface WorkoutState {
  event: string;
  workouts?: Array<Workout>;
  currentWorkout?: Workout;
}

