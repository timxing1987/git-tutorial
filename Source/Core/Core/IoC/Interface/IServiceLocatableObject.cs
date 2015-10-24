namespace Cedar.Core.IoC
{
    /// <summary>
    ///     Mark the class canbe locatable
    /// </summary>
    public interface IServiceLocatableObject
    {
        /// <summary>
        ///     Gets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        IServiceLocator ServiceLocator { get; }
    }
}