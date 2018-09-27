using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FitnessTracker.Mobile.Models
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
