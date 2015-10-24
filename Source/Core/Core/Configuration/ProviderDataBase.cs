#region

using System;
using Cedar.Core.IoC;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     Provider based configuraiton element classes should be derived from this class.
    /// </summary>
    /// <typeparam name="TProvider">The type of the provider.</typeparam>
    public abstract class ProviderDataBase<TProvider> : NameTypeConfigurationElement
    {
        /// <summary>
        ///     Gets the lifetime.
        /// </summary>
        /// <value>
        ///     The lifetime.
        /// </value>
        public virtual Lifetime Lifetime
        {
            get { return Lifetime.Singleton; }
        }

        /// <summary>
        ///     Get the delegate to create provider instance.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The delegate to create provider instance.</returns>
        public virtual Func<TProvider> GetProviderCreator(ServiceLocatableSettings settings)
        {
            return null;
        }
    }
}