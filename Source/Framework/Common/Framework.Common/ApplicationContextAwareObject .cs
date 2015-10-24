#region

using System;
using System.Collections.Generic;
using Cedar.Core.ApplicationContexts;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Framework.Common
{
    public class ApplicationContextAwareObject : MarshalByRefObject, IApplicationContextAwareObject, IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Core.ApplicationContextAwareObject" /> class.
        /// </summary>
        public ApplicationContextAwareObject()
        {
            DisposableObjects = new List<IDisposable>();
        }

        /// <summary>
        ///     Gets the disposable objects.
        /// </summary>
        protected IList<IDisposable> DisposableObjects { get; }

        /// <summary>
        ///     Gets the current application context.
        /// </summary>
        /// <value>The current application context.</value>
        public ApplicationContext ApplicationContext
        {
            get { return ApplicationContext.Current; }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        ///     Adds the disposable object.
        /// </summary>
        /// <param name="disposableObject">The disposable object.</param>
        protected virtual void AddDisposableObject(object disposableObject)
        {
            Guard.ArgumentNotNull(disposableObject, "disposableObject");
            var item = disposableObject as IDisposable;
            if (disposableObject != null && !DisposableObjects.Contains(item))
            {
                DisposableObjects.Add(item);
            }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    foreach (var current in DisposableObjects)
                    {
                        current.Dispose();
                    }
                }
                _isDisposed = true;
            }
        }
    }
}