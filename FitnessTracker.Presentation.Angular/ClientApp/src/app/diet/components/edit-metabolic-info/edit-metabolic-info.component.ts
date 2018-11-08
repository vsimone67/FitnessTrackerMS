import { Component, OnInit, ElementRef, Inject } from "@angular/core";
import { GridOptions } from "ag-grid-community";
import { EventService } from "../../../shared/services";
import { MeatabolicInfo } from "../../models";
import { BaseComponent } from "../../../shared/components";
import { Store, Select } from "@ngxs/store";
import { Observable } from "rxjs/observable";
import { GetMetabolicInfo, SaveMetabolicInfo } from "../../actions/metabolic-info.actions"
import { MetabolicInfoState } from '../../state/metabolic-info.state'

@Component({
  selector: "editMetabolicInfo",
  template: `
    <ag-grid-angular
      #agGrid
      style="width: 97%; height: 300px;"
      class="ag-theme-blue"
      [gridOptions]="gridOptions"
      [rowData]="metabolidInfoList$ | async"
      (cellValueChanged)="onCellValueChanged($event)">
    </ag-grid-angular>`
})
export class EditMetabolicInfoComponent extends BaseComponent
  implements OnInit {
  gridOptions: any;

  @Select(MetabolicInfoState.metabolicInfoList) metabolidInfoList$: Observable<Array<MeatabolicInfo>>;

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
    // this.gridOptions.enableCellEdit = true;
    this.gridOptions.columnDefs = this.createColumnDefs();
  }
  createColumnDefs() {
    return [
      { headerName: "MetabolicInfoId", field: "MetabolicInfoId", hide: true },
      {
        headerName: "Macro",
        field: "macro",
        width: 70,
        cellClass: "text-left"
      },
      {
        headerName: "Cut",
        field: "cut",
        width: 60,
        cellClass: "text-right",
        editable: true
      },
      {
        headerName: "Maintain",
        field: "maintain",
        width: 60,
        cellClass: "text-right",
        editable: true
      },
      {
        headerName: "Gain",
        field: "gain",
        width: 60,
        cellClass: "text-right",
        editable: true
      },
      {
        headerName: "Factor",
        field: "factor",
        width: 60,
        cellClass: "text-right",
        editable: true
      }
    ];
  }
  onCellValueChanged($event: any) {
    if ($event.oldValue !== $event.newValue) {
      this._store.dispatch(new SaveMetabolicInfo($event)).subscribe( () =>  this.showMessage("Data Saved"));      
    }
  }
}
