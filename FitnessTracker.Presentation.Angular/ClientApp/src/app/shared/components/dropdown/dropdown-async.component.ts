import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  SimpleChange
} from "@angular/core";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

@Component({
  selector: "dropdownAsync",
  templateUrl: "dropdown-async.component.html"
})
export class DropDownComponent implements OnInit {
  @Input() displayField: string;
  @Input() items: Observable<any[]>;
  @Input() model: any;
  @Input() defaultText: string;
  @Input() keyChange: boolean;
  @Output() itemSelected: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() { }

  // The ngOnChanges is used to keep dropdown data in an ngfor from cascade change.  The method below is equivilent to the "::" operator in angualr 1
  ngOnChanges(changes: { [key: string]: SimpleChange }): void {
    //  check if the value keyChange is passed (true = we want to preserve the data "::")
    if (this.keyChange !== undefined) {
      // make sure there is a valid model
      if (changes["model"] !== undefined) {
        // if we are no on the first change make sure the model is holding on to the current value and not the new value in case of a cascde change
        if (!changes["model"].isFirstChange()) {
          this.model = changes["model"].previousValue;
        }
      }
    }
  }
  getItem(item: any) {
    return item[this.displayField];
  }
  onItemSelected(itemIndex: any) {
    this.items
      .pipe(map(items => items[itemIndex]))
      .subscribe(val => this.itemSelected.emit(val));
  }
}
