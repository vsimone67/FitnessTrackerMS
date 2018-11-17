import { State, Action, StateContext, Selector } from "@ngxs/store";
import { DietStateModel } from "../models/diet-state.model";
import {
  GetAllMenuItems,
  AddMenuItem,
  DeleteMenuItem,
  SaveMenu,
  GetColumns,
  SetMeals
} from "../actions/diet.actions";
import { DietService } from "../service/diet.service";
import { FoodInfo, Columns, NutritionInfo } from "../models";

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

    console.info("Get All Menu Items");

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

    console.info("Get Columns");
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

    console.info("Setting Meals.... " + mealsPayload.meals.length);
    ctx.patchState({
      ...state,
      nutritionInfo: mealsPayload.meals
    });
  }
}
