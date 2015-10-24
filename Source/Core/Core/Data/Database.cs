using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Cedar.Core.Data
{
    /// <summary>
    ///     A abstract database which is used to perform basic data access operation.
    /// </summary>
    public abstract class Database
    {
        /// <summary>
        ///     Gets the name of the database.
        /// </summary>
        /// <value>
        ///     The name of the database.
        /// </value>
        public abstract string DatabaseName { get; }

        /// <summary>
        ///     Gets the database provider factory.
        /// </summary>
        /// <value>
        ///     The database provider factory.
        /// </value>
        /// <summary>
        ///     Creates the connection.
        /// </summary>
        /// <returns></returns>
        public abstract DbConnection CreateConnection();

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public abstract int Execute(string sql, dynamic param = null, DbTransaction transaction = null,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public abstract IEnumerable<dynamic> Query(string sql, dynamic param = null,
            CommandType commandType = CommandType.Text);

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public abstract IEnumerable<T> Query<T>(string sql, dynamic param = null, DbTransaction transaction = null,
            CommandType commandType = CommandType.Text);
    }
}