﻿using System.Collections.Generic;

namespace FitnessTracker.Application.Common.Interfaces
{
    public interface IApplicationSettings
    {
        string GetConnectionString(string connectionString);

        string GetConfigValue(string key);

        List<string> GetSectionAsList(string section);
    }
}