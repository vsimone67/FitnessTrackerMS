import { State, Action, StateContext, Selector } from "@ngxs/store";
import { MeatabolicInfoStateModel } from "../models/metabolicInfoState.model"
import { GetMetabolicInfo, SaveMetabolicInfo } from "../actions/metabolic-info.actions"
import { DietService } from "../service/diet.service";
import { MeatabolicInfo }  from '../models/metabolicInfo.model'

@State<MeatabolicInfoStateModel>({
  name: "metabolicinfo",
  defaults: {
    metabolicInfoList: [],
    metabolicInfo: new MeatabolicInfo
  }
})
export class MetabolicInfoState {
  constructor(private _dietService: DietService) {}

  @Selector()
  static metabolicInfoList(state: MeatabolicInfoStateModel) {
    return state.metabolicInfoList;
  } 

  @Selector()
  static metabolidInfo(state: MeatabolicInfoStateModel) {
    return state.metabolicInfo
  } 

  @Action(GetMetabolicInfo)
  getMetabolicInfo({ getState, patchState }: StateContext<MeatabolicInfoStateModel>) {
    const state = getState();

    this._dietService.getMetabolicInfo((metabolicInfoList: any) => {
      patchState({
        ...state,
        metabolicInfoList: metabolicInfoList
      });
    });
  }  

  @Action(SaveMetabolicInfo)
  saveMetabolicInfo({ getState, patchState }: StateContext<MeatabolicInfoStateModel>, metabolicinfo: SaveMetabolicInfo) {
    const state = getState();

        this._dietService.saveMetabolicInfo(metabolicinfo.metabolicInfo,(() => {

          patchState({
              ...state,
              metabolicInfo:  metabolicinfo.metabolicInfo
          })
        }));
  }
}
