namespace FitnessTracker.Application.Model.Diet
{
    public class MetabolicInfoDTO
    {     
        public int MetabolicInfoId { get; set; }
        public string macro { get; set; }
        public double cut { get; set; }
        public double maintain { get; set; }
        public double gain { get; set; }
        public double factor { get; set; }
    }
}
