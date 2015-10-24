namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    /// </summary>
    public class PagingModel
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="spname">存储过程名称</param>
        /// <param name="tablename">表名</param>
        /// <param name="fields">字段</param>
        /// <param name="orderfield">排序字段</param>
        /// <param name="sqlwhere">查询条件</param>
        /// <param name="pagesize">页容量</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="groupby">分组</param>
        /// <param name="paging">是否分页</param>
        public PagingModel(
            string spname = null, string tablename = null, string fields = null, string orderfield = null,
            string sqlwhere = null,
            int? pagesize = null, int? pageindex = null, string groupby = null, bool? paging = null)
        {
            SpName = spname;
            TableName = tablename;
            Fields = fields;
            OrderField = orderfield;
            SqlWhere = sqlwhere;
            PageSize = pagesize;
            PageIndex = pageindex;
            GroupBy = groupby;
            Paging = paging ?? (pagesize > 0);
        }

        /// <summary>
        /// </summary>
        /// <param name="spname"></param>
        /// <param name="maintable"></param>
        /// <param name="pk"></param>
        /// <param name="alias"></param>
        /// <param name="subtable"></param>
        /// <param name="fields"></param>
        /// <param name="orderfield"></param>
        /// <param name="sqlwhere"></param>
        /// <param name="sqlwherenopaging"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="groupby"></param>
        /// <param name="paging"></param>
        /// <param name="pagingquerytype"></param>
        public PagingModel(
            string spname = null, string maintable = null, string pk = null, string alias = null, string subtable = null,
            string fields = null, string orderfield = null, string sqlwhere = null, string sqlwherenopaging = null,
            int? pagesize = null,
            int? pageindex = null, string groupby = null, bool? paging = null, int? pagingquerytype = 1)
        {
            SpName = spname;
            MainTable = maintable;
            Pk = pk;
            Alias = alias;
            SubTable = subtable;
            Fields = fields;
            OrderField = orderfield;
            SqlWhere = sqlwhere;
            SqlWhereNoPaging = sqlwherenopaging;
            PageSize = pagesize;
            PageIndex = pageindex;
            GroupBy = groupby;
            Paging = paging ?? (pagesize > 0);
            PagingQueryType = pagingquerytype ?? 1;
        }

        /// <summary>
        ///     存储过程名称
        /// </summary>
        public string SpName { get; set; }

        /// <summary>
        ///     表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        ///     主表
        /// </summary>
        public string MainTable { get; set; }

        /// <summary>
        ///     主键
        /// </summary>
        public string Pk { get; set; }

        /// <summary>
        ///     主表别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     辅表
        /// </summary>
        public string SubTable { get; set; }

        /// <summary>
        ///     查询字段
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        ///     排序字段
        /// </summary>
        public string OrderField { get; set; }

        /// <summary>
        ///     查询条件
        /// </summary>
        public string SqlWhere { get; set; }

        /// <summary>
        ///     不分页的查询条件
        /// </summary>
        public string SqlWhereNoPaging { get; set; }

        /// <summary>
        ///     页容量
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        ///     页索引
        /// </summary>
        public int? PageIndex { get; set; }

        /// <summary>
        ///     分组
        /// </summary>
        public string GroupBy { get; set; }

        /// <summary>
        ///     是否分页
        /// </summary>
        public bool? Paging { get; set; }

        /// <summary>
        ///     查询类型 1.查询总页数 2.查询数据
        /// </summary>
        public int? PagingQueryType { get; set; }
    }
}