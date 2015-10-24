namespace Cedar.Core.IoC
{
    /// <summary>
    ///     All of service locator configurator classes must implement this interface.
    /// </summary>
    public interface IServiceLocatorConfigurator
    {
        /// <summary>
        ///     Configures the specified service locator.
        /// </summary>
        /// <param name="serviceLocator">The service locator.</param>
        void Configure(IServiceLocator serviceLocator);
    }
}