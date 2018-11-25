// This file is used to help material ut lite's dynamic elements work properly in Angular 2
// http://stackoverflow.com/questions/34421919/integrating-material-design-lite-with-angular2
import { Directive, AfterViewInit } from '@angular/core';
declare var componentHandler: any;

@Directive({
  selector: '[mdl]'
})
export class MDL implements AfterViewInit {
  ngAfterViewInit() {
    componentHandler.upgradeAllRegistered();
  }
}
