import { State, Action, StateContext, Selector } from "@ngxs/store";
import { DietStateModel } from "../models/diet-state.model";
import {
  GetAllMenuItems,
  AddMenuItem,
  DeleteMenuItem
} from "../actions/diet.actions";
import { DietService } from "../service/diet.service";
import { FoodInfo } from "../models";

@State<DietStateModel>({
  name: "diet",
  defaults: {
    foodItems: [],
    foodItem: new FoodInfo()
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

  @Action(GetAllMenuItems)
  getAllMenuItems({ getState, patchState }: StateContext<DietStateModel>) {
    const state = getState();

    this._dietService.getDietItems((dietItems: any) => {
      patchState({
        ...state,
        foodItems: dietItems
      });
    });
  }

  @Action(AddMenuItem)
  addMenuItem(
    { getState, patchState }: StateContext<DietStateModel>,
    menuItem: AddMenuItem
  ) {
    const state = getState();

    this._dietService.processItem(menuItem.foodInfo, () => {
      patchState({
        ...state,
        foodItems: [...state.foodItems, menuItem.foodInfo]
      });
    });
  }

  @Action(DeleteMenuItem)
  deleteMenuItem(
    { getState, patchState }: StateContext<DietStateModel>,
    menuItem: DeleteMenuItem
  ) {
    const state = getState();

    state.foodItems.splice(
      state.foodItems.findIndex(exp => exp.ItemId === menuItem.foodInfo.ItemId),
      1
    );

    this._dietService.deleteItem(menuItem.foodInfo, () => {
      patchState({
        ...state,
        foodItems: [...state.foodItems]
      });
    });
  }
}
