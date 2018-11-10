import { FoodInfo } from "../models/foodInfo.model";

export class GetAllMenuItems {
  static readonly type = "[diet] GetAllMenuItems";
}

export class AddMenuItem {
  static readonly type = "[diet] AddMenuItem";

  constructor(public foodInfo: FoodInfo) {}
}

export class DeleteMenuItem {
  static readonly type = "[diet] DeleteMenuItem";

  constructor(public foodInfo: FoodInfo) {}
}
