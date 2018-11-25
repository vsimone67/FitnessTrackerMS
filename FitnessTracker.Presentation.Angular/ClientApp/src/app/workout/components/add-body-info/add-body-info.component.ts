import { Component, OnInit, Input, ElementRef } from '@angular/core';
import { GridOptions } from 'ag-grid-community';
import { Store, Select } from '@ngxs/store';
import { Observable } from 'rxjs/observable'
import { GetBodyInfo, SaveBodyInfo } from '../../actions/body-info.actions'
import { BaseComponent } from '../../../shared/components';
import { EventService } from '../../../shared/services';
import { BodyInfo } from '../../models';
import { BodyInfoState } from '../../state/body-info.state'

@Component({
  templateUrl: 'add-body-info.component.html'
})
export class AddBodyInfoComponent extends BaseComponent implements OnInit {
  @Input() title: string;
  dialog: any;
  bodyInfo: BodyInfo;
  gridOptions: GridOptions;

  @Select(BodyInfoState.BodyInfoItems) bodyInfoItems$: Observable<Array<BodyInfo>>;

  constructor(public store: Store, private _el: ElementRef, public _eventService: EventService) {
    super(_eventService);

    this.bodyInfo = new BodyInfo();

    this.setGridOptions();
  }

  ngOnInit() {
    this.store.dispatch(new GetBodyInfo());
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
    this.store.dispatch(new SaveBodyInfo(this.bodyInfo)).subscribe(() => {
      this.showMessage('Body Info Saved');
      this.dialog.close();
    });
  }
  onClose() {
    this.dialog.close();
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
