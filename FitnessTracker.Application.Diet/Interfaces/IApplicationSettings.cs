
namespace FitnessTracker.Application.Interfaces
{
    public interface IApplicationSettings
    {
        string GetConnectionString(string connectionString);
        string GetConfigValue(string key);
    }
}
