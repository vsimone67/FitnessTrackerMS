import { Reps } from './';

export class Exercise {
  ExerciseId: number;
  Name: string;
  Measure: string;
  SetId: number;
  Reps: Array<Reps>;
  ExerciseOrder: number;
  ExerciseNameId: number;
  AdditionalColumns: Array<string>;
  constructor() {
    this.Reps = new Array<Reps>();
    this.AdditionalColumns = new Array<string>();
  }
}
