import { State, Action, StateContext, Selector } from "@ngxs/store";
import { WorkoutStateModel } from "../models/workout-state.model";
import { Workout } from "../models";
import {
  GetAllWorkouts,
  GetWorkout,
  SaveWorkout
} from "../actions/workout.actions";
import { WorkoutService } from "../services/workout.service";

@State<WorkoutStateModel>({
  name: "workout",
  defaults: {
    workouts: new Array<Workout>(),
    currentWorkout: new Workout()
  }
})
export class WorkoutState {
  constructor(private _workoutService: WorkoutService) {}

  @Selector()
  static workouts(state: WorkoutStateModel) {
    return state.workouts;
  }

  @Selector()
  static CurrentWorkout(state: WorkoutStateModel) {
    return state.currentWorkout;
  }

  @Action(GetAllWorkouts)
  GetAllWorkouts(ctx: StateContext<WorkoutStateModel>) {
    const state = ctx.getState();

    this._workoutService.getWorkouts((workouts: any) => {
      ctx.patchState({
        ...state,
        workouts: workouts
      });
    });
  }

  @Action(GetWorkout)
  getWorkout(ctx: StateContext<WorkoutStateModel>, workout: GetWorkout) {
    const state = ctx.getState();

    this._workoutService.getWorkout(workout.workoutID, (workout: Workout) => {
      ctx.patchState({
        ...state,
        currentWorkout: workout
      });
    });
  }

  @Action(SaveWorkout)
  saveWorkout(ctx: StateContext<WorkoutStateModel>, workout: SaveWorkout) {
    const state = ctx.getState();

    this._workoutService.saveDailyWorkout(workout.workout, () => {
      ctx.patchState({
        ...state,
        currentWorkout: workout.workout
      });
    });
  }
}
