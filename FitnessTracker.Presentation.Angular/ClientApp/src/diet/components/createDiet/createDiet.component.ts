import { Component, OnInit, Inject, ElementRef, ViewChild } from '@angular/core';
import * as Redux from 'redux';
import { GridOptions } from 'ag-grid/main';

import { AppStore, AppState } from '../../../app';
import { BaseComponent } from '../../../shared/components';
import {
    ServingDropDownComponent, MealCheckBoxComponent,
    EditImageComponent, DeleteImageComponent, MacroCalculatorComponent
} from '../../components';
import { EventService } from '../../../shared/services';
import { DietService } from '../../services/diet.service';
import { DietPlannerActions, DIETPLANNER_ACTIONS } from '../../../shared/actions';
import { FoodInfo, Columns, NutritionInfo } from '../../models';

@Component({
    moduleId: module.id,
    templateUrl: 'createDiet.component.html'
})
export class CreateDietComponent extends BaseComponent implements OnInit {
    @ViewChild(MacroCalculatorComponent) macroCalculator: MacroCalculatorComponent;
    dietItem: FoodInfo;
    columns: Array<Columns>;
    maxMacro: NutritionInfo;
    gridOptions: GridOptions;
    dialog: any;
    cell: FoodInfo;
    meals: Array<NutritionInfo>;
    totals: NutritionInfo;

    constructor( @Inject(AppStore) private _store: Redux.Store<AppState>,
        private _dietervice: DietService, public _eventService: EventService,
        private _el: ElementRef, private _dietPlannerActions: DietPlannerActions) {

        super(_eventService);
        this.setGridOptions();
        this.columns = new Array<Columns>();
        this.cell = new FoodInfo();
        this.cell.ItemId = 0;  // default to add 
        this.totals = new NutritionInfo(0, '0');
        _store.subscribe(() => this.updateState());
    }
    ngOnInit() {

        this._dietervice.getColumns((columns: Array<Columns>) => {
            this.addMealColumns(columns);
            this.columns = columns;
            this.loadMenuItems();
        });
    }
    loadMenuItems() {
        this._dietervice.getDietItems((dietItems: any) => {
            this._dietPlannerActions.updateStore<Array<FoodInfo>>(dietItems, DIETPLANNER_ACTIONS.GET_ALL_MENU_ITEMS);
        });


    }
    setGridOptions() {
        this.gridOptions = <GridOptions>{};
        this.gridOptions.rowHeight = 30;
    }
    addMealColumns(columns: Array<Columns>) {
        let newColumns = this.createColumnDefs();

        columns.forEach((column) => {
            newColumns.push({
                headerName: column.MealDisplayName, field: column.MealId.toString(),
                cellRendererFramework: MealCheckBoxComponent,
                cellClass: 'text-center', width: 99
            }
            );
        });

        // add edit icon

        newColumns.push({
            headerName: '', field: '#editFood', cellRendererFramework:
            EditImageComponent
            , width: 35
        });
        // add delete icon
        newColumns.push({
            headerName: '', field: '#deleteFood', cellRendererFramework:
            DeleteImageComponent
            , width: 35
        });

        this.gridOptions.api.setColumnDefs(newColumns);
    }
    createColumnDefs() {
        return [

            { headerName: 'Id', field: 'ItemId', hide: true },
            { headerName: 'Item', field: 'Item' },
            { headerName: 'Serving', field: 'ServingSize', width: 88 },
            { headerName: 'Calories', field: 'Calories', width: 63, cellClass: 'text-center' },
            { headerName: 'Protein', field: 'Protien', width: 65, cellClass: 'text-center' },
            { headerName: 'Carbs', field: 'Carbs', width: 55, cellClass: 'text-center' },
            { headerName: 'Fat', field: 'Fat', width: 55, cellClass: 'text-center' },
            {
                headerName: 'Serving', field: 'Serving',
                cellRendererFramework: ServingDropDownComponent, width: 70
            }

        ];
    }
    onCellClicked($event: any) {

        if ($event.colDef.field === '#editFood' || $event.colDef.field === '#deleteFood') {
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
        this.cell.ItemId = 0;  // default to add 
        this.showDialog('#editFood');
    }
    onPrint() {
        this.meals = this._dietervice.getNutritionInfo();
        this.totals = this.meals.find(exp => exp.meal === 'Totals');
        this.showDialog('#showMenu');
    }
    onPrintMenu() {

        // TODO: convert to Angular 2 syntax and style
        let printContents = document.getElementById('printable').innerHTML;
        var popupWin: any;

        if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
            let popupWin = window.open('', '_blank',
                'width=600,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
            popupWin.window.focus();
            popupWin.document.write('<!DOCTYPE html><html><head>' +
                '<link rel="stylesheet" type="text/css" href="content/css/style.css" />' +
                '</head><body onload="window.print()"><div class="reward-body">' + printContents + '</div></html>');
            popupWin.onbeforeunload = function (event) {
                popupWin.document.close();
                return '.\n';
            };
            popupWin.onabort = function (event) {
                popupWin.document.close();
                popupWin.close();
            };
        } else {
            let popupWin = window.open('', '_blank', 'width=800,height=600');
            popupWin.document.open();
            popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="content/css/style.css" /></head><body onload="window.print()">' + printContents + '</html>');
            popupWin.document.close();
        }
        popupWin.document.close();
        this.onClose();
    }
    onEditMetabolicInfo() {
        this.showDialog('#editMetabolicInfo');
    }
    onSaveMenu() {
        this._dietervice.saveMenu(this._dietervice.getNutritionInfo(), () => { this.showMessage('Menu Saved'); });
    }
    onSaveFoodItem() {
        this._dietervice.processItem(this.cell, () => {
            this.showMessage('Food Saved');
            this.dialog.close();
            this.loadMenuItems();
        });
    }
    onDelete() {
        this._dietervice.deleteItem(this.cell, () => {
            this.showMessage('Food Deleted');
            this.dialog.close();
            this.loadMenuItems();
        });
    }
    onClose() {
        this.dialog.close();
    }
    onCalcMacro() {
        this.macroCalculator.showDialog();
    }
    updateState() {
        let state = this._store.getState();

        if (state.diet.event === DIETPLANNER_ACTIONS.GET_ALL_MENU_ITEMS) {
            if (this.gridOptions.api !== null) {
                this.gridOptions.api.setRowData(state.diet.foodItems);
            }
        } else if (state.diet.event === DIETPLANNER_ACTIONS.GET_MENU_ITEM) {
            this.dietItem = state.diet.foodItem;
        }

    }
}

