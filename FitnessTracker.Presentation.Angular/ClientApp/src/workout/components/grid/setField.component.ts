import { Component} from '@angular/core';
import {AgRendererComponent} from 'ag-grid-angular';
import { Exercise } from '../../models';

@Component({
    selector: 'repWorkout-cell',
    template: `<span *ngFor="let exercise of Exercise">
               {{exercise.Name}}:               
               <b *ngFor="let rep of exercise.Reps">{{rep.Name}} ({{rep.TimeToNextExercise}})..</b></span>`
})
export class SetFieldComponent implements AgRendererComponent {
    private Exercise: Exercise; 

    agInit(params: any): void {
        this.Exercise = params.data.Exercise;
    }

    refresh(params: any): boolean {
      return true;
    }
}
