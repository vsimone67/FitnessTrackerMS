import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogWorkoutComponent } from './components/log-workout/log-workout.component';
import { AddBodyInfoComponent } from './components/add-body-info/add-body-info.component';
import { AddWorkoutComponent } from './components/add-workout/add-workout.component';
import { DeleteExerciseComponent } from './components/grid/delete-exercise/delete-exercise.component';
import { RepsFieldComponent } from './components/grid/reps-field/reps-field.component';
import { SetFieldComponent } from './components/grid/set-field/set-field.component';
import { WorkoutService } from '../workout/services/workout.service'
import { NgxsModule } from '@ngxs/store';
import { WorkoutState } from '../workout/state/workout.state'
import { SharedModule } from '../shared/shared.module';
import { AgGridModule } from 'ag-grid-angular';
import { HttpModule } from '@angular/http';
import { AddSetComponent } from './components/add-set/add-set.component'
import { BodyInfoState } from '../workout/state/body-info.state'
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { EditExerciseComponent } from '../workout/components/grid/edit-exercise/edit-exercise.component';
import { RepsDropDownComponent } from '../workout/components/grid/reps-drop-down/reps-drop-down.component';
import { DeleteImageComponent } from '../shared/components/delete-image/delete-image.component'
@NgModule({
  imports: [CommonModule, SharedModule.forRoot(), HttpModule, HttpClientModule, FormsModule, AgGridModule.withComponents([AddSetComponent,
    DeleteExerciseComponent, EditExerciseComponent, RepsDropDownComponent, SetFieldComponent, NgxsModule.forRoot([WorkoutState, BodyInfoState])])],
  exports: [AddSetComponent, CommonModule],
  declarations: [LogWorkoutComponent, AddBodyInfoComponent, AddWorkoutComponent, AddSetComponent,
    RepsFieldComponent, DeleteExerciseComponent, SetFieldComponent, AddSetComponent, EditExerciseComponent, RepsDropDownComponent],
  providers: [WorkoutService],
  entryComponents: [RepsFieldComponent, DeleteImageComponent]
})

export class WorkoutModule { }
