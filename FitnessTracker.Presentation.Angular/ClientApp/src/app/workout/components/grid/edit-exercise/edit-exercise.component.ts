import { Component } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';

@Component({
  selector: 'editexercise-cell',
  template: ` <a class="mdl-button mdl-js-button  mdl-js-ripple-effect mdl-button--mini-fab
     mdl-button--colored align-right" id="editexercise">
                                <i class="material-icons">edit</i>
                            </a>`
})
export class EditExerciseComponent implements AgRendererComponent {
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
