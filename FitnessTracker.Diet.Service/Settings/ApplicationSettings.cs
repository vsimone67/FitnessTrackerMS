using FitnessTracker.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace FitnessTracker.Diet.ApplicationSettings
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

        public List<string> GetSectionAsList(string section)
        {
            return _config.GetSection(section).Get<List<string>>();
        }
    }
}