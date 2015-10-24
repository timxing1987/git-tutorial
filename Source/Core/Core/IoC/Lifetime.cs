namespace Cedar.Core.IoC
{
    /// <summary>
    ///     Control the lifetime of acticated service instance.
    /// </summary>
    public enum Lifetime
    {
        /// <summary>
        ///     The singleton service instance is got activated for each service activation attempts.
        /// </summary>
        Singleton = 1,

        /// <summary>
        ///     Always create a new service instance for each service activation attempts.
        /// </summary>
        Transient = 0
    }
}