import { Component, OnInit, Input, ElementRef, Inject } from '@angular/core';
import * as Redux from 'redux';
import { GridOptions } from 'ag-grid/main';

import { AppStore, AppState } from '../../../app';
import { BaseComponent } from '../../../shared/components';
import { EventService } from '../../../shared/services';
import { WorkoutService } from '../../services/workout.service';
import { BodyInfo } from '../../models';
import { DietPlannerActions, DIETPLANNER_ACTIONS } from '../../../shared/actions';

@Component({
    moduleId: module.id,
    templateUrl: 'addBodyInfo.component.html'
})
export class AddBodyInfoComponent extends BaseComponent implements OnInit {
    @Input() title: string;
    bodyInfoItems: Array<BodyInfo>;
    dialog: any;
    bodyInfo: BodyInfo;
    gridOptions: GridOptions;

    constructor( @Inject(AppStore) private _store: Redux.Store<AppState>, private _workoutService: WorkoutService,
        private _el: ElementRef, public _eventService: EventService, private _dietPlannerActions: DietPlannerActions) {

        super(_eventService);

        this.bodyInfo = new BodyInfo();
        this.setGridOptions();

        _store.subscribe(() => this.updateState());
    }

    ngOnInit() {
        this.loadBodyInfo();
    }
    loadBodyInfo() {
        this._workoutService.getBodyInfo((bodyInfo: Array<BodyInfo>) => {
            this._dietPlannerActions.updateStore<Array<BodyInfo>>(bodyInfo, DIETPLANNER_ACTIONS.GET_BODY_INFO);
        });
    }
    setGridOptions() {
        this.gridOptions = <GridOptions>{};
        this.gridOptions.columnDefs = this.createColumnDefs();
        this.gridOptions.enableColResize = true;
        // ag-grid does not do a good job of wrapping text so I added a custom row height to wrap the text for notes
        this.gridOptions.getRowHeight = function (params: any) {
            // assuming line height of 30 and max 80 charcacters per line            
            return 30 * ((params.data.Note === null) ? 1 : (Math.floor(params.data.Note.length / 80) + 1));
        };
    }
    createColumnDefs() {
        return [
            { headerName: 'Phase', field: 'Phase', width: 185 },
            {
                headerName: 'Date', field: 'Date', width: 85,
                cellRenderer: this.dateCellRenderer
            },
            { headerName: 'Weight', field: 'Weight', width: 70, cellClass: this.weightCSSRenderer, cellRenderer: this.weightCellRenderer },
            {
                headerName: 'BodyFat', field: 'BodyFat', width: 70, cellClass: this.bodyFatCSSRenderer,
                cellRenderer: this.bodyFatCellRenderer
            },
            { headerName: 'Calories', field: 'Calories', width: 70, cellClass: 'text-right' },
            { headerName: 'Protien', field: 'Protein', width: 70, cellClass: 'text-right' },
            { headerName: 'Carbs', field: 'Carbs', width: 60, cellClass: 'text-right' },
            { headerName: 'Fat', field: 'Fat', width: 60, cellClass: 'text-right' },
            { headerName: 'Note', field: 'Note', cellClass: 'wrap-text', width: 500 }

        ];
    }
    weightCellRenderer(parms: any) {
        return arrowCell(parms.value, parms.data.isBestWeight, parms.data.isWorstWeight);
    }
    weightCSSRenderer(parms: any) {
        let retCSS = 'text-right ';
        let currentIndex = Number(parms.node.id);
        let value = Number(parms.value);
        let model = parms.api.getModel();
        let items = model.rowsToDisplay;

        if (currentIndex > 0 && currentIndex < items.length) {
            if (value > items[currentIndex - 1].data.Weight) {
                retCSS += 'text-red';
            } else if (value < items[currentIndex - 1].data.Weight) {
                retCSS += 'text-green';
            }
        }

        return retCSS;
    }
    bodyFatCellRenderer(parms: any) {
        return arrowCell(parms.value, parms.data.isBestBodyFat, parms.data.isWorstBodyFat);
    }
    bodyFatCSSRenderer(parms: any) {
        let retCSS = 'text-right ';
        let currentIndex = Number(parms.node.id);
        let value = Number(parms.value);
        let model = parms.api.getModel();
        let items = model.rowsToDisplay;

        if (currentIndex > 0 && currentIndex < items.length) {
            if (value > items[currentIndex - 1].data.BodyFat) {
                retCSS += 'text-red';
            } else if (value < items[currentIndex - 1].data.BodyFat) {
                retCSS += 'text-green';
            }
        }

        return retCSS;
    }
    dateCellRenderer(parms: any) {
        let eDivPercentBar = document.createElement('div');

        let eValue = document.createElement('div');
        eValue.innerHTML = parms.value.substr(0, 10);
        let eOuterDiv = document.createElement('div');
        eOuterDiv.appendChild(eValue);
        eOuterDiv.appendChild(eDivPercentBar);

        return eOuterDiv;
    }
    addBodyInfo() {
        this.dialog = this._el.nativeElement.querySelector('dialog');

        if (!this.dialog.showModal) {
            this.dialog.dialogPolyfill.registerDialog(this.dialog);
        }

        this.dialog.showModal();
    }
    onSave() {
        this._workoutService.saveBodyInfo(this.bodyInfo, () => {
            this.showMessage('Body Info Saved');
            this.dialog.close();
            this.loadBodyInfo();
        });
    }
    onClose() {
        this.dialog.close();
    }
    updateState() {
        let state = this._store.getState();

        if (this.gridOptions.api !== null) {
            this.gridOptions.api.setRowData(state.bodyInfo.bodyInfoItems);
        }
    }
}

function arrowCell(value: any, isMax: boolean, isMin: boolean) {

    let eDivPercentBar = document.createElement('div');

    let eValue = document.createElement('div');

    if (isMax) {
        eValue.innerHTML = value + '<i class="material-icons">sentiment_satisfied</i>';
    } else if (isMin) {
        eValue.innerHTML = value + '<i class="material-icons">sentiment_dissatisfied</i>';
    } else {
        eValue.innerHTML = value;
    }
    let eOuterDiv = document.createElement('div');
    eOuterDiv.appendChild(eValue);
    eOuterDiv.appendChild(eDivPercentBar);

    return eOuterDiv;
}
