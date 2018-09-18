using System.Collections.Generic;

namespace FitnessTracker.Domain.Diet
{
    public class CurrentMacros
    {
        #region CONSTANTS

        public const string Weight = "WEIGHT";
        public const string DCR = "DCR";
        public const string Calories = "CALORIES";
        public const string Protein = "PROTEINPER";
        public const string Carbs = "CARBSPER";
        public const string Fat = "FATPER";

        #endregion CONSTANTS

        public double calories { get; set; }
        public int caloriesFactor { get; set; }
        public double protein { get; set; }
        public double proteinFactor { get; set; }
        public double carbs { get; set; }
        public double carbsFactor { get; set; }
        public double fat { get; set; }
        public double fatFactor { get; set; }
        public double weight { get; set; }
        public int weightFactor { get; set; }
        public double dcr { get; set; }
        public int dcrFactor { get; set; }

        public void HydrateFromMetabolicInfo(List<MetabolicInfo> currentMacroList, string mode)
        {
            weight = (double)currentMacroList.Find(exp => exp.macro == CurrentMacros.Weight).GetPropertyValue(mode);
            weightFactor = (int)currentMacroList.Find(exp => exp.macro == CurrentMacros.Weight).factor;
            dcr = (double)currentMacroList.Find(exp => exp.macro == CurrentMacros.DCR).GetPropertyValue(mode);
            dcrFactor = (int)currentMacroList.Find(exp => exp.macro == CurrentMacros.DCR).factor;
            calories = (double)currentMacroList.Find(exp => exp.macro == CurrentMacros.Calories).GetPropertyValue(mode);
            caloriesFactor = (int)currentMacroList.Find(exp => exp.macro == CurrentMacros.Calories).factor;
            protein = (double)currentMacroList.Find(exp => exp.macro == CurrentMacros.Protein).GetPropertyValue(mode); ;
            proteinFactor = currentMacroList.Find(exp => exp.macro == CurrentMacros.Protein).factor;
            carbs = (double)currentMacroList.Find(exp => exp.macro == CurrentMacros.Carbs).GetPropertyValue(mode); ;
            carbsFactor = currentMacroList.Find(exp => exp.macro == CurrentMacros.Carbs).factor;
            fat = (double)currentMacroList.Find(exp => exp.macro == CurrentMacros.Fat).GetPropertyValue(mode); ;
            fatFactor = currentMacroList.Find(exp => exp.macro == CurrentMacros.Fat).factor;
        }
    }
}