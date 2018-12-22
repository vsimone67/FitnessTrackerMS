import { Component } from "@angular/core";
import { AgRendererComponent } from "ag-grid-angular";
import { WorkoutService } from '../../../services/workout.service';
import { RepsName } from '../../../models';
@Component({
  selector: "dropdownset-cell",
  template: `
    <select
      (change)="repsSelected($event.target.selectedIndex)"
      [(ngModel)]="currentSelection"
      materialize="material_select">
      <option *ngFor="let rep of repNames" [ngValue]="rep">{{rep.Name}}</option>
    </select>
  `
})
export class RepsDropDownComponent implements AgRendererComponent {
  private cell: any;
  protected currentSelection: RepsName;

  private repNames: Array<RepsName>;

  constructor(private _workoutService: WorkoutService) {
  }

  agInit(params: any): void {
    this._workoutService.getReps((reps: Array<RepsName>) => {
      this.repNames = reps;
      if (this.cell !== null) {
        this.currentSelection = this.repNames.find(exp => exp.Name === this.cell.Name);
      }
    });
    this.cell = params.data; // hold data of grid row
  }

  repsSelected(item: any) {
    this.currentSelection = this.repNames[item];
    this.cell.RepsNameId = this.currentSelection.RepsNameId;
    this.cell.Name = this.currentSelection.Name;
  }

  refresh(params: any): boolean {
    return true;
  }
}
