using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Security.AccessControl;
using System.Xml;

namespace Cedar.Framework.Common.BaseClasses
{
    /// <summary>
    ///     Xml辅助类，静态方法(XML文件操作和配置文件的操作类)
    /// </summary>
    public class XmlHelper
    {
        #region

        /// <summary>{ For 根据参数得到XML节点列表 }</summary>
        /// <param name="listName">ListName(String)</param>
        /// <returns>IList{ParameterModel}|没有择返回null</returns>
        public static XmlNodeList GetXmlNodes(string listName)
        {
            listName = listName ?? string.Empty;
            const string nodeformat = "Root/Items/List[@name='{0}']/item";
            var nodepath = string.Format(nodeformat, listName);

            var nodes = _pDoc.SelectNodes(nodepath);

            return nodes;
        }

        #endregion

        #region { 00.初始化相关的xml文件数据-非单例模式 }

        //xDoc 是对应会员特定配置文件(CustomerSetting.config)
        private static XmlDocument _xDoc;

        private static readonly string XPath = ConfigurationManager.AppSettings["CustomerSettingPath"] ??
                                               "Configs/CustomerSetting.config";

        //pDoc 是对应系统的全局参数配置文件(ParameterSetting.config)
        private static XmlDocument _pDoc;

        private static readonly string PPath = ConfigurationManager.AppSettings["ParameterSettingPath"] ??
                                               "Configs/ParameterSetting.config";

        private static readonly object LockHelper = new object();

        /// <summary>
        ///     静态构造函数
        ///     加载类时，初始化XML文档
        /// </summary>
        static XmlHelper()
        {
            Init();
        }

        /// <summary>初始化XML文档的载入</summary>
        /// { Created At Time:[ 2013-10-22 16:28], By User:Administrator, On Machine:APP-SEAN }
        private static void Init()
        {
            lock (LockHelper)
            {
                if (_xDoc == null)
                {
                    _xDoc = new XmlDocument();
                    var xmlFilePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, XPath);
                    //设置XML文档所有人完全控制权限
                    var dSecurity = new DirectorySecurity();
                    //dSecurity.SetAccessRule(new FileSystemAccessRule("everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                    Directory.SetAccessControl(xmlFilePath, dSecurity);

                    using (var reader = CreateReader(xmlFilePath))
                    {
                        _xDoc.Load(reader);
                    }
                }

                if (_pDoc == null)
                {
                    _pDoc = new XmlDocument();
                    var xmlFilePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, PPath);
                    //设置XML文档所有人完全控制权限
                    var dSecurity = new DirectorySecurity();
                    Directory.SetAccessControl(xmlFilePath, dSecurity);

                    using (var reader = CreateReader(xmlFilePath))
                    {
                        _pDoc.Load(reader);
                    }
                }
            }
        }

        /// <summary>
        ///     强制重新加载XML文档(文档内容变化时刷新用)
        /// </summary>
        /// { Created At Time:[ 2013-10-22 16:29], By User:Administrator, On Machine:APP-SEAN }
        public static void Reload()
        {
            _xDoc = null;
            _pDoc = null;
            Init();
        }

        #endregion

        #region { 01.通用的XML操作方法 }

        /// <summary>
        ///     生成XmlReader
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="ignorecomments">忽略注释</param>
        /// <returns>XmlReader</returns>
        public static XmlReader CreateReader(string filename, bool ignorecomments = true)
        {
            var settings = new XmlReaderSettings();
            settings.IgnoreComments = ignorecomments;
            return XmlReader.Create(filename, settings);
        }

        /// <summary>
        ///     将xml字符串转换成数据集
        /// </summary>
        /// <param name="filename">xml文件路径</param>
        /// <returns>数据集</returns>
        public static DataSet ReadXml2Set(string filename)
        {
            var ds = new DataSet();
            if (File.Exists(filename))
            {
                ds.ReadXml(filename);
            }
            return ds;
        }

        #endregion
    }
}