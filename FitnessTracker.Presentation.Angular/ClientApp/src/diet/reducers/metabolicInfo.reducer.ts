
import { BodyInfoState } from '../../workout/models';
import { DIETPLANNER_ACTIONS } from '../../shared/actions';

import { MeatabolicInfoState, MeatabolicInfo } from '../models';

const initialState: MeatabolicInfoState = {
    metabolicInfo: new Array<MeatabolicInfo>(), event: ''
};

export function MetabolicInfoReducer(state = initialState, action: any): MeatabolicInfoState {
    switch (action.type) {
        case DIETPLANNER_ACTIONS.GET_METABOLICINFO:

            return {
                metabolicInfo: Object.assign([], action.payload),
                event: action.type
            };
        default:
            return state;
    }
};


