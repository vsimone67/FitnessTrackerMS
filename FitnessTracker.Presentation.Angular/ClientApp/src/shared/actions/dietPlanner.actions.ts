import { Inject, Injectable } from '@angular/core';
import * as Redux from 'redux';

import { AppState, AppStore } from '../../app';

@Injectable()
export class DietPlannerActions {
    constructor( @Inject(AppStore) private _store: Redux.Store<AppState>) { }

    public updateStore<T>(payload: T, event: string) {
        this._store.dispatch({ type: event, payload: payload });
    }
}
