import { Component } from '@angular/core';
import { AgRendererComponent } from 'ag-grid-angular';
import { NutritionInfo, CurrentMenu, SavedMenu } from '../../../models'
import { DietService } from '../../../service/diet.service'

@Component({    
    selector: 'checkbox-cell',
    template: `<input type="checkbox" (click)="checkboxClick($event)"  [(ngModel)]="isChecked" /> `
})
export class MealCheckBoxComponent implements AgRendererComponent {

    private cellName: string;
    private cell: any;
    private isChecked: boolean;
    private field: string;
    private mealid: string;
    constructor(private _dietService: DietService) { this.isChecked = false; }

    agInit(params: any): void {
        this.cell = params.data;
        this.cellName = params.colDef.field;
        this.field = params.colDef.headerName;
        
        let foodDefault = new Array<SavedMenu>();
        foodDefault = this.cell.SavedMenu;

        if (foodDefault.find(exp => exp.MealId === Number(this.cellName)) != null) {
            this.isChecked = true;
            this.addMacroData(true, params.colDef.headerName);
        }

    }
    checkboxClick(event: any) {
        this.addMacroData(event.target.checked, this.field);
    }
    addMacroData(isChecked: boolean, mealName: string) {
        let nutritionInfo: Array<NutritionInfo> = this._dietService.getNutritionInfo();

        let selectedMeal = nutritionInfo.find((meal) => meal.meal === mealName);

        if (isChecked !== undefined) {

            if (selectedMeal != null) {

                let foodItem = selectedMeal.item.find(item => item.ItemID === this.cell.ItemId);
                let totals = nutritionInfo.find((meal) => meal.meal === 'Totals');
                let remaining = nutritionInfo.find((meal) => meal.meal === 'Remaining');
                let maxMacro = nutritionInfo.find((meal) => meal.meal === 'Max Macro');
                let value = this.cell.Serving;
                
                if (!isChecked) {
                    value *= -1;

                    if (foodItem != null) {
                        selectedMeal.item.splice(selectedMeal.item.indexOf(foodItem, 0), 1);
                    }
                } else {
                    selectedMeal.item.push(new CurrentMenu(this.cell.ItemId.toString(),
                        this.cell.ItemId, value, this.cell.ServingSize, this.cell.Item));
                }

                selectedMeal.calories += Math.round(this.cell.Calories * value);
                selectedMeal.carbs += Math.round(this.cell.Carbs * value);
                selectedMeal.protein += Math.round(this.cell.Protien * value);
                selectedMeal.fat += Math.round(this.cell.Fat * value);

                totals.carbs += Math.round(this.cell.Carbs * value);
                totals.protein += Math.round(this.cell.Protien * value);
                totals.fat += Math.round(this.cell.Fat * value);
                totals.calories = Math.round((totals.carbs * 4) + (totals.protein * 4) + (totals.fat * 9)); // recalc calories via macro and not what the foodl says

                remaining.calories = Math.round(maxMacro.calories - totals.calories);
                remaining.carbs -= Math.round(this.cell.Carbs * value);
                remaining.protein -= Math.round(this.cell.Protien * value);
                remaining.fat -= Math.round(this.cell.Fat * value);


            }
        }
    }

    refresh(params: any): boolean {
      return true;
    }

}

