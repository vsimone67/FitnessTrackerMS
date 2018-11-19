import { Component, OnInit, Input, SimpleChange } from "@angular/core";
import { NutritionInfo, Columns, CurrentMacros, CalcMacro } from "../../models";
import { DropDownModel } from "../../../shared/models";
import { DietService } from "../../service/diet.service";
import { Store, Select } from "@ngxs/store";
import { SetMeals } from "../../actions/diet.actions";
import { Observable } from "rxjs/observable";
import { DietState } from "../../state/diet.state";

@Component({
  selector: "metabolicCounter",
  templateUrl: "metabolic-counter.component.html"
})
export class MetabolicCounterComponent implements OnInit {
  @Input() macroColumns: Array<NutritionInfo>;

  macroMax: number; // hold what type of max, Low Carb (1), High Carb (2) , Gain (3)
  mode: DropDownModel; // hold what type of cacl (carb cycle, calorie, etc)
  modes: Array<DropDownModel>;

  modeLocalStorage: string = "currentMode";

  @Select(DietState.meals) meals$: Observable<Array<NutritionInfo>>;

  constructor(private _dietervice: DietService, private _store: Store) {
    this.macroColumns = new Array<NutritionInfo>();
    this.macroMax = 1;

    this.meals$.subscribe(meals => {
      if (this.macroColumns.length > 0) {
        this.calcTotals();
      }
    });
  }

  ngOnInit() {
    this.modes = new Array<DropDownModel>();
    this.modes.push(new DropDownModel("Low Carb", "cut"));
    this.modes.push(new DropDownModel("High Carb", "maintain"));
    this.modes.push(new DropDownModel("Gain", "gain"));

    let savedMode = localStorage.getItem(this.modeLocalStorage);

    if (savedMode != null) {
      this.mode = this.modes.find(exp => exp.value === savedMode);
    } else {
      this.mode = this.modes[0];
    }
  }

  metabolicInfoSelected(item: DropDownModel) {
    this.mode = item;
    this.calcTotals();

    localStorage.setItem(this.modeLocalStorage, this.mode.value);
  }
  calcTotals() {
    this._dietervice.getcurrentMacro(
      this.mode.value,
      (metabolicInfo: CurrentMacros) => {
        let calcMacro = new CalcMacro(metabolicInfo);
        let maxMacro = this.macroColumns.find(
          meal => meal.meal === "Max Macro"
        );
        let remaining = this.macroColumns.find(
          meal => meal.meal === "Remaining"
        );
        let totals = this.macroColumns.find(meal => meal.meal === "Totals");

        maxMacro.calories = calcMacro.calcCalories();
        maxMacro.carbs = calcMacro.caclCarbs();
        maxMacro.fat = calcMacro.calcFat();
        maxMacro.protein = calcMacro.calcProtein();

        remaining.calories = maxMacro.calories - totals.calories;
        remaining.carbs = maxMacro.carbs - totals.carbs;
        remaining.protein = maxMacro.protein - totals.protein;
        remaining.fat = maxMacro.fat - totals.fat;

        this._store.dispatch(new SetMeals(this.macroColumns));
      }
    );
  }
}
