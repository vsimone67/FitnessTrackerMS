
import { WorkoutState } from '../models';
import { DIETPLANNER_ACTIONS } from '../../shared/actions';
import { Workout } from '../models';

const initialState: WorkoutState = {
  workouts: new Array<Workout>(), currentWorkout: new Workout(), event: ''

};

export function WorkoutsReducer(state = initialState, action: any): WorkoutState {
  switch (action.type) {
    case DIETPLANNER_ACTIONS.GET_ALL_WORKOUTS:

      return {
        workouts: Object.assign([], action.payload),
        event: action.type
      };
    case DIETPLANNER_ACTIONS.GET_WORKOUT:
      return {
        currentWorkout: Object.assign({}, action.payload),
        event: action.type
      };
    default:
      return state;
  }
};
