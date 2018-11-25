import { Component, OnInit } from '@angular/core';
import { Store, Select } from '@ngxs/store';
import { BaseComponent } from '../../../shared/components';
import { EventService } from '../../../shared/services';
import { Workout } from '../../models';
import { DropDownModel } from '../../../shared/models';
import { GetAllWorkouts, GetWorkout, SaveWorkout } from '../../actions/workout.actions'
import { WorkoutState } from '../../state/workout.state'
import { Observable } from 'rxjs/observable'

@Component({
  templateUrl: './log-workout.component.html'
})

export class LogWorkoutComponent extends BaseComponent implements OnInit {
  phases: Array<DropDownModel>;
  currentWorkout: Workout;

  @Select(WorkoutState.CurrentWorkout) workoutDetail$: Observable<Workout>;
  @Select(WorkoutState.workouts) workouts$: Observable<Array<Workout>>;

  constructor(public store: Store, public _eventService: EventService) {
    super(_eventService);

    this.currentWorkout = new Workout();
  }

  ngOnInit() {
    this.store.dispatch(new GetAllWorkouts());
  }

  workoutSelected(item: Workout) {
    this.store.dispatch(new GetWorkout(item.WorkoutId)).subscribe(() => this.workoutDetail$.subscribe(workout => { this.currentWorkout = workout; this.CheckReps(this.currentWorkout); }));
  }

  onSubmit() {
    this.store.dispatch(new SaveWorkout(this.currentWorkout)).subscribe(() => this.showMessage("Workout Saved"));
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
