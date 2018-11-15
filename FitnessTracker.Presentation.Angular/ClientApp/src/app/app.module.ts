import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { NavagationComponent } from "../app/layout/navagation.component";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { SharedModule } from "./shared/shared.module";
import { WorkoutModule } from "./workout/workout.module";
import { NgxsReduxDevtoolsPluginModule } from "@ngxs/devtools-plugin";
import { NgxsLoggerPluginModule } from "@ngxs/logger-plugin";
import { NgxsModule } from "@ngxs/store";
import { WorkoutState } from "../app/workout/state/workout.state";
import { BodyInfoState } from "../app/workout/state/body-info.state";
import { DietModule } from "./diet/diet.module";
import { DietState } from "./diet/state/diet.state";
import { MetabolicInfoState } from "./diet/state/metabolic-info.state";
@NgModule({
  declarations: [AppComponent, NavagationComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    WorkoutModule,
    NgxsModule.forRoot([
      WorkoutState,
      BodyInfoState,
      DietState,
      MetabolicInfoState
    ]),
    // NgxsReduxDevtoolsPluginModule.forRoot(),
    // NgxsLoggerPluginModule.forRoot(),
    DietModule
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
