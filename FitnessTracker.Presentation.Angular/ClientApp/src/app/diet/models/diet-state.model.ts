import { FoodInfo, Columns, NutritionInfo } from ".";

export interface DietStateModel {
  foodItems?: Array<FoodInfo>;
  foodItem?: FoodInfo;
  columns: Array<Columns>;
  nutritionInfo: Array<NutritionInfo>;
}
