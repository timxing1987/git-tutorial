using System.Collections.Generic;

namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    /// </summary>
    public abstract class QueryModel
    {
        /// <summary>
        /// </summary>
        protected QueryModel()
        {
            PageIndex = 1;
            PageSize = 10;
        }

        /// <summary>
        ///     页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     页容量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     是否分页
        /// </summary>
        public bool? Paging { get; set; }

        /// <summary>
        ///     前多少条记录
        /// </summary>
        public int? Top { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        ///     分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        ///     DataTables请求服务器端次数
        /// </summary>
        public int? Echo { get; set; }

        /// <summary>
        ///     1:获取列表,2:获取count
        /// </summary>
        public int? PagingQueryType { get; set; }
    }

    /// <summary>
    ///     分页公共返回类型列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePageList<T>
    {
        /// <summary>
        ///     DataTables请求服务器端次数
        /// </summary>
        public int? sEcho { get; set; }

        /// <summary>
        ///     总记录数
        /// </summary>
        public int? iTotalRecords { get; set; }

        /// <summary>
        ///     显示的记录数
        /// </summary>
        public int? iTotalDisplayRecords { get; set; }

        /// <summary>
        ///     数据实体列表
        /// </summary>
        public IEnumerable<T> aaData { get; set; }
    }
}