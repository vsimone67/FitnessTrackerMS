import { State, Action, StateContext, Selector } from "@ngxs/store";
import { MeatabolicInfoStateModel } from "../models/metabolic-Info-state.model";
import {
  GetMetabolicInfo,
  SaveMetabolicInfo
} from "../actions/metabolic-info.actions";
import { DietService } from "../service/diet.service";
import { MeatabolicInfo } from "../models/metabolic-info.model";

@State<MeatabolicInfoStateModel>({
  name: "metabolicinfo",
  defaults: {
    metabolicInfoList: [],
    metabolicInfo: new MeatabolicInfo()
  }
})
export class MetabolicInfoState {
  constructor(private _dietService: DietService) { }

  @Selector()
  static metabolicInfoList(state: MeatabolicInfoStateModel) {
    return state.metabolicInfoList;
  }

  @Selector()
  static metabolidInfo(state: MeatabolicInfoStateModel) {
    return state.metabolicInfo;
  }

  @Action(GetMetabolicInfo)
  getMetabolicInfo(ctx: StateContext<MeatabolicInfoStateModel>) {
    const state = ctx.getState();

    this._dietService.getMetabolicInfo((metabolicInfoList: any) => {
      ctx.patchState({
        ...state,
        metabolicInfoList: metabolicInfoList
      });
    });
  }

  @Action(SaveMetabolicInfo)
  saveMetabolicInfo(
    ctx: StateContext<MeatabolicInfoStateModel>,
    metabolicinfo: SaveMetabolicInfo
  ) {
    const state = ctx.getState();

    this._dietService.saveMetabolicInfo(metabolicinfo.metabolicInfo, () => {
      ctx.patchState({
        ...state,
        metabolicInfo: metabolicinfo.metabolicInfo
      });
    });
  }
}
