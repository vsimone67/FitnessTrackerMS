import { State, Action, StateContext, Selector } from "@ngxs/store";
import { DietStateModel } from "../models/dietState.model";
import { GetAllMenuItems } from "../actions/diet.actions";
import { DietService } from "../service/diet.service";
import { FoodInfo } from "../models";

@State<DietStateModel>({
  name: "diet",
  defaults: {
    foodItems: [],
    foodItem: new FoodInfo()
  }
})
export class DietState {
  constructor(private _dietService: DietService) {}

  @Selector()
  static foodItems(state: DietStateModel) {
    return state.foodItems;
  }

  @Selector()
  static foodItem(state: DietStateModel) {
    return state.foodItem;
  }

  @Action(GetAllMenuItems)
  GetAllMenuItems({ getState, patchState }: StateContext<DietStateModel>) {
    const state = getState();

    this._dietService.getDietItems((dietItems: any) => {
      patchState({
        ...state,
        foodItems: dietItems
      });
    });
  }

  // @Action(GetMenuItem)
  //  getWorkout( {getState, patchState } : StateContext<WorkoutStateModel>,workout: GetWorkout ) {

  //     const state = getState();

  //   this._workoutService.getWorkout(workout.workoutID, (workout: Workout) => {
  //     patchState({
  //         ...state,
  //         currentWorkout:  workout
  //     })

  //   });
  // }

  // @Action(SaveWorkout)
  // saveWorkout({ getState, patchState }: StateContext<WorkoutStateModel>, workout: SaveWorkout) {
  //   const state = getState();

  //       this._workoutService.saveDailyWorkout(workout.workout,(() => {

  //         patchState({
  //             ...state,
  //             currentWorkout:  workout.workout
  //         })
  //       }));
  // }
}
