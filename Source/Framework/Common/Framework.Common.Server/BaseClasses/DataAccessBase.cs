#region

using System;
using Cedar.Core.IoC;

#endregion

namespace Cedar.Framework.Common.Server.BaseClasses
{
    /// <summary>
    /// </summary>
    public class DataAccessBase : MarshalByRefObject, IServiceLocatableObject
    {
        private MySqlDbHelper dbHelper;

        /// <summary>
        /// </summary>
        protected virtual MySqlDbHelper Helper
        {
            get
            {
                if (dbHelper == null)
                {
                    dbHelper = new MySqlDbHelper("mysqldb");
                }
                return dbHelper;
            }
        }

        /// <summary>
        ///     Gets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        public IServiceLocator ServiceLocator
        {
            get { return ServiceLocatorFactory.GetServiceLocator(null); }
        }
    }
}