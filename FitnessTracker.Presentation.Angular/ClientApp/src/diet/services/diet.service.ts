import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { BaseService, EventService } from '../../shared/services';
import { NutritionInfo } from '../models';
import { SiteConstants } from '../../shared/models';

const URL_SITE_DIET = SiteConstants.apiServer + '/api/Diet/';

@Injectable()
export class DietService extends BaseService {

  protected nutritionInfo: Array<NutritionInfo>;

  constructor(public _http: Http, public _eventService: EventService) {
    super(_http, _eventService);

    this.nutritionInfo = new Array<NutritionInfo>();
  }

  getDietItems(callback: any) {
    return this.getDataWithSpinner(URL_SITE_DIET + 'GetSavedMenuItems', callback);
  }
  getColumns(callback: any) {
    return this.getDataWithSpinner(URL_SITE_DIET + 'GetColumns', callback);
  }
  saveMenu(payload: any, callback: any) {
    return this.putDataWithSpinner(URL_SITE_DIET + 'SaveMenu', payload, callback);
  }
  getMetabolicInfo(callback: any) {
    return this.getDataWithSpinner(URL_SITE_DIET + 'GetMetabolicInfo', callback);
  }
  saveMetabolicInfo(payload: any, callback: any) {
    return this.putDataWithSpinner(URL_SITE_DIET + 'EditMetabolicInfo', payload, callback);
  }
  getcurrentMacro(mode: string, callback: any) {
    return this.getDataWithSpinner(URL_SITE_DIET + 'GetMetabolicInfoCalcType/' + mode, callback);
  }
  processItem(payload: any, callback: any) {
    return this.putDataWithSpinner(URL_SITE_DIET + 'ProcessItem', payload, callback);

  }
  deleteItem(payload: any, callback: any) {
    return this.putDataWithSpinner(URL_SITE_DIET + 'DeleteFoodItem', payload, callback);

  }
  getNutritionInfo(): Array<NutritionInfo> {
    return this.nutritionInfo;
  }
  setNutritionInfo(nutritionInfo: Array<NutritionInfo>) {
    this.nutritionInfo = nutritionInfo;
  }
}
