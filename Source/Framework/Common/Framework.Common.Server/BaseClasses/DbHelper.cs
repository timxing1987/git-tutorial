#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cedar.Framework.Common.BaseClasses;
using Dapper;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Framework.Common.Server.BaseClasses
{
    /// <summary>
    /// </summary>
    public class MySqlDbHelper : MarshalByRefObject
    {
        //private Dictionary<CacheKey, DbParameterCollection> cachedParameters = new Dictionary<CacheKey, DbParameterCollection>();

        /// <summary>
        /// </summary>
        /// <param name="connectionStringName"></param>
        public MySqlDbHelper(string connectionStringName)
        {
            Guard.ArgumentNotNullOrEmpty(connectionStringName, "connectionStringName");
            ConnectionStringName = connectionStringName;
            var cnnStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            ConnectionString = cnnStringSettings.ConnectionString;
            Factory = DbProviderFactories.GetFactory(cnnStringSettings.ProviderName);
        }

        /// <summary>
        /// </summary>
        protected string ConnectionStringName { get; private set; }

        /// <summary>
        /// </summary>
        protected string ConnectionString { get; }

        /// <summary>
        /// </summary>
        public DbProviderFactory Factory { get; }

        //protected abstract void DeriveParameters(DbCommand discoveryCommand);

        //protected virtual int UserParametersStartIndex()
        //{
        //    return 1;
        //}

        //public virtual string BuildParameterName(string name)
        //{
        //    if (name.StartsWith("@"))
        //    {
        //        return name;
        //    }
        //    return "@" + name;
        //}

        //protected virtual void AssignParameters(DbCommand command, object[] parameters)
        //{
        //    CacheKey key = new CacheKey { Database = this.ConnectionStringName, Procedure = command.CommandText };
        //    if (!cachedParameters.ContainsKey(key))
        //    {
        //        this.DeriveParameters(command);
        //        lock (cachedParameters)
        //        {
        //            cachedParameters[key] = command.Parameters;
        //        }
        //    }
        //    int parameterIndexShift = this.UserParametersStartIndex();
        //    for (int i = 0; i < parameters.Length; i++)
        //    {
        //        IDataParameter parameter = command.Parameters[i + parameterIndexShift];
        //        command.Parameters[this.BuildParameterName(parameter.ParameterName)].Value = parameters[i] ?? DBNull.Value;
        //    }
        //}

        /// <summary>
        ///     获取链接
        /// </summary>
        /// <returns></returns>
        public virtual DbConnection GetConnection()
        {
            var connection = Factory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual T ExecuteScalar<T>(string sql, object parameters, CommandType commandType = CommandType.Text)
        {
            using (var connection = Factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                return connection.ExecuteScalar<T>(sql, parameters, null, null, commandType);
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters,
            CommandType commandType = CommandType.Text)
        {
            using (var connection = Factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                return connection.QueryAsync<T>(sql, parameters, null, null, commandType);
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Query<T>(string sql, object parameters = null,
            CommandType commandType = CommandType.Text)
        {
            using (var connection = Factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                return connection.Query<T>(sql, parameters, null, true, null, commandType);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual IEnumerable<dynamic> Query(string sql, object parameters = null,
            CommandType commandType = CommandType.Text)
        {
            using (var connection = Factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                return connection.Query(sql, parameters, null, true, null, commandType);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual Task<int> ExecuteAsync(string sql, object parameters = null,
            CommandType commandType = CommandType.Text)
        {
            using (var connection = Factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                return connection.ExecuteAsync(sql, parameters, null, null, commandType);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual int Execute(string sql, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var connection = Factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                return connection.Execute(sql, parameters, null, null, commandType);
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual T Execute<T>(string sql, object parameters = null, CommandType commandType = CommandType.Text)
        {
            using (var connection = Factory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();
                return connection.ExecuteScalar<T>(sql, parameters, null, null, commandType);
            }
        }

        /// <summary>
        ///     执行分页存储过程
        /// </summary>
        /// <typeparam name="T">返回实体类型</typeparam>
        /// <param name="model">分页实体</param>
        /// <param name="echo">服务器请求次数</param>
        /// <param name="connStr"></param>
        /// <returns>实体集合</returns>
        public BasePageList<T> ExecutePaging<T>(PagingModel model, int? echo, string connStr = "")
        {
            var baseList = new BasePageList<T>();
            try
            {
                using (var connection = Factory.CreateConnection())
                {
                    connection.ConnectionString = ConnectionString;
                    connection.Open();
                    //参数
                    var obj = new
                    {
                        model.TableName,
                        model.Fields,
                        model.OrderField,
                        GroupField = model.GroupBy,
                        sqlWhere = model.SqlWhere,
                        pageSize = model.PageSize,
                        pageIndex = model.PageIndex,
                        sqlCountTable = "",
                        Paging = model.Paging != null && model.Paging.Value
                    };

                    var args = new DynamicParameters(obj);
                    args.Add("totalpage", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    //执行存储过程
                    IEnumerable<T> data;
                    using (
                        var result = connection.QueryMultiple(model.SpName, args,
                            commandType: CommandType.StoredProcedure))
                    {
                        //获取结果集
                        data = result.Read<T>();
                    }

                    //获取输出参数
                    baseList.sEcho = echo;
                    baseList.iTotalDisplayRecords = args.Get<int>("totalpage");
                    baseList.iTotalRecords = args.Get<int>("totalpage");
                    baseList.aaData = data;
                }
            }
            catch (Exception ex)
            {
                // ignored
            }

            return baseList;
        }

        /// <summary>
        ///     根据实体属性值转换成sql语句
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="symbol">分隔符</param>
        /// <returns>sql语句</returns>
        public string CreateField(dynamic entity, string symbol = ",")
        {
            var sql = new StringBuilder();
            if (entity == null)
                return sql.ToString();
            var type = entity.GetType();
            if (type == null)
                return sql.ToString();
            //属性
            PropertyInfo[] propers = type.GetProperties();
            foreach (var proper in propers)
            {
                //属性值
                var value = proper.GetValue(entity);
                if (value != null)
                {
                    sql.Append(proper.Name.ToLower()).Append(" = @").Append(proper.Name).Append(" " + symbol + " ");
                }
            }
            return sql.ToString();
        }
    }

    //internal class CacheKey
    //{
    //    public string Database { get; set; }
    //    public string Procedure { get; set; }
    //    public override int GetHashCode()
    //    {
    //        return this.Database.GetHashCode() ^ this.Procedure.GetHashCode();
    //    }
    //}
}