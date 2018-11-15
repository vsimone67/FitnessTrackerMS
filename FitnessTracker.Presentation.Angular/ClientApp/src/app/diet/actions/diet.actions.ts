import { FoodInfo } from "../models/food-Info.model";
import { NutritionInfo } from "../models";

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

export class SaveMenu {
  static readonly type = "[diet] SaveMenu";

  constructor(public meals: Array<NutritionInfo>) {}
}

export class GetColumns {
  static readonly type = "[diet] GetColumns";
}

export class SetMeals {
  static readonly type = "[diet] SetMeals";

  constructor(public meals: Array<NutritionInfo>) {}
}
