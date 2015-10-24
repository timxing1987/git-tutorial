using System;
using Cedar.Core.Configuration;

namespace Cedar.Core.ApplicationContexts.Configuration
{
    /// <summary>
    ///     Define the call context locator data.
    /// </summary>
    public class CallContextLocatorData : ContextLocatorDataBase
    {
        /// <summary>
        ///     Get the delegate to create provider instance.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The delegate to create provider instance.</returns>
        public override Func<IContextLocator> GetProviderCreator(ServiceLocatableSettings settings)
        {
            return () => new CallContextLocator();
        }
    }
}