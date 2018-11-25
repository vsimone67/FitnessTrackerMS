import { Exercise, Reps } from './';

export class set {
  public Name: string;
  public SetId: number;
  public SetNameId: number;
  public DisplayReps: Array<Reps>;
  public Exercise: Array<Exercise>;
  public SetOrder: number;

  constructor() {
    this.Exercise = new Array<Exercise>();
    this.Name = '';
  }
}
