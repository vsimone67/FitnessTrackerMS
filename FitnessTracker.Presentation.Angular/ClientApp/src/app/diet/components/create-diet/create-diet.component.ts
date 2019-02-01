import { Component, OnInit, ElementRef, ViewChild } from "@angular/core";
import { GridOptions } from "ag-grid-community";
import { BaseComponent } from "../../../shared/components";
import { MacroCalculatorComponent } from "../../components/macro-calculator/macro-calculator.component";
import { MealCheckBoxComponent } from "../../components/grid/meal-check-box/meal-check-box.component";
import { EditImageComponent } from "../../components/grid/edit-image/edit-image.component";
import { DeleteImageComponent } from '../../../shared/components/delete-image/delete-image.component'
import { ServingDropDownComponent } from "../../components/grid/servings-drop-down/servings-drop-down.component";
import { EventService } from "../../../shared/services";
import {
  AddMenuItem,
  DeleteMenuItem,
  GetColumns,
  SaveMenu,
  CreateMenu
} from "../../actions/diet.actions";
import { FoodInfo, Columns, NutritionInfo } from "../../models";
import { Store, Select } from "@ngxs/store";
import { Observable } from "rxjs/Observable";
import { DietState } from "../../state/diet.state";

@Component({
  templateUrl: "create-diet.component.html"
})
export class CreateDietComponent extends BaseComponent implements OnInit {
  @ViewChild(MacroCalculatorComponent)
  macroCalculator: MacroCalculatorComponent;

  dietItem: FoodInfo;
  columns: Array<Columns>;
  maxMacro: NutritionInfo;
  gridOptions: GridOptions;
  dialog: any;
  cell: FoodInfo;
  meals: Array<NutritionInfo>;
  totals: NutritionInfo;
  newColumns: any;
  foodItems: Array<FoodInfo>;

  @Select(DietState.foodItems) foodList$: Observable<Array<FoodInfo>>; // list of availbie food
  @Select(DietState.columns) columns$: Observable<Array<Columns>>; // colums to use for the grid and macro calc
  @Select(DietState.meals) meals$: Observable<Array<NutritionInfo>>; // selected meals for menu

  constructor(
    public _store: Store,
    public _eventService: EventService,
    private _el: ElementRef
  ) {
    super(_eventService);

    this.setGridOptions();
    this.columns = new Array<Columns>();
    this.cell = new FoodInfo();
    this.cell.ItemId = 0; // default to add
    this.totals = new NutritionInfo(0, "0");

    // subscribe to the changed in meals
    this.meals$.subscribe(meals => {
      if (meals.length > 0) {
        this.meals = meals;
      }
    });

    // subscribe when the food items are returned from the api
    this.foodList$.subscribe(foodList => {
      this._store.dispatch(new CreateMenu(foodList, this.columns)); // create menu from items saved (they come from the foodlist returned from the api server)
    });
  }
  ngOnInit() {
    this._store.dispatch(new GetColumns()).subscribe(() =>
      this.columns$.subscribe(columns => {
        this.addMealColumns(columns);
        this.columns = columns;
      })
    );
  }
  setGridOptions() {
    this.gridOptions = <GridOptions>{};
    this.gridOptions.rowHeight = 30;
  }
  addMealColumns(columns: Array<Columns>) {
    this.newColumns = this.createColumnDefs();

    columns.forEach(column => {
      this.newColumns.push({
        headerName: column.MealDisplayName,
        field: column.MealId.toString(),
        cellRendererFramework: MealCheckBoxComponent,
        cellClass: "text-center",
        width: 95
      });
    });

    // add edit icon
    this.newColumns.push({
      headerName: "",
      field: "#editFood",
      cellRendererFramework: EditImageComponent,
      width: 55
    });
    // add delete icon
    this.newColumns.push({
      headerName: "",
      field: "#deleteFood",
      cellRendererFramework: DeleteImageComponent,
      width: 55
    });

    if (this.gridOptions.api !== undefined) {
      this.gridOptions.api.setColumnDefs(this.newColumns);
    }
  }
  createColumnDefs() {
    return [
      { headerName: "Id", field: "ItemId", hide: true },
      { headerName: "Item", field: "Item" },
      { headerName: "Serving", field: "ServingSize", width: 88 },
      {
        headerName: "Calories",
        field: "Calories",
        width: 90,
        cellClass: "text-center"
      },
      {
        headerName: "Protein",
        field: "Protien",
        width: 83,
        cellClass: "text-center"
      },
      {
        headerName: "Carbs",
        field: "Carbs",
        width: 75,
        cellClass: "text-center"
      },
      { headerName: "Fat", field: "Fat", width: 75, cellClass: "text-center" },
      {
        headerName: "Serving",
        field: "Serving",
        cellRendererFramework: ServingDropDownComponent,
        width: 85
      }
    ];
  }
  onCellClicked($event: any) {
    if (
      $event.colDef.field === "#editFood" ||
      $event.colDef.field === "#deleteFood"
    ) {
      this.cell = $event.data;
      this.showDialog($event.colDef.field);
    }
  }
  showDialog(dialogName: string) {
    this.dialog = this._el.nativeElement.querySelector(dialogName);

    if (!this.dialog.showModal) {
      this.dialog.dialogPolyfill.registerDialog(this.dialog);
    }

    this.dialog.showModal();
  }
  onAddFood() {
    this.cell = new FoodInfo();
    this.cell.ItemId = 0; // default to add
    this.showDialog("#editFood");
  }
  onPrint() {
    //this.meals = this._dietervice.getNutritionInfo();
    this.totals = this.meals.find(exp => exp.meal === "Totals");
    this.showDialog("#showMenu");
  }
  onPrintMenu() {
    // TODO: convert to Angular 2 syntax and style
    let printContents = document.getElementById("printable").innerHTML;
    var popupWin: any;

    if (navigator.userAgent.toLowerCase().indexOf("chrome") > -1) {
      let popupWin = window.open(
        "",
        "_blank",
        "width=600,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no"
      );
      popupWin.window.focus();
      popupWin.document.write(
        "<!DOCTYPE html><html><head>" +
        '<link rel="stylesheet" type="text/css" href="content/css/style.css" />' +
        '</head><body onload="window.print()"><div class="reward-body">' +
        printContents +
        "</div></html>"
      );
      popupWin.onbeforeunload = function (event) {
        popupWin.document.close();
        return ".\n";
      };
      popupWin.onabort = function (event) {
        popupWin.document.close();
        popupWin.close();
      };
    } else {
      let popupWin = window.open("", "_blank", "width=800,height=600");
      popupWin.document.open();
      popupWin.document.write(
        '<html><head><link rel="stylesheet" type="text/css" href="content/css/style.css" /></head><body onload="window.print()">' +
        printContents +
        "</html>"
      );
      popupWin.document.close();
    }

    this.onClose();
  }
  onEditMetabolicInfo() {
    this.showDialog("#editMetabolicInfo");
  }
  onSaveMenu() {
    this._store
      .dispatch(new SaveMenu(this.meals))
      .subscribe(() => this.showMessage("Menu Saved"));
  }
  onSaveFoodItem() {
    this._store.dispatch(new AddMenuItem(this.cell)).subscribe(() => {
      this.showMessage("Food Saved");
      this.dialog.close();
    });
  }
  onDelete() {
    this._store.dispatch(new DeleteMenuItem(this.cell)).subscribe(() => {
      this.showMessage("Food Deleted");
      this.dialog.close();
    });
  }
  onClose() {
    this.dialog.close();
  }
  onCalcMacro() {
    this.macroCalculator.showDialog();
  }
}
