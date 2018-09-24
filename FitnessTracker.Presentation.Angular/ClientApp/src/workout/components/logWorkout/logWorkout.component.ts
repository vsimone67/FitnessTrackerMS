import { Component, OnInit, Inject } from '@angular/core';
import * as Redux from 'redux';

import { BaseComponent } from '../../../shared/components';
import { EventService } from '../../../shared/services';
import { WorkoutService } from '../../services/workout.service';
import { AppStore, AppState } from '../../../app';
import { DietPlannerActions, DIETPLANNER_ACTIONS } from '../../../shared/actions';
import { Workout } from '../../models';
import { DropDownModel } from '../../../shared/models';
 
@Component({
    moduleId: module.id,
    templateUrl: 'logWorkout.component.html'
})
export class LogWorkoutComponent extends BaseComponent implements OnInit {

    workouts: Array<Workout>;
    workoutDetail: Workout;
    measure: string = 'Pounds';

    phases: Array<DropDownModel>;
    weightConverter: number;

    constructor( @Inject(AppStore) private _store: Redux.Store<AppState>,
        private _workoutService: WorkoutService, public _eventService: EventService, private _dietPlannerActions: DietPlannerActions) {
        super(_eventService);
        this.workoutDetail = new Workout();

        this.phases = [
            { displayName: 'Week 1', value: 'Week 1' },
            { displayName: 'Week 2', value: 'Week 2' },
            { displayName: 'Week 3 (Increase Weight)', value: 'Week 3' },
            { displayName: 'Week 4', value: 'Week 4' },
            { displayName: 'Week 5 (Increase Weight)', value: 'Week 5' },
            { displayName: 'Week 6', value: 'Week 6' },
            { displayName: 'Week 7 (Increase Weight)', value: 'Week 7' },
            { displayName: 'Week 8', value: 'Week 8' },
            { displayName: 'Week 9 (Increase Weight)', value: 'Week 9' },
            { displayName: 'Week 10', value: 'Week 10' },
            { displayName: 'Week 11 (Increase Weight)', value: 'Week 11' },
            { displayName: 'Week 12', value: 'Week 12' }

        ];

        _store.subscribe(() => this.updateState());
    }

    ngOnInit() {
        this._workoutService.getWorkouts((workouts: any) => {
            this._dietPlannerActions.updateStore<Array<Workout>>(workouts, DIETPLANNER_ACTIONS.GET_ALL_WORKOUTS);
        });

    }

    workoutSelected(item: Workout) {
        this._workoutService.getWorkout(item.WorkoutId, (workout: Workout) => {
            this.CheckReps(workout);
            this._dietPlannerActions.updateStore<Workout>(workout, DIETPLANNER_ACTIONS.GET_WORKOUT);
        });

    }
    phaseSelected(item: any) {
        this.workoutDetail.Phase = item.value;
    }
    selectMeasure() {
        if (this.measure === 'Pounds') {
            this.measure = 'Kilo';
            this.weightConverter = 0.453592;
        } else {
            this.measure = 'Pounds';
            this.weightConverter = 2.2046;
        };

        this.convertToMeasure();

    }
    convertToMeasure() {
        this.workoutDetail.Set.forEach(
            set => set.Exercise.forEach(
                exercise => exercise.Reps.forEach(
                    rep => rep.Weight = Math.round((rep.Weight * this.weightConverter)
                    ))));
    }

    onSubmit() {
        this._workoutService.saveDailyWorkout(this.workoutDetail, () => this.showMessage('Workout Saved'));
    }
    updateState() {
        let state = this._store.getState();

        if (state.workout.event === DIETPLANNER_ACTIONS.GET_ALL_WORKOUTS) {
            this.workouts = state.workout.workouts;
        } else if (state.workout.event === DIETPLANNER_ACTIONS.GET_WORKOUT) {
            this.workoutDetail = state.workout.currentWorkout;
        }

    }
    CheckReps(workout: Workout) {
        let maxReps = this.FindMaxReps(workout);

        workout.Set.forEach(
            set => set.Exercise.forEach(
                exercise => {
                    exercise.AdditionalColumns = new Array<string>();
                    if (maxReps > exercise.Reps.length) {
                        for (var _i = 0; _i < maxReps - exercise.Reps.length; _i++) {

                            exercise.AdditionalColumns.push('1');
                        }
                    }
                }
            ));

    }
    FindMaxReps(workout: Workout) {

        let maxReps = 0;
        workout.Set.forEach(
            set => set.Exercise.forEach(
                exercise => maxReps = Math.max(exercise.Reps.length, maxReps)));

        return maxReps;

    }

}


