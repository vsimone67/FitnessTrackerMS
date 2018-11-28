using Xamarin.Forms;

namespace FitnessTracker.Presentation.Mobile.Models
{
    public class LabelExt : Label
    {
        public string TimeToNextExercise { get; set; }
        public bool IsClicked { get; set; }

        public LabelExt()
        {
            IsClicked = false;
        }
    }
}