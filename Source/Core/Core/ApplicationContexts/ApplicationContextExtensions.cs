using System;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    ///     This class defines some extensions methods against ApplicationContext />.
    /// </summary>
    public static class ApplicationContextExtensions
    {
        /// <summary>
        ///     Clone the specified ApplicationContext and create a new DependentApplicationContext />.
        /// </summary>
        /// <param name="context">The DependentApplicationContext to be cloned.</param>
        /// <returns>The DependentApplicationContext</returns>
        /// .
        public static DependentApplicationContext DepedentClone(this ApplicationContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            var currentContext = context.ContextLocator.GetCurrentContext();
            var contextItemCollection = new ContextItemCollection();
            foreach (var current in currentContext)
            {
                var obj = current.Value;
                var cloneable = obj as ICloneable;
                if (cloneable != null)
                {
                    obj = cloneable.Clone();
                }
                var contextItem = new ContextItem(current.Key, obj, current.IsLocal);
                if (current.ReadOnly)
                {
                    contextItem.ReadOnly = true;
                }
                contextItemCollection.Add(contextItem);
            }
            return new DependentApplicationContext(contextItemCollection);
        }
    }
}