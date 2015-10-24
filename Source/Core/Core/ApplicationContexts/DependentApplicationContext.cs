using System.Threading;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    ///     This class holds context items and thread information cloned from current application context.
    /// </summary>
    public class DependentApplicationContext
    {
        /// <summary>
        ///     Initializes a new instance of the DependentApplicationContext class.
        /// </summary>
        /// <param name="items">The collection of context items.</param>
        public DependentApplicationContext(ContextItemCollection items)
        {
            Guard.ArgumentNotNull(items, "items");
            Items = items;
            MasterThread = Thread.CurrentThread;
        }

        /// <summary>
        ///     Gets the collection of context items.
        /// </summary>
        /// <value>
        ///     The collection of context items.
        /// </value>
        public ContextItemCollection Items { get; private set; }

        /// <summary>
        ///     Gets the master thread to which the current application is belong.
        /// </summary>
        /// <value>
        ///     The master thread to which the current application is belong.
        /// </value>
        public Thread MasterThread { get; private set; }
    }
}