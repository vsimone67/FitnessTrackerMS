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
  GetAllWorkouts({ getState, patchState }: StateContext<WorkoutStateModel>) {
    const state = getState();

    this._workoutService.getWorkouts((workouts: any) => {
      patchState({
        ...state,
        workouts: workouts
      });
    });
  }

  @Action(GetWorkout)
  getWorkout(
    { getState, patchState }: StateContext<WorkoutStateModel>,
    workout: GetWorkout
  ) {
    const state = getState();

    this._workoutService.getWorkout(workout.workoutID, (workout: Workout) => {
      patchState({
        ...state,
        currentWorkout: workout
      });
    });
  }

  @Action(SaveWorkout)
  saveWorkout(
    { getState, patchState }: StateContext<WorkoutStateModel>,
    workout: SaveWorkout
  ) {
    const state = getState();

    this._workoutService.saveDailyWorkout(workout.workout, () => {
      patchState({
        ...state,
        currentWorkout: workout.workout
      });
    });
  }
}
