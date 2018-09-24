import { NgModule } from '@angular/core';

import {
    ServingDropDownComponent, MealCheckBoxComponent, EditImageComponent, DeleteImageComponent,
    MacroCalculatorComponent, EditMetabolicInfoComponent, MetabolicCounter, CreateDietComponent
} from './components';
import { DietService } from './services/diet.service';
import { SharedModule } from '../shared/shared.module';
import { AgGridModule } from 'ag-grid-angular';


@NgModule({
    imports: [SharedModule.forRoot(), AgGridModule.withComponents([ServingDropDownComponent,
        MealCheckBoxComponent, EditImageComponent, DeleteImageComponent])],
    exports: [],
    declarations: [ServingDropDownComponent, MealCheckBoxComponent, EditImageComponent,
        DeleteImageComponent, MacroCalculatorComponent, EditMetabolicInfoComponent, MetabolicCounter, CreateDietComponent],
    providers: [DietService],
})
export class DietModule { }
