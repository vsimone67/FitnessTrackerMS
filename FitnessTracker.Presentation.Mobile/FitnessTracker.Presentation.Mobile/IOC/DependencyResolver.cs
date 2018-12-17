using SimpleInjector;
using System;

namespace FitnessTracker.Presentation.Mobile.IOC
{
    public static class DependencyResolver
    {
        private static readonly Container _container = new Container();

        public static void Register(Type service, Type implementation)

        {
            _container.Register(service, implementation);
        }

        public static void Register<TService, TImplementation>()
        {
            Register(typeof(TService), typeof(TImplementation));
        }

        public static void Register<TImplementation>()
        {
            _container.Register(typeof(TImplementation));
        }

        public static T Resolve<T>() where T : class
        {
            return _container.GetInstance<T>();
        }

        public static object Resolve(Type implementation)
        {
            return _container.GetInstance(implementation);
        }
    }
}