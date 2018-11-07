import { Component, OnInit, Input } from '@angular/core';
// Include the file that will help Angular render dynamic content include directive below

@Component({
    selector: 'baseLayout',
    templateUrl: 'baseLayout.component.html'
})
export class BaseLayoutComponent implements OnInit {

    @Input() cardcellwidth: string = 'mdl-cell--8-col';

    constructor() { }

    ngOnInit() { }

}
