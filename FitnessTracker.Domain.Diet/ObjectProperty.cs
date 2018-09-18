using System.Linq;
using System.Reflection;

namespace FitnessTracker.Domain.Diet
{
    public static class ObjectPropertyExtentionMethod
    {
        public static object GetPropertyValue(this object car, string propertyName)
        {
            return car.GetType().GetRuntimeProperties()
               .Single(pi => pi.Name == propertyName)
               .GetValue(car, null);
        }
    }
}