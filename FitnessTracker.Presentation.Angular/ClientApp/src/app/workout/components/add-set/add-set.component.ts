
import { Component, OnInit, Input, ElementRef } from '@angular/core';
import { GridOptions } from 'ag-grid-community';

import { EventService } from '../../../shared/services';
import { WorkoutService } from '../../services/workout.service';
import { BaseComponent } from '../../../shared/components';
import { RepsFieldComponent, DeleteExerciseComponent } from '../../components';
import { Workout, set, ExerciseName, RepsName, Exercise, Reps, SetName } from '../../models';
import { DropDownModel, events } from '../../../shared/models';

@Component({    
    selector: 'addSet',
    templateUrl: 'add-set.component.html'
})
export class AddSetComponent extends BaseComponent implements OnInit {
    @Input() workout: Workout;
    private _set: set;
    dialog: any;
    exercises: Array<ExerciseName>;
    measure: Array<DropDownModel>;
    private selectedMeasure: DropDownModel;
    private selectedExercise: Exercise;
    private selectedSet: set;
    private reps: Array<Reps>;
    private sets: Array<SetName>;
    private repNames: Array<RepsName>;
    timeToNextExercise: string;
    selectedRep: RepsName;
    gridOptions: GridOptions;

    constructor(private _el: ElementRef, private _workoutService: WorkoutService, public _eventService: EventService) {
        super(_eventService);
        this.sets = new Array<SetName>();
        this.repNames = new Array<RepsName>();

        this.measure = [
            { displayName: 'W', value: 'W' },
            { displayName: 'R', value: 'R' },
            { displayName: 'RT: W', value: 'RT: W' },
            { displayName: 'LT: W', value: 'LT: W' },
            { displayName: 'RT: R', value: 'RT: R' },
            { displayName: 'LT: R', value: 'LT: R' },
            { displayName: 'Sec', value: 'Sec' },
            { displayName: 'C', value: 'C' }
        ];

    }

    ngOnInit() {
        this._set = new set();
        this.InitExercise();
        this.setGridOptions();

        // TODO: Add to NGSX store
        this._workoutService.getExercises((exercises: Array<ExerciseName>) => { this.exercises = exercises; });
        this._workoutService.getSets((sets: Array<SetName>) => { this.sets = sets; });
        this._workoutService.getReps((reps: Array<RepsName>) => { this.repNames = reps; });
    }
    setGridOptions() {
        this.gridOptions = <GridOptions>{};
        this.gridOptions.columnDefs = this.createColumnDefs();
        this.gridOptions.rowData = this._set.Exercise;
        this.gridOptions.rowHeight = 30;
    }
    createColumnDefs() {
        return [
            { headerName: 'Exercise', field: 'Name', width: 215 },
            { headerName: 'Measure', field: 'Measure', width: 70 },
            {
                headerName: 'Reps', field: 'Reps',
                cellRendererFramework: RepsFieldComponent, width: 610
            },
            {
                headerName: 'Remove', field: 'Reps',
                cellRendererFramework: DeleteExerciseComponent, width: 90
            }

        ];
    }

    InitExercise() {
        this.selectedExercise = new Exercise();
        this.reps = new Array<Reps>();
        this.selectedRep = new RepsName();
        this.selectedMeasure = new DropDownModel('', '');
        this.timeToNextExercise = '';
    }
    showDialog() {
        this._set = new set();
        this.selectedSet = new set();
        this.InitExercise();
        this.gridOptions.api.setRowData(this._set.Exercise);
        this.dialog = this._el.nativeElement.querySelector('dialog');

        if (!this.dialog.showModal) {
            this.dialog.dialogPolyfill.registerDialog(this.dialog);
        }

        this.dialog.showModal();
    }
    onAddExercise() {

        if (this.reps.length > 0) {
            this.onSaveRep();
            this.selectedExercise.Measure = this.selectedMeasure.value;
            this.selectedExercise.Reps = this.reps;
            this.selectedExercise.ExerciseOrder = this._set.Exercise.length + 1
            this._set.Exercise.push(this.selectedExercise);
        }
        this.InitExercise();
        this.gridOptions.api.setRowData(this._set.Exercise);

    }
    onCellClicked($event: any) {

        if ($event.colDef.headerName === 'Remove') {
            this._set.Exercise.splice($event.rowIndex, 1);
            this.gridOptions.api.setRowData(this._set.Exercise);
        }

    }
    onAddReps() {

        if (this.reps.length > 0) {
            this.onSaveRep();
        }
        this.reps.push(new Reps());
    }
    onSaveRep() {
        this.reps[this.reps.length - 1].TimeToNextExercise = this.timeToNextExercise;
        this.reps[this.reps.length - 1].RepsNameId = this.selectedRep.RepsNameId;
        this.reps[this.reps.length - 1].Name = this.selectedRep.Name;
    }
    onSaveSet() {
        this.onSaveRep();
        this.onAddExercise();
        this._set.SetOrder = this.workout.Set.length + 1;
        this.workout.Set.push(this._set);
        this.onClose();        
        this._eventService.sendEvent(events.addSetEvent, this._set);

    }
    onClose() {
        this.dialog.close();
    }
    exerciseSelected(item: any) {
        this.selectedExercise.ExerciseOrder = 1;
        this.selectedExercise.Name = item.Name;
        this.selectedExercise.ExerciseNameId = item.ExerciseNameId;
    }
    setSelected(item: any) {
        this._set.Name = item.Name;
        this._set.SetNameId = item.SetNameId;
        this.selectedSet = item;
    }
    measureSelected(item: any) {
        this.selectedExercise.Measure = item.value;
        this.selectedMeasure = item;
    }
    repSelected(item: any) {
        this.selectedRep = item;
    }

}