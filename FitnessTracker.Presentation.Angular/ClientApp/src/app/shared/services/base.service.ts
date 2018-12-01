import { Observable } from 'rxjs/Observable';
import { Http, Response } from '@angular/http';
import { EventService } from '../services';
import { Toast, events } from '../models';
import { AppsettingsService } from '../../shared/services'
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

export class BaseService {
  protected errorColor: string = 'mdl-color--red';
  protected isSettingsLoaded: boolean;
  protected serviceURL: string;

  constructor(public _http: Http, public _eventService: EventService, public _appSettings: AppsettingsService) { }

  async loadConfiguration(serviceURL: string) {
    if (!this.isSettingsLoaded) {
      await this.loadConfigData();
      this.serviceURL = await this._appSettings.GetValue(serviceURL);
      this.isSettingsLoaded = true;
    }
  }
  async loadConfigData() {
    try {
      await this._appSettings.LoadConfigData('assets/appsettings/appsettings.json');
    }
    catch (e) {
      console.log("Error " + e.message);
    }
  }
  getData(url: string) {
    return this._http.get(url)
      .map((response: Response) => response.json())
      .catch(this._handlerError);
  }
  putData(url: string, payload: any) {
    return this._http.post(url, payload)
      .map((response: Response) => response.json())
      .catch(this._handlerError);
  }
  _handlerError(error: Response | any) {
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }

    return Observable.throw(errMsg);
  }
  getDataWithSpinner(url: string, callback: any) {
    this.spinnerOn();

    this.getData(url).subscribe((data) => { callback(data); },
      (err) => this.showError(err),
      () => this.spinnerOff()
    );
  }
  putDataWithSpinner(url: string, payload: any, callback: any) {
    this.spinnerOn();

    this.putData(url, payload).subscribe((data) => { callback(data); },
      (err) => this.showError(err),
      () => this.spinnerOff()
    );
  }
  spinnerOn() {
    this._eventService.sendEvent(events.spinnerEvent, 'is-active');
  }
  spinnerOff() {
    this._eventService.sendEvent(events.spinnerEvent, '');
  }
  showError(message: string) {
    this.spinnerOff();
    // TODO:  for some reason you have to call the event twice when changing the color, fix this
    this._eventService.sendEvent(events.toastEvent, new Toast(message, 10, this.errorColor));
    this._eventService.sendEvent(events.toastEvent, new Toast(message, 8000, this.errorColor));
  }
}
