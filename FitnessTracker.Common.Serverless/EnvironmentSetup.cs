using AutoMapper;
using EventBusAzure;
using FitnessTracker.Common.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace FitnessTracker.Common.Serverless
{
    /// <summary>
    /// class to emulate the DI environment of FitnessTracker as well as all config settings, etc.  Azure functions do not support DI right now so this is a way to get all the services and helper classes instantiated
    /// </summary>
    /// <typeparam name="T">Interface to map to</typeparam>
    /// <typeparam name="TH">Concrete class to create instance of and map to T</typeparam>
    public class EnvironmentSetup<T, TH>
    {
        public IMapper Mapper { get; set; }
        public T Service { get; set; }
        public IOptions<FitnessTrackerSettings> Settings { get; set; }

        public EnvironmentSetup(string appDir, IMapper mapper)
        {
            Mapper = mapper;
            Settings = Options.Create(new FitnessTrackerSettings());

            var config = new ConfigurationBuilder()
                   .SetBasePath(appDir)
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();

            Settings.Value.ConnectionString = config.GetValue<string>("ConnectionString");

            Settings.Value.AzureConnectionSettings = new AzureConnectionSettings
            {
                ConnectionString = config.GetValue<string>("AzureConnectionSettings:ConnectionString"),
                TopicName = config.GetValue<string>("AzureConnectionSettings:TopicName"),
                SubscriptionClientName = config.GetValue<string>("AzureConnectionSettings:SubscriptionClientName")
            };

            Service = (T)Activator.CreateInstance(typeof(TH), Settings);  // using activator because there are two different implementations of TH that need constructor arguments (WorkoutDB, DietDB) and new TH() will not work
        }
    }
}