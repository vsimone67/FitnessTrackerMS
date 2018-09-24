import { WorkoutState, BodyInfoState } from '../workout/models';
import { DietState, MeatabolicInfoState } from '../diet/models';

export interface AppState {
    workout: WorkoutState;
    diet: DietState;
    bodyInfo: BodyInfoState;
    metabolicInfo: MeatabolicInfoState;
}
