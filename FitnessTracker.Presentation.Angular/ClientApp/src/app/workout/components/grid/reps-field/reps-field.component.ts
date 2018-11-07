import { Component } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';
import { Reps } from '../../../models'

@Component({
    selector: 'rep-cell',
    template: `<span *ngFor="let rep of reps">{{rep.Name}} ({{rep.TimeToNextExercise}})</span>`
})
export class RepsFieldComponent implements AgRendererComponent {
    private reps: Reps;

    agInit(params: any): void {
        this.reps = params.data.Reps;
    }

    refresh(params: any): boolean {
      return true;
    }
}
