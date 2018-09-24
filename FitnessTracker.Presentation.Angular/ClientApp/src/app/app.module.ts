import { NgModule, NgZone } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { createStore, Store, StoreEnhancer } from 'redux';

import { NavagationComponent } from '../layout';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from '../shared/shared.module';
import { WorkoutModule } from '../workout/workout.module';
import { DietModule } from '../diet/diet.module';

import { AppState, AppStore, rootReducer, AppComponent } from '../app';


let devtools: StoreEnhancer<AppState> =
  window['devToolsExtension'] ?
  window['devToolsExtension']() : f => f;

let store: Store<AppState> = createStore<AppState>(rootReducer, devtools);

export function _ngReduxFactory() {
  return store;
}
@NgModule({
  imports: [BrowserModule, AppRoutingModule, SharedModule, WorkoutModule, DietModule],  
  

  declarations: [AppComponent, NavagationComponent],
  bootstrap: [AppComponent],
  providers: [{ provide: AppStore, useFactory: _ngReduxFactory}]  
})
export class AppModule { }
