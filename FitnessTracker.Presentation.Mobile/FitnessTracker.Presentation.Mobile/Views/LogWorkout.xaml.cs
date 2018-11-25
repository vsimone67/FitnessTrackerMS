using FitnessTracker.Application.Model.Workout;
using FitnessTracker.Mobile.Models;
using FitnessTracker.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitnessTracker.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogWorkout : BaseView
    {
        protected LogWorkoutViewModel viewModel;
        private int _currentRow;

        public LogWorkout()
        {
            InitializeComponent();

            _currentRow = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateToolBar();

            viewModel = (LogWorkoutViewModel)BindingContext;

            if (viewModel.Workouts.Count == 0 && !viewModel.IsBusy)
                viewModel.LoadWorkoutCommand.Execute(null);

            // Keep Screen on
            viewModel.TurnSleepOffCommand.Execute(null);

            // Reinitialize two viewmodel variables in case we redo or pick another workout mid stream
            viewModel.IsFirstClick = false;
            viewModel.NumberOfClicks = 0;
        }

        #region Grid

        protected void FillGrid()
        {
            _currentRow = 0;

            if (selectedWorkoutGrid.Children.Count > 0)  // Clear all items if the grid was already drawn
            {
                selectedWorkoutGrid.Children.Clear();
            }
            CreateAndFillWorkoutGrid();
        }

        private void CreateAndFillWorkoutGrid()
        {
            foreach (SetDisplayDTO set in viewModel.SelectedWorkout.Set)  // go through all sets (set -> Exercise -> Rep)
            {
                WriteSetHeader(set);  // write the set header info with reps
                WriteExerciseInfo(set);    // write exercises for the set
            }
        }

        private void WriteSetHeader(SetDisplayDTO set)
        {
            AddGridItem(set.Name + " (" + set.DisplayReps.Count.ToString() + ")", Color.White, Color.Black, 0, _currentRow); // write the header info

            int currentColumn = 1;
            foreach (RepsDisplayDTO rep in set.DisplayReps) // write all reps that are part of a set
            {
                AddGridItem(rep.Name, Color.White, Color.Black, currentColumn, _currentRow);  // Write the rep name
                currentColumn++;  // set next grid column
            }
        }

        private void WriteExerciseInfo(SetDisplayDTO set)
        {
            _currentRow++;
            foreach (ExerciseDisplayDTO exercise in set.Exercise)
            {
                int currentColumn = 1;

                // Write Workout data
                AddGridItem(exercise.Name, Color.Black, Color.Wheat, 0, _currentRow);  // Write the exercise name

                foreach (RepsDisplayDTO reps in exercise.Reps)  // write all rep data for each exercise
                {
                    AddGridItemTap(exercise.Measure + ": " + reps.Weight + " (" + reps.TimeToNextExercise + ")", reps.TimeToNextExercise, Color.Black, Color.Wheat, currentColumn, _currentRow);  // write rep information
                    currentColumn++;  // set next grid column
                }

                _currentRow++;  // set next grid row
            }
        }

        private void AddGridItemTap(string text, string timeToNextExercise, Color textColor, Color backgroundColor, int currentColumn, int currentRow)
        {
            LabelExt gridLabel = CreateLabel(text, textColor, backgroundColor);
            gridLabel.TimeToNextExercise = timeToNextExercise;

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                OnCellTap(s, e);
            };
            gridLabel.GestureRecognizers.Add(tapGestureRecognizer);

            // Add column to workout grid
            selectedWorkoutGrid.Children.Add(gridLabel, currentColumn, currentRow);
        }

        private void AddGridItem(string text, Color textColor, Color backgroundColor, int currentColumn, int currentRow)
        {
            LabelExt gridLabel = CreateLabel(text, textColor, backgroundColor);

            // Add column to workout grid
            selectedWorkoutGrid.Children.Add(gridLabel, currentColumn, currentRow);
        }

        private LabelExt CreateLabel(string text, Color textColor, Color backgroundColor)
        {
            LabelExt gridLabel = new LabelExt()
            {
                Text = text,
                TextColor = textColor,
                BackgroundColor = backgroundColor
            };

            return gridLabel;
        }

        // user taps a cell to complete a exercise/rep
        private void OnCellTap(object sender, EventArgs eventArgs)
        {
            LabelExt label = (LabelExt)sender;
            label.BackgroundColor = Color.Green;  // Change the background color to green (done)

            if (!label.IsClicked)  // check to see if this column has been
            {
                viewModel.NumberOfClicks++;  // increment the number of clicks for the workout, this is to determien
                label.IsClicked = true;  // set this cell as clicked
            }

            if (!viewModel.IsFirstClick) // has the workout started, the first click with signal the start of the workout.  If it is get the starting time
            {
                viewModel.IsFirstClick = true;
                viewModel.StartWorkoutTime = DateTime.Now;
            }

            if (int.Parse(label.TimeToNextExercise) > 0) // only allow the timer windows to appear for a rest time greater than 0
            {
                viewModel.StartRestTimerCommand.Execute(int.Parse(label.TimeToNextExercise));
            }

            // Workout is over popup the save
            if (viewModel.NumberOfClicks == viewModel.MaxExercisesPerWorkout)
            {
                viewModel.WorkoutEndedCommand.Execute(null);
            }

        }

        #endregion Grid

        #region Control Events

        private void WorkoutSelected(object sender, EventArgs e)
        {
            WorkoutDTO workout = (WorkoutDTO)((Xamarin.Forms.Picker)sender).SelectedItem;

            if (!viewModel.IsBusy)
            {
                viewModel.GetWorkoutCommand.Execute(workout.WorkoutId);
                FillGrid();

                if (viewModel.isIncreaseWeight)
                {
                    DisplayAlert("Workout", "Increase Weight", "OK");
                }
            }
        }

        #endregion Control Events
    }
}