using Cedar.Core.IoC;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.Data
{
    /// <summary>
    ///     The factory to create <see cref="T:Cedar.Core.Data.Database" /> by the specified connection string name.
    /// </summary>
    public static class DatabaseFactories
    {
        /// <summary>
        ///     Gets the database.
        /// </summary>
        /// <param name="databaseName">Name of the connection string.</param>
        /// <returns>The <see cref="T:Cedar.Core.Data.Database" />.</returns>
        public static Database GetDatabase(string databaseName)
        {
            Guard.ArgumentNotNullOrEmpty(databaseName, "databaseName");
            var serviceLocator = ServiceLocatorFactory.GetServiceLocator(null);
            var service = serviceLocator.GetService<IDatabaseFactory>(null);
            return service.GetDatabase(databaseName);
        }

        /// <summary>
        ///     Gets the database.
        /// </summary>
        /// <returns>The <see cref="T:Cedar.Core.Data.Database" />.</returns>
        public static Database GetDatabase()
        {
            var serviceLocator = ServiceLocatorFactory.GetServiceLocator(null);
            var service = serviceLocator.GetService<IDatabaseFactory>(null);
            return service.GetDatabase();
        }
    }
}