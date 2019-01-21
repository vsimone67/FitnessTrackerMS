import { Component, OnInit, ElementRef, Inject } from "@angular/core";
import { GridOptions } from "ag-grid-community";
import { EventService } from "../../../shared/services";
import { MeatabolicInfo } from "../../models/metabolic-info.model";
import { BaseComponent } from "../../../shared/components";
import { Store, Select } from "@ngxs/store";
import { Observable } from "rxjs/Observable";
import {
  GetMetabolicInfo,
  SaveMetabolicInfo
} from "../../actions/metabolic-info.actions";
import { MetabolicInfoState } from "../../state/metabolic-info.state";
import { THIS_EXPR } from "@angular/compiler/src/output/output_ast";

@Component({
  selector: "editMetabolicInfo",
  template: `
    <ag-grid-angular
      #agGrid
      style="width: 97%; height: 300px;"
      class="ag-theme-blue"
      [gridOptions]="gridOptions"
      [rowData]="metabolidInfoList$ | async"
      (cellValueChanged)="onCellValueChanged($event)"
    >
    </ag-grid-angular>
  `
})
export class EditMetabolicInfoComponent extends BaseComponent
  implements OnInit {
  gridOptions: any;

  @Select(MetabolicInfoState.metabolicInfoList)
  metabolidInfoList$: Observable<Array<MeatabolicInfo>>;

  constructor(
    private _store: Store,
    private _el: ElementRef,
    public _eventService: EventService
  ) {
    super(_eventService);
  }
  ngOnInit() {
    this.setGridOptions();

    this._store.dispatch(new GetMetabolicInfo());
  }
  setGridOptions() {
    this.gridOptions = <GridOptions>{};

    this.gridOptions.defaultColDef = {
      editable: true
    };

    this.gridOptions.columnDefs = this.createColumnDefs();
  }
  createColumnDefs() {
    return [
      { headerName: "MetabolicInfoId", field: "MetabolicInfoId", hide: true },
      {
        headerName: "Macro",
        field: "macro",
        width: 80,
        cellClass: "text-left"
      },
      {
        headerName: "Cut",
        field: "cut",
        width: 80,
        cellClass: "text-right",
        editable: true
      },
      {
        headerName: "Maintain",
        field: "maintain",
        width: 90,
        cellClass: "text-right",
        editable: true
      },
      {
        headerName: "Gain",
        field: "gain",
        width: 80,
        cellClass: "text-right",
        editable: true
      },
      {
        headerName: "Factor",
        field: "factor",
        width: 80,
        cellClass: "text-right",
        editable: true
      }
    ];
  }
  onCellValueChanged($event: any) {
    if ($event.oldValue !== $event.newValue) {
      this._store
        .dispatch(new SaveMetabolicInfo($event.data))
        .subscribe(() => this.showMessage("Data Saved"));
    }
  }
}
