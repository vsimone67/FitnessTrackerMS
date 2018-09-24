import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService, EventService } from '../../shared/services';
import { Workout, BodyInfo } from '../models';
import { SiteConstants } from '../../shared/models';

const URL_SITE_WORKOUTS = SiteConstants.apiServer + '/api/Workout/';

@Injectable()
export class WorkoutService extends BaseService {
  constructor(public _http: Http, public _eventService: EventService) { super(_http, _eventService); }

  saveDailyWorkout(workout: Workout, callback: any) {
    return this.putDataWithSpinner(URL_SITE_WORKOUTS + 'SaveDailyWorkout', workout, callback);
  }
  saveWorkout(workout: Workout, callback: any) {
    return this.putDataWithSpinner(URL_SITE_WORKOUTS + 'SaveWorkout', workout, callback);
  }
  saveBodyInfo(bodyInfo: BodyInfo, callback: any) {
    return this.putDataWithSpinner(URL_SITE_WORKOUTS + 'SaveBodyInfo', bodyInfo, callback);
  }
  getWorkouts(callback: any) {
    this.getDataWithSpinner(URL_SITE_WORKOUTS + 'GetWorkouts', callback);
  }
  getWorkout(id: number, callback: any) {

    return this.getDataWithSpinner(URL_SITE_WORKOUTS + 'GetWorkoutForDisplay/' + id.toString(), callback);
  }
  getBodyInfo(callback: any) {
    return this.getDataWithSpinner(URL_SITE_WORKOUTS + 'GetBodyInfo', callback);
  }
  getSets(callback: any) {
    return this.getDataWithSpinner(URL_SITE_WORKOUTS + 'GetSets', callback);
  }
  getExercises(callback: any) {
    return this.getDataWithSpinner(URL_SITE_WORKOUTS + 'GetExercises', callback);
  }
  getReps(callback: any) {
    return this.getDataWithSpinner(URL_SITE_WORKOUTS + 'GetReps', callback);
  }

}
