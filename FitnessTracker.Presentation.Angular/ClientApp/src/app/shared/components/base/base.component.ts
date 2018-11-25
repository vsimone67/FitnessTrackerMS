import { EventService } from '../../../shared/services';
import { Toast, events } from '../../models';

export class BaseComponent {
  protected errorColor: string = 'mdl-color--red';
  protected messageColor: string = 'mdl-color--primary';

  constructor(public _eventService: EventService) { }

  showMessage(message: string) {
    // TODO:  for some reason you have to call the event twice when changing the color, fix this
    this._eventService.sendEvent(events.toastEvent, new Toast(message, 10, this.messageColor));
    this._eventService.sendEvent(events.toastEvent, new Toast(message, 4000, this.messageColor));
  }
}
