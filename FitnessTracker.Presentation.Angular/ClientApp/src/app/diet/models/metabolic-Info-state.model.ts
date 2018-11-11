import { MeatabolicInfo } from "./metabolic-info.model";

export interface MeatabolicInfoStateModel {
  metabolicInfoList: Array<MeatabolicInfo>;
  metabolicInfo: MeatabolicInfo;
}
