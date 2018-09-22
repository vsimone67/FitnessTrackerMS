using AutoMapper;
using FitnessTracker.Application.MappingProfile;
using FitnessTracker.Common.Attributes;
using FitnetssTracker.Application.Common;
using FitnetssTracker.Application.Common.Processor;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

using System.Linq;

namespace FitnessTracker.Workout.Service.IOC
{
    public class SimpleInjectorBootstrapper
    {
        private Container _container;

        public Container Container { get { return _container; } }

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
        /// Automatically registers any dependencies in Eeverest assemblies that have the AutoRegister attribute on the class.
        /// </summary>
        /// <returns></returns>
        public SimpleInjectorBootstrapper AutoRegisterFitnessTrackerDependencies(string nameSpace)
        {
            var fitnessTrackerAssemblyDefinedTypes = LibraryManager.GetReferencingAssemblies(nameSpace)
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
        public SimpleInjectorBootstrapper RegisterCommandAndQueryHandlers(string nameSpace)
        {
            // Get all the fitnesstracker assemblies
            var fitnessTrackerAssemblies = LibraryManager.GetReferencingAssemblies(nameSpace);

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