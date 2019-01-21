import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CreateDietComponent } from "./components/create-diet/create-diet.component";
import { EditMetabolicInfoComponent } from "./components/edit-metabolic-info/edit-metabolic-info.component";
import { DeleteImageComponent } from '../shared/components/delete-image/delete-image.component'
import { EditImageComponent } from "./components/grid/edit-image/edit-image.component";
import { MealCheckBoxComponent } from "./components/grid/meal-check-box/meal-check-box.component";
import { ServingDropDownComponent } from "./components/grid/servings-drop-down/servings-drop-down.component";
import { MacroCalculatorComponent } from "./components/macro-calculator/macro-calculator.component";
import { MetabolicCounterComponent } from "./components/metabolic-counter/metabolic-counter.component";
import { DietService } from "../diet/service/diet.service";
import { AgGridModule } from "ag-grid-angular";
import { SharedModule } from "../shared/shared.module";
import { NgxsModule } from "@ngxs/store";
import { DietState } from "../diet/state/diet.state";
import { MetabolicInfoState } from "../../app/diet/state/metabolic-info.state";

@NgModule({
  imports: [
    SharedModule.forRoot(),
    CommonModule,
    AgGridModule.withComponents([
      ServingDropDownComponent,
      MealCheckBoxComponent,
      EditImageComponent      
    ]),
    NgxsModule.forRoot([DietState, MetabolicInfoState])
  ],
  declarations: [
    CreateDietComponent,
    EditMetabolicInfoComponent,
    EditImageComponent,
    MealCheckBoxComponent,
    ServingDropDownComponent,
    MacroCalculatorComponent,
    MetabolicCounterComponent
  ],
  exports: [
    CreateDietComponent,
    EditMetabolicInfoComponent,    
    EditImageComponent,
    MealCheckBoxComponent,
    ServingDropDownComponent,
    MacroCalculatorComponent,
    MetabolicCounterComponent
  ],
  providers: [DietService],
  entryComponents: [DeleteImageComponent]
})
export class DietModule { }
