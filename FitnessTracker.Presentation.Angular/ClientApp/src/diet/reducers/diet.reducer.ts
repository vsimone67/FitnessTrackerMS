
import { DietState } from '../models';
import { DIETPLANNER_ACTIONS } from '../../shared/actions';
import { FoodInfo } from '../models';

const initialState: DietState = {
  foodItems: new Array<FoodInfo>(), foodItem: new FoodInfo(), event: ''

};

export function DietReducer(state = initialState, action: any): DietState {
  switch (action.type) {
    case DIETPLANNER_ACTIONS.GET_ALL_MENU_ITEMS:

      return {
        foodItems: Object.assign([], action.payload),
        event: action.type
      };
    case DIETPLANNER_ACTIONS.GET_MENU_ITEM:
      return {
        foodItem: Object.assign({}, action.payload),
        event: action.type
      };
    default:
      return state;
  }
};


