using System;

namespace FitnessTracker.Application.Common.Processor
{
    /// <summary>
    /// A delegate to hold a reference to a dependency resolver's GetInstance (or similar method) to resolve an instance to a specific type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public delegate dynamic DependencyResolverGetInstance(Type type);
}