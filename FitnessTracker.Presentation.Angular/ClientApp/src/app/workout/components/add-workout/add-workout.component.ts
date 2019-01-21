import { Component, OnInit, ElementRef, ViewChild } from "@angular/core";
import { GridOptions } from "ag-grid-community";
import { DeleteExerciseComponent, SetFieldComponent, EditExerciseComponent} from "../../components";
import { AddSetComponent } from "../add-set/add-set.component";
import { BaseComponent } from "../../../shared/components";
import { EventService } from "../../../shared/services";
import { WorkoutService } from "../../services/workout.service";
import { Workout } from "../../models";
import { events } from "../../../shared/models";

@Component({
  selector: "addWorkout",
  templateUrl: "add-workout.component.html"
})
export class AddWorkoutComponent extends BaseComponent implements OnInit {
  @ViewChild(AddSetComponent) addSet: AddSetComponent;
  workout: Workout;
  workouts: Array<Workout>;
  isEdit: boolean;
  gridOptions: GridOptions;

  constructor(
    private _workoutService: WorkoutService,
    public _eventService: EventService,
    private _el: ElementRef
  ) {
    super(_eventService);

    this.workout = new Workout();
    // get event from addset that the aet has been successfully added
    _eventService.getEvent(events.addSetEvent).subscribe(() => {
      this.onSetAdded();
    });

    this.setGridOptions();
    this.isEdit = false;
  }

  ngOnInit() {
    this._workoutService.getWorkouts((workouts: Array<Workout>) => {
      this.workouts = workouts;
    });
  }
  onSetAdded() {
    // ag-grid does not do a good job of dynamic adding so you have to re-populate the row data when updated
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
      { headerName: "Set", field: "Name", width: 215 },
      { headerName: "Order", field: "SetOrder", width: 100, editable: true },      
      {
        headerName: "Exercise",
        field: "Exercise",
        cellRendererFramework: SetFieldComponent,
        width: 610
      },
      {
        headerName: "Edit",
        field: "Exercise",
        cellRendererFramework: EditExerciseComponent,
        width: 90
      },
      {
        headerName: "Remove",
        field: "Exercise",
        cellRendererFramework: DeleteExerciseComponent,
        width: 90
      }
    ];
  }

  onAddSet() {
    this.addSet.showDialog();
  }
  onCellClicked($event: any) {
    if ($event.colDef.headerName === "Remove") {
      this.workout.Set.splice($event.rowIndex, 1);
      this.gridOptions.api.setRowData(this.workout.Set);
    } else if ($event.colDef.headerName === "Edit") {
      this.addSet.showDialogForEdit($event.data);
    }
  }
  workoutSelectedForEdit(workout: Workout) {
    this._workoutService.getWorkout(workout.WorkoutId, (workout: Workout) => {
      this.workout = workout;
      this.isEdit = true;
      this.gridOptions.api.setRowData(workout.Set);
    });
  }
  saveWorkout() {
    if (!this.isEdit) {
      this._workoutService.saveWorkout(this.workout, () =>
        this.showMessage("Workout Saved")
      );
    } else {
      this._workoutService.updateWorkout(this.workout, () =>
        this.showMessage("Workout Updated")
      );
    }

    this.isEdit = false;
  }
}
