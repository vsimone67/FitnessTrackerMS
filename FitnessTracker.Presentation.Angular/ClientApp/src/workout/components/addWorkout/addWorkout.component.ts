import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { GridOptions } from 'ag-grid/main';

import { AddSetComponent, DeleteExerciseComponent, SetFieldComponent } from '../../components';
import { BaseComponent } from '../../../shared/components';
import { EventService } from '../../../shared/services';
import { WorkoutService } from '../../services/workout.service';
import { Workout } from '../../models';
import { events } from '../../../shared/models';

@Component({
    moduleId: module.id,
    selector: 'addWorkout',
    templateUrl: 'addWorkout.component.html'
})
export class AddWorkoutComponent extends BaseComponent implements OnInit {
    @ViewChild(AddSetComponent) addSet: AddSetComponent;
    workout: Workout;
    gridOptions: GridOptions;

    constructor(private _workoutService: WorkoutService, public _eventService: EventService,
        private _el: ElementRef) {
        super(_eventService);

        this.workout = new Workout();
        // get event from addset that the aet has been sucessfully added
        _eventService.getEvent(events.addSetEvent).subscribe(
            setObj => {
                this.onSetAdded();
            });

        this.setGridOptions();
    }

    ngOnInit() { }
    onSetAdded() {
        // ag-grid does not do a good job of dynamic adding so you have to re-popuplate the rowdata when updated
        this.gridOptions.api.setRowData(this.workout.Set);
    }
    setGridOptions() {
        this.gridOptions = <GridOptions>{};
        this.gridOptions.columnDefs = this.createColumnDefs();
        this.gridOptions.rowData = this.workout.Set;
        this.gridOptions.rowHeight = 30;
    }
    createColumnDefs() {
        return [
            { headerName: 'Set', field: 'Name', width: 215 },
            {
                headerName: 'Exercise', field: 'Exercise',
                cellRendererFramework: SetFieldComponent, width: 610
            },
            {
                headerName: 'Remove', field: 'Exercise',
                cellRendererFramework: DeleteExerciseComponent, width: 90
            }

        ];
    }

    onAddSet() {
        this.addSet.showDialog();
    }
    onCellClicked($event: any) {

        if ($event.colDef.headerName === 'Remove') {
            this.workout.Set.splice($event.rowIndex, 1);
            this.gridOptions.api.setRowData(this.workout.Set);
        }

    }
    saveWorkout() {
        this._workoutService.saveWorkout(this.workout, () => this.showMessage('Workout Saved'));
    }
}
