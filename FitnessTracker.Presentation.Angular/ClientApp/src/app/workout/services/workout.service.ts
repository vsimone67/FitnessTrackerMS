import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService, EventService } from '../../shared/services';
import { Workout, BodyInfo } from '../models';
import { AppsettingsService } from '../../shared/services'

@Injectable({
  providedIn: 'root'
})

export class WorkoutService extends BaseService {
  constructor(http: Http, eventService: EventService, appSettings: AppsettingsService) {
    super(http, eventService, appSettings);

    this.isSettingsLoaded = false;
  }

  async saveDailyWorkout(workout: Workout, callback: any) {
    await this.loadConfiguration('workoutServiceURL');
    return this.putDataWithSpinner(this.serviceURL + 'SaveDailyWorkout', workout, callback);
  }
  async saveWorkout(workout: Workout, callback: any) {
    await this.loadConfiguration('workoutServiceURL');
    return this.putDataWithSpinner(this.serviceURL + 'SaveWorkout', workout, callback);
  }
  async saveBodyInfo(bodyInfo: BodyInfo, callback: any) {
    await this.loadConfiguration('workoutServiceURL');
    return this.putDataWithSpinner(this.serviceURL + 'SaveBodyInfo', bodyInfo, callback);
  }
  async getWorkouts(callback: any) {
    await this.loadConfiguration('workoutServiceURL');    
    this.getDataWithSpinner(this.serviceURL + 'GetWorkouts', callback);
  }
  async getWorkout(id: number, callback: any) {
    await this.loadConfiguration('workoutServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetWorkoutForDisplay/' + id.toString(), callback);
  }
  async getBodyInfo(callback: any) {
    await this.loadConfiguration('workoutServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetBodyInfo', callback);
  }
  async getSets(callback: any) {
    await this.loadConfiguration('workoutServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetSets', callback);
  }
  async getExercises(callback: any) {
    await this.loadConfiguration('workoutServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetExercises', callback);
  }
  async getReps(callback: any) {
    await this.loadConfiguration('workoutServiceURL');
    return this.getDataWithSpinner(this.serviceURL + 'GetReps', callback);
  }
}
