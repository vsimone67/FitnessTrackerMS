
import { BodyInfoState } from '../models';
import { DIETPLANNER_ACTIONS } from '../../shared/actions';
import { BodyInfo } from '../models';

const initialState: BodyInfoState = {
  bodyInfoItems: new Array<BodyInfo>(), event: ''
};

export function BodyInfoReducer(state = initialState, action: any): BodyInfoState {
  switch (action.type) {
    case DIETPLANNER_ACTIONS.GET_BODY_INFO:

      return {
        bodyInfoItems: Object.assign([], action.payload),
        event: action.type
      };
    default:
      return state;
  }
};


