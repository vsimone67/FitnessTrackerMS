import { FoodDefault, SavedMenu } from "./";

export class FoodInfo {
  ItemId: number;
  Item: string;
  ServingSize: string;
  Calories: number;
  Protien: number;
  Carbs: number;
  Fat: number;
  Serving: number;
  FoodDefault: Array<FoodDefault>;
  SavedMenu: Array<SavedMenu>;

  constructor() {
    this.SavedMenu = new Array<SavedMenu>();
  }
}
