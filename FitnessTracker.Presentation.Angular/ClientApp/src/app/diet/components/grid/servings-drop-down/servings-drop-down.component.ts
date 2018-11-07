import { Component } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';
import { SavedMenu } from '../../../models';

@Component({    
    selector: 'dropdown-cell',
    template: ` <select (change)="servingSelected($event.target.selectedIndex)" 
                    [(ngModel)]="currentSelection" materialize="material_select">
                     <option *ngFor="let serving of servings" [value]="serving">{{serving}}</option>
                 </select>
`
})
export class ServingDropDownComponent implements AgRendererComponent {

    private cell: any;
    private currentSelection: string;
    private cellName: string;

    private servings: Array<string> = ['0.5', '1', '1.5', '2', '2.5', '1', '1.5', '2', '2.5',
        '3', '3.5', '4', '4.5', '5', '5.5', '6', '6.5', '7', '7.5', '8'];

    constructor() {

        this.currentSelection = this.servings[1];
    }

    agInit(params: any): void {
        this.cell = params.data; // hold data of grid row
        this.cellName = params.colDef.field;
        let foodDefault = new Array<SavedMenu>();
        foodDefault = this.cell.SavedMenu;

        if (foodDefault.length > 0) {
            let food = foodDefault[0];
            if (food != null) {
                let item = this.servings.find(item => item === food.Serving.toString());

                if (item != null) {
                    let pos = this.servings.indexOf(item)
                    this.currentSelection = this.servings[pos];
                    this.cell.Serving = Number(this.servings[pos]);
                }

            }
        }
    }

    servingSelected(item: any) {
        this.cell.Serving = Number(this.servings[item]);
    }

    refresh(params: any): boolean {
      return true;
    }
}
