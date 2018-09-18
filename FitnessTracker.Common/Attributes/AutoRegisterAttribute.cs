using System;

namespace FitnessTracker.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AutoRegisterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoRegisterAttribute"/> class.
        /// </summary>
        /// <param name="registerAsType">Type to use for registration</param>
        public AutoRegisterAttribute(Type registerAsType)
        {
            RegisterAsType = registerAsType;
        }

        /// <summary>
        /// Gets the type to use for registration.
        /// </summary>
        /// <value>The type of the register as.</value>
        public Type RegisterAsType { get; private set; }
    }
}