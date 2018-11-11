import { DailyWorkoutInfo } from ".";

export class DailyWorkout {
  public DailyWorkoutId: number;
  public WorkoutDate: Date;
  public Phase: string;
  public WorkoutId: number;
  public Duration: number;
  public DailyWorkoutInfo: Array<DailyWorkoutInfo>;
}
