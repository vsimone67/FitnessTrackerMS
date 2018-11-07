import { State, Action, StateContext, Selector } from '@ngxs/store';
import { BodyInfoStateModel } from '../models/bodyInfoState.model'
import { GetBodyInfo, SaveBodyInfo } from '../actions/body-info.actions'
import { WorkoutService } from '../services/workout.service'

@State<BodyInfoStateModel>({
    name: 'BodyInfo',
    defaults: {
        bodyInfoItems: []
    }

})

export class BodyInfoState {

    constructor(private _workoutService: WorkoutService) { }

    @Selector()
    static BodyInfoItems(state: BodyInfoStateModel) {
        return state.bodyInfoItems;
    }

    @Action(GetBodyInfo)
    GetBodyInfo({ getState, patchState }: StateContext<BodyInfoStateModel>) {
        const state = getState();

        this._workoutService.getBodyInfo((bodyInfoItems: any[]) => {
            patchState({
                ...state,
                bodyInfoItems: bodyInfoItems
            })

        });
    }

    @Action(SaveBodyInfo)
    saveWorkout({ getState, patchState }: StateContext<BodyInfoStateModel>, bodyInfo: SaveBodyInfo) {
        const state = getState();

        this._workoutService.saveBodyInfo(bodyInfo.bodyInfo, (() => {

            patchState({
                ...state,
                bodyInfoItems: [...state.bodyInfoItems, bodyInfo.bodyInfo]
            })
        }));
    }

}