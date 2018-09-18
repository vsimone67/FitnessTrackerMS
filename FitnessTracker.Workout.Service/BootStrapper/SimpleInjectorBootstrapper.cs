using AutoMapper;
using FitnessTracker.Application.MappingProfile;
using FitnessTracker.Common.Attributes;
using FitnetssTracker.Application.Common;
using FitnetssTracker.Application.Common.Processor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

//using SimpleInjector.Integration.AspNetCore.Mvc;
using System.Linq;

namespace FitnessTracker.Workout.Service.IOC
{
    public class SimpleInjectorBootstrapper
    {
        private Container _container;

        public SimpleInjectorBootstrapper()
        {
            InitContainer();
        }

        /// <summary>
        /// Gets a new instance of the SimpleInjectorBootstrapper.
        /// </summary>
        /// <returns></returns>
        public static SimpleInjectorBootstrapper New()
        {
            return new SimpleInjectorBootstrapper();
        }

        /// <summary>
        /// Intializes the Web API specific dependencies. This includes registering the controllers, logger,
        /// global exception logging, and a per web api request logger.
        /// </summary>
        /// <param name="httpConfiguration">The http configuration.</param>
        /// <returns></returns>
        public SimpleInjectorBootstrapper ForNETCore(IServiceCollection services)
        {
            //services.AddSingleton<IControllerActivator>(
            //  new SimpleInjectorControllerActivator(_container));
            //services.AddSingleton<IViewComponentActivator>(
            //    new SimpleInjectorViewComponentActivator(_container));
            //services.UseSimpleInjectorAspNetRequestScoping(_container);

            return this;
        }

        /// <summary>
        /// Automatically registers any dependencies in Eeverest assemblies that have the AutoRegister attribute on the class.
        /// </summary>
        /// <returns></returns>
        public SimpleInjectorBootstrapper AutoRegisterFitnessTrackerDependencies()
        {
            // Get all the defined types in the Everest assemblies
            var fitnessTrackerAssemblyDefinedTypes = LibraryManager.GetReferencingAssemblies("FitnessTracker")
                .SelectMany(an => an.DefinedTypes);

            // Get types with AutoRegisterAttribute in assemblies
            var typesWithAutoRegisterAttribute =
                from t in fitnessTrackerAssemblyDefinedTypes
                let attributes = t.GetCustomAttributes(typeof(AutoRegisterAttribute), true)
                where attributes != null && attributes.Any()
                select new { Type = t, Attributes = attributes.Cast<AutoRegisterAttribute>() };

            // Loop through types to register with IoC
            foreach (var typeToRegister in typesWithAutoRegisterAttribute)
            {
                foreach (var attribute in typeToRegister.Attributes)
                {
                    _container.Register(attribute.RegisterAsType, typeToRegister.Type.AsType());
                }
            }

            return this;
        }

        public SimpleInjectorBootstrapper RegisterApplicationSettings(IConfigurationRoot configuration)
        {
            //var settings = new FitnessTracker.ApplicationSettings.ApplicationSettings(configuration);
            //_container.Register<IApplicationSettings>(() => settings);

            return this;
        }

        /// <summary>
        /// Registers the mapping engine.
        /// </summary>
        /// <returns></returns>
        public SimpleInjectorBootstrapper RegisterMappingEngine()
        {
            IMapper mapperConfig = GetMapperConfiguration();
            _container.Register<IMapper>(() => mapperConfig, Lifestyle.Singleton);  // Register automapper config and mappings

            return this;
        }

        /// <summary>
        /// Registers the command and query handlers.
        /// </summary>
        /// <returns></returns>
        public SimpleInjectorBootstrapper RegisterCommandAndQueryHandlers()
        {
            // Get all the fitnesstracker assemblies
            var fitnessTrackerAssemblies = LibraryManager.GetReferencingAssemblies("FitnessTracker");

            // Look in all assemblies and register all implementations of ICommandHandler<in TCommand>
            _container.Register(typeof(ICommandHandler<,>), fitnessTrackerAssemblies);

            // Look in all assemblies and register all implementations of IQueryHandler<in TQuery, TResult>
            _container.Register(typeof(IQueryHandler<,>), fitnessTrackerAssemblies);

            // Register the command and query processors to reduce the amount of handler references in the Controllers
            _container.Register<ICommandProcessor>(() => new CommandProcessor(_container.GetInstance));
            _container.Register<IQueryProcessor>(() => new QueryProcessor(_container.GetInstance));

            return this;
        }

        /// <summary>
        /// Verifies the Simple Injector container is setup correctly.  Throws an exception if there is an error.
        /// </summary>
        /// <returns></returns>
        public SimpleInjectorBootstrapper Verify()
        {
            _container.Verify();

            return this;
        }

        private void InitContainer()
        {
            if (_container == null)
            {
                _container = new Container();
            }
        }

        protected IMapper GetMapperConfiguration()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new WorkoutMappingProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }
}