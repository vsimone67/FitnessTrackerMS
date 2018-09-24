import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LogWorkoutComponent, AddBodyInfoComponent, AddWorkoutComponent } from '../workout/components';
import { CreateDietComponent } from '../diet/components';

// *** Add Routes Here ***
const routes: Routes = [
  { path: 'addBodyInfo', component: AddBodyInfoComponent },
  { path: 'createDiet', component: CreateDietComponent },
  { path: 'logWorkout', component: LogWorkoutComponent },
  { path: 'addWorkout', component: AddWorkoutComponent },
  { path: '', component: LogWorkoutComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
