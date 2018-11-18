import { State, Action, StateContext, Selector } from "@ngxs/store";
import { DietStateModel } from "../models/diet-state.model";
import {
  GetAllMenuItems,
  AddMenuItem,
  DeleteMenuItem,
  SaveMenu,
  GetColumns,
  SetMeals,
  CreateMenu
} from "../actions/diet.actions";
import { DietService } from "../service/diet.service";
import { FoodInfo, Columns, NutritionInfo, CurrentMenu } from "../models";
import { noUndefined } from "@angular/compiler/src/util";

@State<DietStateModel>({
  name: "diet",
  defaults: {
    foodItems: [],
    foodItem: new FoodInfo(),
    columns: new Array<Columns>(),
    nutritionInfo: new Array<NutritionInfo>()
  }
})
export class DietState {
  constructor(private _dietService: DietService) {}

  @Selector()
  static foodItems(state: DietStateModel) {
    return state.foodItems;
  }

  @Selector()
  static foodItem(state: DietStateModel) {
    return state.foodItem;
  }

  @Selector()
  static columns(state: DietStateModel) {
    return state.columns;
  }

  @Selector()
  static meals(state: DietStateModel) {
    return state.nutritionInfo;
  }

  @Action(GetAllMenuItems)
  getAllMenuItems(ctx: StateContext<DietStateModel>) {
    const state = ctx.getState();

    this._dietService.getDietItems((dietItems: any) => {
      ctx.patchState({
        ...state,
        foodItems: dietItems
      });
    });
  }

  @Action(AddMenuItem)
  addMenuItem(ctx: StateContext<DietStateModel>, menuItem: AddMenuItem) {
    const state = ctx.getState();

    console.info("Add Menu Item");
    this._dietService.processItem(menuItem.foodInfo, () => {
      ctx.patchState({
        ...state,
        foodItems: [...state.foodItems, menuItem.foodInfo]
      });
    });
  }

  @Action(DeleteMenuItem)
  deleteMenuItem(ctx: StateContext<DietStateModel>, menuItem: AddMenuItem) {
    const state = ctx.getState();

    state.foodItems.splice(
      state.foodItems.findIndex(exp => exp.ItemId === menuItem.foodInfo.ItemId),
      1
    );
    this._dietService.deleteItem(menuItem.foodInfo, () => {
      ctx.patchState({
        ...state,
        foodItems: [...state.foodItems]
      });
    });
  }

  @Action(SaveMenu)
  saveMenu(ctx: StateContext<DietStateModel>, mealsPayload: SaveMenu) {
    const state = ctx.getState();

    this._dietService.saveMenu(mealsPayload.meals, () => {
      ctx.patchState({
        ...state,
        nutritionInfo: mealsPayload.meals
      });
    });
  }

  @Action(GetColumns)
  getColumns(ctx: StateContext<DietStateModel>) {
    const state = ctx.getState();

    this._dietService.getColumns((columns: Array<Columns>) => {
      ctx.patchState({
        ...state,
        columns: columns
      });

      ctx.dispatch(new GetAllMenuItems());
    });
  }

  @Action(SetMeals)
  setMeals(ctx: StateContext<DietStateModel>, mealsPayload: SetMeals) {
    const state = ctx.getState();

    ctx.patchState({
      ...state,
      nutritionInfo: mealsPayload.meals
    });
  }

  @Action(CreateMenu)
  createMenu(ctx: StateContext<DietStateModel>, foodListPayload: CreateMenu) {
    const state = ctx.getState();

    let meals = new Array<NutritionInfo>();
    foodListPayload.columns.forEach(column => {
      meals.push(new NutritionInfo(column.MealId, column.MealDisplayName));
    });
    meals.push(new NutritionInfo(0, "Max Macro"));
    meals.push(new NutritionInfo(0, "Totals"));
    meals.push(new NutritionInfo(0, "Remaining"));

    foodListPayload.foodInfiList.forEach(food => {
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

    ctx.patchState({
      ...state,
      nutritionInfo: meals
    });

    ctx.dispatch(new SetMeals(meals));
  }
}
