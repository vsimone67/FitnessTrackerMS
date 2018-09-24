import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    moduleId: module.id,
    selector: 'dropdown',
    templateUrl: 'dropdown.component.html'
})
export class DropDownComponent implements OnInit {
    @Input() displayField: string;
    @Input() items: any[];
    @Input() model: any;
    @Output() itemSelected: EventEmitter<any> = new EventEmitter();

    constructor() { }

    ngOnInit() {}

    getItem(item: any) {
        return item[this.displayField];
    }
    onItemSelected(itemIndex: any) {
        this.itemSelected.emit(this.items[itemIndex]);
    }
}

