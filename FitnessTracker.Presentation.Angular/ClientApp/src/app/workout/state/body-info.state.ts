import { State, Action, StateContext, Selector } from "@ngxs/store";
import { BodyInfoStateModel } from "../models/body-Info-state.model";
import { GetBodyInfo, SaveBodyInfo } from "../actions/body-info.actions";
import { WorkoutService } from "../services/workout.service";

@State<BodyInfoStateModel>({
  name: "BodyInfo",
  defaults: {
    bodyInfoItems: []
  }
})
export class BodyInfoState {
  constructor(private _workoutService: WorkoutService) { }

  @Selector()
  static BodyInfoItems(state: BodyInfoStateModel) {
    return state.bodyInfoItems;
  }

  @Action(GetBodyInfo)
  GetBodyInfo(ctx: StateContext<BodyInfoStateModel>) {
    const state = ctx.getState();

    this._workoutService.getBodyInfo((bodyInfoItems: any[]) => {
      ctx.patchState({
        ...state,
        bodyInfoItems: bodyInfoItems
      });
    });
  }

  @Action(SaveBodyInfo)
  saveWorkout(ctx: StateContext<BodyInfoStateModel>, bodyInfo: SaveBodyInfo) {
    const state = ctx.getState();

    this._workoutService.saveBodyInfo(bodyInfo.bodyInfo, () => {
      ctx.patchState({
        ...state,
        bodyInfoItems: [...state.bodyInfoItems, bodyInfo.bodyInfo]
      });
    });
  }
}
