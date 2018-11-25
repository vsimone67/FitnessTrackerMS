import { Workout } from '../models';

export class GetAllWorkouts {
  static readonly type = '[Workout] GetAllWorkouts';
}

export class GetWorkout {
  static readonly type = '[Workout] GetWorkout';

  constructor(public workoutID: number) { }
}

export class SaveWorkout {
  static readonly type = '[Workout] SaveWorkout';

  constructor(public workout: Workout) { }
}
