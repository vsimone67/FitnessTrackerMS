import { Reducer, combineReducers } from 'redux';

import { WorkoutsReducer, BodyInfoReducer } from '../workout/reducers';
import { DietReducer, MetabolicInfoReducer } from '../diet/reducers';

import { AppState } from './';

export const rootReducer: Reducer<AppState> = combineReducers<AppState>({
    workout: WorkoutsReducer,
    diet: DietReducer,
    bodyInfo: BodyInfoReducer,
    metabolicInfo: MetabolicInfoReducer
});

