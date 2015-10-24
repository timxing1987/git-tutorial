using System;
using System.Threading;
using Cedar.Core.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    ///     This class control the scope of dependent context.
    /// </summary>
    public class ApplicationContextScope : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Cedar.Core.ApplicationContexts.ApplicationContextScope" /> class.
        /// </summary>
        /// <param name="context">The dependent context cloned from the master application context.</param>
        public ApplicationContextScope(DependentApplicationContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            if (context.MasterThread == Thread.CurrentThread)
            {
                throw new InvalidOperationException(Resources.ExceptionInvalidThreadToCreateContextScope);
            }
            ApplicationContext.Current.ContextLocator.AttachContext(context.Items, ContextAttachBehavior.Clear);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ApplicationContext.Current.ContextLocator.Clear();
        }
    }
}