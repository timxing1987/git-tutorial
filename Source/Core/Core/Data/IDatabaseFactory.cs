namespace Cedar.Core.Data
{
    /// <summary>
    ///     All database factory classes must implement this interface.
    /// </summary>
    public interface IDatabaseFactory
    {
        /// <summary>
        ///     Gets the database.
        /// </summary>
        /// <param name="databaseName">Name of the connection string.</param>
        /// <returns>The <see cref="T:Cedar.Core.Data.Database" />.</returns>
        Database GetDatabase(string databaseName);

        /// <summary>
        ///     Gets the database.
        /// </summary>
        /// <returns>The <see cref="T:Cedar.Core.Data.Database" />.</returns>
        Database GetDatabase();
    }
}