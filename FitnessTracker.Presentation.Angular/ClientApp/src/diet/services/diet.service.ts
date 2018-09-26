import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { BaseService, EventService } from '../../shared/services';
import { NutritionInfo } from '../models';

import { AppsettingsService } from '../../shared/services'

@Injectable()
export class DietService extends BaseService {
  protected nutritionInfo: Array<NutritionInfo>;
  protected serviceURL: string;

  constructor(http: Http, eventService: EventService, appSettings: AppsettingsService) {
    super(http, eventService, appSettings);

    this.nutritionInfo = new Array<NutritionInfo>();
    this.isSettingsLoaded = false;
  }

  async getDietItems(callback: any) {
    await this.loadConfiguration('dietServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetSavedMenuItems', callback);
  }
  async getColumns(callback: any) {
    await this.loadConfiguration('dietServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetColumns', callback);
  }
  async saveMenu(payload: any, callback: any) {
    await this.loadConfiguration('dietServiceURL');
    return this.putDataWithSpinner(this.serviceURL + 'SaveMenu', payload, callback);
  }
  async getMetabolicInfo(callback: any) {
    await this.loadConfiguration('dietServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetMetabolicInfo', callback);
  }
  async saveMetabolicInfo(payload: any, callback: any) {
    await this.loadConfiguration('dietServiceURL');
    return this.putDataWithSpinner(this.serviceURL + 'EditMetabolicInfo', payload, callback);
  }
  async getcurrentMacro(mode: string, callback: any) {
    await this.loadConfiguration('dietServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetMetabolicInfoCalcType/' + mode, callback);
  }
  async processItem(payload: any, callback: any) {
    await this.loadConfiguration('dietServiceURL');
    return this.putDataWithSpinner(this.serviceURL + 'ProcessItem', payload, callback);
  }
  async deleteItem(payload: any, callback: any) {
    await this.loadConfiguration('dietServiceURL');
    return this.putDataWithSpinner(this.serviceURL + 'DeleteFoodItem', payload, callback);
  }

  getNutritionInfo(): Array<NutritionInfo> {
    this.loadConfiguration('dietServiceURL');
    return this.nutritionInfo;
  }
  async setNutritionInfo(nutritionInfo: Array<NutritionInfo>) {
    await this.loadConfiguration('dietServiceURL');
    this.nutritionInfo = nutritionInfo;
  }
}
