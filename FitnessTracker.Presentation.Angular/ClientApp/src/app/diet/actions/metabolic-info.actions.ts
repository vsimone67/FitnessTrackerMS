import { MeatabolicInfo } from "../models/metabolic-info.model";

export class GetMetabolicInfo {
  static readonly type = "[metabolidinfo] GetMetabolicInfo";
}

export class SaveMetabolicInfo {
  static readonly type = "[metabolidinfo] SaveMetabolicInfo";
  constructor(public metabolicInfo: MeatabolicInfo) {}
}
