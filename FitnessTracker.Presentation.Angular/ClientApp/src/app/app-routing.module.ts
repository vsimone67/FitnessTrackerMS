import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogWorkoutComponent, AddBodyInfoComponent } from '../app/workout/components'
import {AddWorkoutComponent} from './/workout/components/add-workout/add-workout.component'
import { CreateDietComponent } from '../app/diet/components/create-diet/create-diet.component'

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
