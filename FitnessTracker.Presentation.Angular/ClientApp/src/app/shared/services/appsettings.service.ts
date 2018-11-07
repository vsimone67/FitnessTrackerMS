import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AppsettingsService {
  private _configData: any;
  constructor(private _http: HttpClient) { }

  public async LoadConfigData(url: string) {
    this._configData = await this._http.get(url).toPromise();
  }

  public GetValue(item: string): any {
    return this._configData[item];
  }

  public async GetConfigData<T>(url: string) {
    return await this._http.get<T>(url).toPromise();
  }
}
