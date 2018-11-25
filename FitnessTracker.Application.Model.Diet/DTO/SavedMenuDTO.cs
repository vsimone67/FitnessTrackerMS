namespace FitnessTracker.Application.Model.Diet
{
    public class SavedMenuDTO
    {
        public int Id { get; set; }
        public double Serving { get; set; }
        public int ItemId { get; set; }
        public int MealId { get; set; }
    }
}