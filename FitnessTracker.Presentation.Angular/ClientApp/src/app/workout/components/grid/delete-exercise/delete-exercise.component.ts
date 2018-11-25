import { Component } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';

@Component({
  selector: 'deleteexercise-cell',
  template: ` <a class="mdl-button mdl-js-button  mdl-js-ripple-effect mdl-button--mini-fab
     mdl-button--colored align-right" id="deleteexercise">
                                <i class="material-icons">delete</i>
                            </a>`
})
export class DeleteExerciseComponent implements AgRendererComponent {
  private cell: any;
  private params: any;

  agInit(params: any): void {
    this.cell = params.data;
    this.params = params;
  }

  refresh(params: any): boolean {
    return true;
  }
}
