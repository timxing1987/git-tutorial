#region

using System;
using Cedar.Core.IoC;

#endregion

namespace Cedar.Framework.Common.Server.BaseClasses
{
    public abstract class ServiceBase : MarshalByRefObject, IServiceLocatableObject
    {
        public IServiceLocator ServiceLocator
        {
            get { return ServiceLocatorFactory.GetServiceLocator(null); }
        }
    }

    /// <summary>
    ///     The base class for all serivces with a typed BC object.
    /// </summary>
    /// <typeparam name="TBusinessComponent">The type of the BC object.</typeparam>
    public class ServiceBase<TBusinessComponent> : ServiceBase where TBusinessComponent : BusinessComponentBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Architecture.Server.ServiceBase`1" /> class.
        /// </summary>
        /// <param name="bcObject">The bc object.</param>
        public ServiceBase(TBusinessComponent bcObject)
        {
            BusinessComponent = bcObject;
        }

        /// <summary>
        ///     Gets or sets the BC object.
        /// </summary>
        /// <value>The BC object.</value>
        public TBusinessComponent BusinessComponent { get; set; }
    }
}