import { Component, OnInit, ElementRef } from '@angular/core';

import { EventService } from '../../services';
import { events } from '../../models';

@Component({    
    selector: 'toast',
    templateUrl: 'toast.component.html'
})
export class ToastComponent implements OnInit {
    toastColor: string = 'mdl-color--primary';
    constructor(private el: ElementRef, private _eventService: EventService) {
        _eventService.getEvent(events.toastEvent).subscribe(
            toastObj => {
                this.toastColor = toastObj.color;
                let snackbarContainer = this.el.nativeElement.querySelector('#popuptoast');
                snackbarContainer.MaterialSnackbar.showSnackbar(toastObj);
            });
    }
    ngOnInit() { }
}
