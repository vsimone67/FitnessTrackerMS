import { set } from './';

export class Workout {
  public WorkoutId: number;
  public Name: string;
  public Set: Array<set>;
  public Phase: string;
  public Duration: number;

  constructor() {
    this.Set = new Array<set>();
    this.Name = '';
  }
}
