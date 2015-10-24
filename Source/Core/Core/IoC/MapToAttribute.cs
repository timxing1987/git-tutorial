#region

using System;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    ///     This attribute is used to build mapping between service interface and implementation type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MapToAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        /// <param name="registeredType"></param>
        /// <param name="quality"></param>
        public MapToAttribute(Type registeredType, int quality = 0)
        {
            Guard.ArgumentNotNull(registeredType, "registeredType");
            RegisteredType = registeredType;
            Quality = quality;
            Lifetime = Lifetime.Transient;
        }

        /// <summary>
        ///     Gets service interface type.
        /// </summary>
        /// <value>
        ///     The service interface type.
        /// </value>
        public Type RegisteredType { get; private set; }

        /// <summary>
        ///     Gets or sets the lifetime.
        /// </summary>
        /// <value>
        ///     The lifetime.
        /// </value>
        public Lifetime Lifetime { get; set; }

        /// <summary>
        ///     Gets the mapping quality.
        /// </summary>
        /// <value>
        ///     The mapping quality.
        /// </value>
        public int Quality { get; private set; }
    }
}