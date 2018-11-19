import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import { BaseService, EventService } from "../../shared/services";
import { NutritionInfo, Columns, FoodInfo, CurrentMenu } from "../models";

import { AppsettingsService } from "../../shared/services";
@Injectable({
  providedIn: "root"
})
export class DietService extends BaseService {
  protected nutritionInfo: Array<NutritionInfo>;
  protected serviceURL: string;

  constructor(
    http: Http,
    eventService: EventService,
    appSettings: AppsettingsService
  ) {
    super(http, eventService, appSettings);

    this.nutritionInfo = new Array<NutritionInfo>();
    this.isSettingsLoaded = false;
  }

  async getDietItems(callback: any) {
    await this.loadConfiguration("dietServiceURL");
    return this.getDataWithSpinner(
      this.serviceURL + "GetSavedMenuItems",
      callback
    );
  }
  async getColumns(callback: any) {
    await this.loadConfiguration("dietServiceURL");
    return this.getDataWithSpinner(this.serviceURL + "GetColumns", callback);
  }
  async saveMenu(payload: any, callback: any) {
    await this.loadConfiguration("dietServiceURL");
    return this.putDataWithSpinner(
      this.serviceURL + "SaveMenu",
      payload,
      callback
    );
  }
  async getMetabolicInfo(callback: any) {
    await this.loadConfiguration("dietServiceURL");
    return this.getDataWithSpinner(
      this.serviceURL + "GetMetabolicInfo",
      callback
    );
  }
  async saveMetabolicInfo(payload: any, callback: any) {
    await this.loadConfiguration("dietServiceURL");
    return this.putDataWithSpinner(
      this.serviceURL + "EditMetabolicInfo",
      payload,
      callback
    );
  }
  async getcurrentMacro(mode: string, callback: any) {
    await this.loadConfiguration("dietServiceURL");
    return this.getDataWithSpinner(
      this.serviceURL + "GetMetabolicInfoCalcType/" + mode,
      callback
    );
  }
  async processItem(payload: any, callback: any) {
    await this.loadConfiguration("dietServiceURL");
    return this.putDataWithSpinner(
      this.serviceURL + "ProcessItem",
      payload,
      callback
    );
  }
  async deleteItem(payload: any, callback: any) {
    await this.loadConfiguration("dietServiceURL");
    return this.putDataWithSpinner(
      this.serviceURL + "DeleteFoodItem",
      payload,
      callback
    );
  }

  makeSavedMenu(
    columns: Array<Columns>,
    foodList: Array<FoodInfo>
  ): Array<NutritionInfo> {
    let meals = new Array<NutritionInfo>();
    columns.forEach(column => {
      meals.push(new NutritionInfo(column.MealId, column.MealDisplayName));
    });
    meals.push(new NutritionInfo(0, "Max Macro"));
    meals.push(new NutritionInfo(0, "Totals"));
    meals.push(new NutritionInfo(0, "Remaining"));

    foodList.forEach(food => {
      food.SavedMenu.forEach(menuItem => {
        let selectedMeal = meals.find(meal => meal.id === menuItem.MealId);

        let totals = meals.find(meal => meal.meal === "Totals");
        let remaining = meals.find(meal => meal.meal === "Remaining");
        let maxMacro = meals.find(meal => meal.meal === "Max Macro");

        let value = menuItem.Serving;

        selectedMeal.item.push(
          new CurrentMenu(
            menuItem.ItemId.toString(),
            menuItem.ItemId,
            menuItem.Serving,
            food.ServingSize,
            food.Item
          )
        );

        selectedMeal.calories += Math.round(food.Calories * value);
        selectedMeal.carbs += Math.round(food.Carbs * value);
        selectedMeal.protein += Math.round(food.Protien * value);
        selectedMeal.fat += Math.round(food.Fat * value);

        totals.carbs += Math.round(food.Carbs * value);
        totals.protein += Math.round(food.Protien * value);
        totals.fat += Math.round(food.Fat * value);
        totals.calories = Math.round(
          totals.carbs * 4 + totals.protein * 4 + totals.fat * 9
        ); // recalc calories via macro and not what the foodl says

        remaining.calories = Math.round(maxMacro.calories - totals.calories);
        remaining.carbs -= Math.round(food.Carbs * value);
        remaining.protein -= Math.round(food.Protien * value);
        remaining.fat -= Math.round(food.Fat * value);
      });
    });

    return meals;
  }
}
