using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Common.Attributes;
using Microsoft.Extensions.Configuration;

namespace FitnessTracker.ApplicationSettings
{
    public class ApplicationSettings : IApplicationSettings
    {
        protected IConfiguration _config;

        public ApplicationSettings(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString(string connectionString)
        {
            return _config.GetConnectionString(connectionString);
        }

        public string GetConfigValue(string key)
        {
            return _config.GetSection(key).Value;
        }
    }
}