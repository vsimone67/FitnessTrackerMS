import { Component } from '@angular/core';
import { AgRendererComponent  } from 'ag-grid-angular';

@Component({    
    selector: 'selector',
    template: `<i class="material-icons">delete</i>`
})
export class DeleteImageComponent implements AgRendererComponent {
    cell: any;
    constructor() { }

    agInit(params: any): void {
        this.cell = params.data;
    }

    refresh(params: any): boolean {
      return true;
    }
}
