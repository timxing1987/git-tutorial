using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using Cedar.Core.Data;
using Microsoft.Practices.Unity.Utility;
using MySql.Data.Entity;

namespace Cedar.Core.EntLib.Data
{
    public class MySqlDatabaseWrapper : Database
    {
        protected static DbConnection connection;
        private readonly ConnectionStringSettings connectionStringSettings;

        public MySqlDatabaseWrapper(Func<MySqlConnectionFactory> dataAccessor,
            string databaseName, ConnectionStringSettings connectionStringSettings)
        {
            Guard.ArgumentNotNull(dataAccessor, "dataAccessor");
            Guard.ArgumentNotNullOrEmpty(databaseName, "databaseName");
            Database = dataAccessor();
            DatabaseName = databaseName;
            this.connectionStringSettings = connectionStringSettings;
        }

        public MySqlConnectionFactory Database { get; }

        public override string DatabaseName { get; }

        public override DbConnection CreateConnection()
        {
            return Database.CreateConnection(connectionStringSettings.ConnectionString);
        }

        public int Execute(List<SqlObject> sql)
        {
            using (IDbConnection conn = Database.CreateConnection(connectionStringSettings.ConnectionString))
            {
                var transaction = conn.BeginTransaction();
                var row = 0;
                try
                {
                    row +=
                        sql.Sum(sqlObject => conn.Execute(sqlObject.Sql, sqlObject.Paramters, transaction, null, null));
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }

                return row;
            }
        }

        public override int Execute(string sql, dynamic param = null, DbTransaction transaction = null,
            CommandType commandType = CommandType.Text)
        {
            using (var con = Database.CreateConnection(connectionStringSettings.ConnectionString))
            {
                return param == null
                    ? con.Execute(sql, null, transaction, null, commandType)
                    : con.Execute(sql, (object) param, transaction, null, commandType);
            }
        }

        public override IEnumerable<T> Query<T>(string sql, dynamic param = null, DbTransaction transaction = null,
            CommandType commandType = CommandType.Text)
        {
            using (var con = Database.CreateConnection(connectionStringSettings.ConnectionString))
            {
                return con.Query<T>(sql, (object) param, transaction, true, null, commandType);
            }
        }

        public override IEnumerable<dynamic> Query(string sql, dynamic param = null,
            CommandType commandType = CommandType.Text)
        {
            using (var con = Database.CreateConnection(connectionStringSettings.ConnectionString))
            {
                con.Open();
                return con.Query(sql, (object) param, null, true, null, commandType);
            }
        }
    }

    /// <summary>
    /// </summary>
    public class SqlObject
    {
        /// <summary>
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// </summary>
        public object Paramters { get; set; }

        /// <summary>
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// </summary>
        public bool IsStrictResult { get; set; }
    }
}