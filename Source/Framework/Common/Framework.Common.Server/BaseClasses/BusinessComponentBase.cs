#region

using System;
using Cedar.Core.IoC;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Framework.Common.Server.BaseClasses
{
    /// <summary>
    /// </summary>
    public abstract class BusinessComponentBase : MarshalByRefObject, IServiceLocatableObject
    {
        /// <summary>
        ///     Gets the service locator.
        /// </summary>
        /// <value>The service locator.</value>
        public IServiceLocator ServiceLocator
        {
            get { return ServiceLocatorFactory.GetServiceLocator(null); }
        }
    }

    /// <summary>
    ///     Base class of business component.
    /// </summary>
    /// <typeparam name="TDataAccess">The type of the data access.</typeparam>
    public class BusinessComponentBase<TDataAccess> : BusinessComponentBase where TDataAccess : DataAccessBase
    {
        /// <summary>
        /// </summary>
        /// <param name="daObject"></param>
        public BusinessComponentBase(TDataAccess daObject)
        {
            Guard.ArgumentNotNull(daObject, "daObject");
            DataAccess = daObject;
        }

        /// <summary>
        ///     Gets or sets the DA object.
        /// </summary>
        /// <value>The DA object.</value>
        public TDataAccess DataAccess { get; private set; }
    }
}