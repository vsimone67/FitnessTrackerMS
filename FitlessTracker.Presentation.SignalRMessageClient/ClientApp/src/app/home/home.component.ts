import { Component, OnInit } from '@angular/core';
import { AppsettingsService } from '../services/appSettings.service';
import { SignalrHubService } from '../services/signlarHub.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  protected messages: string[] = [];

  protected _workoutConnection: SignalrHubService;
  protected _dietConnection: SignalrHubService;
  constructor(protected _appSettings: AppsettingsService) {
    // cannot inject the hub service becuase we need two distinct connects to the hub and angular injection is a singleton
    this._workoutConnection = new SignalrHubService();
    this._dietConnection = new SignalrHubService();
  }

  async ngOnInit() {
    await this._appSettings.LoadConfigData('assets/appsettings.json');
    await this.CreateWorkoutConnection();
    await this.CreateDietConnection();
  }

  async CreateWorkoutConnection() {
    this._workoutConnection.URL = this._appSettings.GetValue('hubConnections')['workoutHubURL'];
    await this._workoutConnection.Connect();

    if (this._workoutConnection.IsConnected) {
      this.RegisterWorkoutEvents();
    }
  }
  protected RegisterWorkoutEvents(): void {
    this._workoutConnection.Hub.on('WorkoutCompleted', (data: any) => {
      const received = `Workout Hub: Workout Completed`;
      this.messages.push(received);
    });

    this._workoutConnection.Hub.on('NewWorkoutAdded', (data: any) => {
      const received = `Workout Hub: New Workout Added, ID: ${data.addedWorkout.name}`;
      this.messages.push(received);
    });

    this._workoutConnection.Hub.on('BodyInfoSaved', (data: any) => {
      const received = `Workout Hub: New BodyInfo Saved`;
      this.messages.push(received);
    });
  }

  async CreateDietConnection() {
    this._dietConnection.URL = this._appSettings.GetValue('hubConnections')['dietHubURL'];
    await this._dietConnection.Connect();

    if (this._dietConnection.IsConnected) {
      this.RegisterDietEvents();
    }
  }

  protected RegisterDietEvents(): void {
    this._dietConnection.Hub.on('MenuSaved', (data: any) => {
      const received = `Diet Hub: Menu Saved:`;
      this.messages.push(received);
    });

    this._dietConnection.Hub.on('AddNewFood', (data: any) => {
      const received = `Diet Hub: New Food Added To The Menu: ${data.addedFoodItem.item}`;
      this.messages.push(received);
    });

    this._dietConnection.Hub.on('DeleteFoodItem', (data: any) => {
      const received = `Diet Hub: Food Item Was Deleted: ${data.deletedFoodItem.item}`;
      this.messages.push(received);
    });

    this._dietConnection.Hub.on('EditMetabolicInfo', (data: any) => {
      const received = `Diet Hub: Edit Metabolic Info Completed`;
      this.messages.push(received);
    });
  }
}
