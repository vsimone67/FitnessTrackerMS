import { CurrentMenu } from ".";

export class NutritionInfo {
  id: number;
  meal: string;
  calories: number;
  protein: number;
  carbs: number;
  fat: number;
  weight: number;
  item: Array<CurrentMenu>;

  constructor(id: number, name: string) {
    this.id = id;
    this.meal = name;
    this.calories = 0;
    this.protein = 0;
    this.carbs = 0;
    this.fat = 0;

    this.item = new Array<CurrentMenu>();
  }
}
