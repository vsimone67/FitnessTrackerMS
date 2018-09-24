import { NgModule } from '@angular/core';
import { AgGridModule } from 'ag-grid-angular';
import { HttpModule } from '@angular/http';
import {
    LogWorkoutComponent, AddBodyInfoComponent, AddWorkoutComponent,
    AddSetComponent, DeleteExerciseComponent, SetFieldComponent, RepsFieldComponent
} from './components';
import { WorkoutService } from './services/workout.service';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [SharedModule.forRoot(), HttpModule, AgGridModule.withComponents([AddSetComponent,
        DeleteExerciseComponent, SetFieldComponent])],
    exports: [],
    declarations: [LogWorkoutComponent, AddBodyInfoComponent, AddWorkoutComponent, AddSetComponent,
        RepsFieldComponent, DeleteExerciseComponent, SetFieldComponent],
    providers: [WorkoutService],
    entryComponents: [RepsFieldComponent]
})
export class WorkoutModule { }
