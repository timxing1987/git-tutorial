namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    ///     接口返回格式
    /// </summary>
    public class JResult
    {
        /// <summary>
        ///     操作状态码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        ///     结果类型
        /// </summary>
        public object errmsg { get; set; }

        #region 封装结果

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static JResult _jResult(int result)
        {
            return new JResult
            {
                errcode = result > 0 ? 0 : 400,
                errmsg = result > 0 ? "操作成功" : "操作失败"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errcode"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static JResult _jResult(int errcode, object errmsg)
        {
            return new JResult
            {
                errcode = errcode,
                errmsg = errmsg
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static JResult _jResult(object model)
        {
            return new JResult
            {
                errcode = model == null ? 400 : 0,
                errmsg = model ?? ""
            };
        }

        #endregion
    }
}