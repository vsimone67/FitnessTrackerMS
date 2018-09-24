import { Component } from '@angular/core';
import { AgRendererComponent  } from 'ag-grid-angular';

@Component({
    moduleId: module.id,
    selector: 'editImage-cell',
     template: `<i class="material-icons">add_circle_outline</i>`

})
export class EditImageComponent implements AgRendererComponent {
    cell: any;

    constructor() { }

    agInit(params: any): void {
        this.cell = params.data;
    }

    refresh(params: any): boolean {
      return true;
    }
}
