 
import { FoodInfo } from './';

export interface DietState {
    event: string;
    foodItems?: Array<FoodInfo>;
    foodItem?: FoodInfo;
}
