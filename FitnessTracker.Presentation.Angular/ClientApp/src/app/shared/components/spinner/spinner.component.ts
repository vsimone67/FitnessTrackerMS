import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { events } from '../../models/events.model';

@Component({
  selector: 'spinner',
  templateUrl: 'spinner.component.html'
})
export class SpinnerComponent implements OnInit {
  isActive = '';

  constructor(private _eventService: EventService) {
    _eventService.getEvent(events.spinnerEvent).subscribe(
      spinnerState => {
        this.isActive = spinnerState;
      });
  }
  ngOnInit() { }
}
