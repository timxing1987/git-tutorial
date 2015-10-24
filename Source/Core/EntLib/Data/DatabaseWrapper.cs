
using System;
using System.Data;
using System.Data.Common;
using Cedar.Core.Data;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.EntLib.Data
{
    internal class DatabaseWrapper : Database
    {
        private string databaseName;

        public Microsoft.Practices.EnterpriseLibrary.Data.Database Database
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the database provider factory.
        /// </summary>
        /// <value>
        /// The database provider factory.
        /// </value>
        public override DbProviderFactory DbProviderFactory
        {
            get
            {
                return Database.DbProviderFactory;
            }
        }

        /// <summary>
        /// Gets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public override string DatabaseName
        {
            get
            {
                return databaseName;
            }
        }

        public DatabaseWrapper(Func<Microsoft.Practices.EnterpriseLibrary.Data.Database> dataAccessor, string databaseName)
        {
            Guard.ArgumentNotNull(dataAccessor, "dataAccessor");
            Guard.ArgumentNotNullOrEmpty(databaseName, "databaseName");
            Database = dataAccessor();
            this.databaseName = databaseName;
        }

        public override int ExecuteNonQuery(DbCommand command, DbTransaction transaction = null)
        {
            Guard.ArgumentNotNull(command, "command");
            if (transaction == null)
            {
                return Database.ExecuteNonQuery(command);
            }
            return Database.ExecuteNonQuery(command, transaction);
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        public override int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
        {
            return Database.ExecuteNonQuery(storedProcedureName, parameterValues);
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns>The number of rows affected.</returns>
        public override int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return Database.ExecuteNonQuery(commandType, commandText);
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns>
        /// The number of rows affected.
        /// </returns>
        public override int ExecuteNonQuery(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return Database.ExecuteNonQuery(transaction, storedProcedureName, parameterValues);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(DbCommand command, DbTransaction transaction = null)
        {
            Guard.ArgumentNotNull(command, "command");
            if (transaction == null)
            {
                return Database.ExecuteReader(command);
            }
            return Database.ExecuteReader(command, transaction);
        }

        /// <summary>
        /// Executes the data set.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns>
        /// The <see cref="T:System.Data.DataSet" />.
        /// </returns>
        public override DataSet ExecuteDataSet(DbCommand command, DbTransaction transaction = null)
        {
            if (transaction == null)
            {
                return Database.ExecuteDataSet(command);
            }
            return Database.ExecuteDataSet(command, transaction);
        }

        /// <summary>
        /// Gets the stored proc command.
        /// </summary>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns></returns>
        public override DbCommand GetStoredProcCommand(string procedureName)
        {
            Guard.ArgumentNotNullOrEmpty(procedureName, "procedureName");
            return Database.GetStoredProcCommand(procedureName);
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public override DbConnection CreateConnection()
        {
            return Database.CreateConnection();
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object to the command.</para>
        /// </summary>
        /// <param name="command">The command to add the parameter.</param>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="T:System.Data.DbType" /> values.</para></param>        
        /// <param name="direction"><para>One of the <see cref="T:System.Data.ParameterDirection" /> values.</para></param>                
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value" />.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="T:System.Data.DataRowVersion" /> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>  
        public override void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            Guard.ArgumentNotNull(command, "command");
            Database.AddParameter(command, name, dbType, direction, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// Adds a new In <see cref="T:System.Data.Common.DbParameter" /> object to the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">The command to add the in parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="dbType">One of the <see cref="T:System.Data.DbType" /> values.</param>
        /// <remarks>
        /// This version of the method is used when you can have the same parameter object multiple times with different values.
        /// </remarks>
        public override void AddInParameter(DbCommand command, string name, DbType dbType)
        {
            Guard.ArgumentNotNull(command, "command");
            Database.AddInParameter(command, name, dbType);
        }

        /// <summary>
        /// Adds a new In <see cref="T:System.Data.Common.DbParameter" /> object to the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">The commmand to add the parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="dbType">One of the <see cref="T:System.Data.DbType" /> values.</param>
        /// <param name="value">The value of the parameter.</param>
        public override void AddInParameter(DbCommand command, string name, DbType dbType, object value)
        {
            Guard.ArgumentNotNull(command, "command");
            Database.AddInParameter(command, name, dbType, value);
        }

        /// <summary>
        /// Adds a new In <see cref="T:System.Data.Common.DbParameter" /> object to the given <paramref name="command" />.
        /// </summary>
        /// <param name="command">The command to add the parameter.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="dbType">One of the <see cref="T:System.Data.DbType" /> values.</param>
        /// <param name="sourceColumn">The name of the source column mapped to the DataSet and used for loading or returning the value.</param>
        /// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
        public override void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            Guard.ArgumentNotNull(command, "command");
            Database.AddInParameter(command, name, dbType, sourceColumn, sourceVersion);
        }

        /// <summary>
        /// Adds the out parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="size">The size.</param>
        public override void AddOutParameter(DbCommand command, string name, DbType dbType, int size)
        {
            Guard.ArgumentNotNull(command, "command");
            Database.AddOutParameter(command, name, dbType, size);
        }

        /// <summary>
        /// Gets the parameter value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public override object GetParameterValue(DbCommand command, string name)
        {
            Guard.ArgumentNotNull(command, "command");
            return Database.GetParameterValue(command, name);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
        {
            Guard.ArgumentNotNull(storedProcedureName, "storedProcedureName");
            return Database.ExecuteReader(storedProcedureName, parameterValues);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            Guard.ArgumentNotNull(commandType, "commandType");
            Guard.ArgumentNotNullOrEmpty(commandText, "commandText");
            return Database.ExecuteReader(commandType, commandText);
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        public override object ExecuteScalar(CommandType commandType, string commandText)
        {
            Guard.ArgumentNotNull(commandType, "commandType");
            Guard.ArgumentNotNullOrEmpty(commandText, "commandText");
            return Database.ExecuteScalar(commandType, commandText);
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public override object ExecuteScalar(DbCommand command)
        {
            Guard.ArgumentNotNull(command, "command");
            return Database.ExecuteScalar(command);
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="transaction">The transaction.</param>
        /// <returns></returns>
        public override object ExecuteScalar(DbCommand command, DbTransaction transaction)
        {
            Guard.ArgumentNotNull(command, "command");
            Guard.ArgumentNotNull(transaction, "transaction");
            return Database.ExecuteScalar(command, transaction);
        }

        /// <summary>
        /// Discovers the parameters.
        /// </summary>
        /// <param name="cmd">The command.</param>
        public override void DiscoverParameters(DbCommand cmd)
        {
            Guard.ArgumentNotNull(cmd, "command");
            Database.DiscoverParameters(cmd);
        }

        /// <summary>
        /// Sets the parameter value.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public override void SetParameterValue(DbCommand command, string name, object value)
        {
            Guard.ArgumentNotNull(command, "command");
            Guard.ArgumentNotNullOrEmpty(name, "name");
            Database.SetParameterValue(command, name, value);
        }
       
        /// <summary>
        /// Gets the stored proc command.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <returns></returns>
        public override DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues)
        {
            Guard.ArgumentNotNullOrEmpty(storedProcedureName, "storedProcedureName");
            return Database.GetStoredProcCommand(storedProcedureName, parameterValues);
        }
        
        /// <summary>
        /// Gets a DbDataAdapter with Standard update behavior.
        /// </summary>
        /// <returns>A <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
        /// <seealso cref="T:System.Data.Common.DbDataAdapter" />
        /// <devdoc>
        /// Created this new, public method instead of modifying the protected, abstract one so that there will be no
        /// breaking changes for any currently derived Database class.
        /// </devdoc>
        public override DbDataAdapter GetDataAdapter()
        {
            return Database.GetDataAdapter();
        }

        /// <summary>
        /// Wraps around a derived class's implementation of the GetStoredProcCommandWrapper method and adds functionality for
        /// using this method with UpdateDataSet.  The GetStoredProcCommandWrapper method (above) that takes a params array 
        /// expects the array to be filled with VALUES for the parameters. This method differs from the GetStoredProcCommandWrapper 
        /// method in that it allows a user to pass in a string array. It will also dynamically discover the parameters for the 
        /// stored procedure and set the parameter's SourceColumns to the strings that are passed in. It does this by mapping 
        /// the parameters to the strings IN ORDER. Thus, order is very important.
        /// </summary>
        /// <param name="storedProcedureName"><para>The name of the stored procedure.</para></param>
        /// <param name="sourceColumns"><para>The list of DataFields for the procedure.</para></param>
        /// <returns><para>The <see cref="T:System.Data.Common.DbCommand" /> for the stored procedure.</para></returns>
        public override DbCommand GetStoredProcCommandWithSourceColumns(string storedProcedureName, params string[] sourceColumns)
        {
            Guard.ArgumentNotNullOrEmpty(storedProcedureName, "storedProcedureName");
            Guard.ArgumentNotNull(sourceColumns, "sourceColumns");
            return Database.GetStoredProcCommandWithSourceColumns(storedProcedureName, sourceColumns);
        }

        /// <summary>
        /// Builds DBMS specific parameter name.
        /// </summary>
        /// <param name="parameterName">The parameter name without any prefix symbol.</param>
        /// <returns>
        /// The DBMS specific parameter name.
        /// </returns>
        public override string BuildParameterName(string parameterName)
        {
            return Database.BuildParameterName(parameterName);
        }

        /// <summary>
        /// Build a <see cref="T:System.Data.Common.DbCommand" /> based on the SQL text.
        /// </summary>
        /// <param name="query">The SQL text.</param>
        /// <returns>The <see cref="T:System.Data.Common.DbCommand" />.</returns>
        public override DbCommand GetSqlStringCommand(string query)
        {
            return Database.GetSqlStringCommand(query);
        }
    }
}
