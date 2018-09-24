
import { Component, OnInit, ElementRef, Inject } from '@angular/core';
import * as Redux from 'redux';
import { GridOptions } from 'ag-grid/main';

import { EventService } from '../../../shared/services';
import { DietService } from '../../services/diet.service';
import { MeatabolicInfo } from '../../models';
import { BaseComponent } from '../../../shared/components';
import { DietPlannerActions, DIETPLANNER_ACTIONS } from '../../../shared/actions';

import { AppStore, AppState } from '../../../app';

@Component({
    moduleId: module.id,
    selector: 'editMetabolicInfo',
    template: ` <ag-grid-angular #agGrid style="width: 97%; height: 300px;" class="ag-blue"
                     [gridOptions]="gridOptions"
                     (cellValueChanged)="onCellValueChanged($event)">
                </ag-grid-angular>`
})
export class EditMetabolicInfoComponent extends BaseComponent implements OnInit {
    gridOptions: any;

    constructor(private _dietService: DietService, private _el: ElementRef,
        public _eventService: EventService, private _dietPlannerActions: DietPlannerActions,
        @Inject(AppStore) private _store: Redux.Store<AppState>) {
        super(_eventService);

        _store.subscribe(() => this.updateState());
    }
    ngOnInit() {

        this.setGridOptions();

        this._dietService.getMetabolicInfo((metabolicInfo: MeatabolicInfo) => {
            this._dietPlannerActions.updateStore<MeatabolicInfo>(metabolicInfo, DIETPLANNER_ACTIONS.GET_METABOLICINFO);
        });

    }
    setGridOptions() {
        this.gridOptions = <GridOptions>{};
        this.gridOptions.enableCellEdit = true;
        this.gridOptions.columnDefs = this.createColumnDefs();
    }
    createColumnDefs() {
        return [
            { headerName: 'MetabolicInfoId', field: 'MetabolicInfoId', hide: true },
            { headerName: 'Macro', field: 'macro', width: 70, cellClass: 'text-left' },
            { headerName: 'Cut', field: 'cut', width: 60, cellClass: 'text-right', editable: true },
            { headerName: 'Maintain', field: 'maintain', width: 60, cellClass: 'text-right', editable: true },
            { headerName: 'Gain', field: 'gain', width: 60, cellClass: 'text-right', editable: true },
            { headerName: 'Factor', field: 'factor', width: 60, cellClass: 'text-right', editable: true }


        ];
    }
    onCellValueChanged($event: any) {

        if ($event.oldValue !== $event.newValue) {
            this._dietService.saveMetabolicInfo($event.data, () => {
                this.showMessage('Data Saved');
            });
        }
    }
    updateState() {
        let state = this._store.getState();

        if (this.gridOptions.api !== null) {
            this.gridOptions.api.setRowData(state.metabolicInfo.metabolicInfo);
        }
    }
}
