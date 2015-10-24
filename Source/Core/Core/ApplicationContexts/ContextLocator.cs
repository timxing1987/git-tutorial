using System;
using Cedar.Core.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    ///     Define the context locator.
    /// </summary>
    public abstract class ContextLocator : IContextLocator
    {
        /// <summary>
        ///     Get an existing context item by given key.
        /// </summary>
        /// <param name="key">The key of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> to get.</param>
        /// <returns>
        ///     The <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> object to get.
        /// </returns>
        public abstract ContextItem GetContextItem(string key);

        /// <summary>
        ///     Add a new context item or use the new context item to override the exiting one.
        /// </summary>
        /// <param name="contextItem">The new <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> to set.</param>
        public void SetContextItem(ContextItem contextItem)
        {
            EnsureCanWrite(contextItem);
            SetContextItemCore(contextItem);
        }

        /// <summary>
        ///     Get all current context item collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Cedar.Core.ApplicationContexts.ContextItemCollection" /> containg all of the current context items.
        /// </returns>
        public abstract ContextItemCollection GetCurrentContext();

        /// <summary>
        ///     Clear the current context item collection.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        ///     Attach a new context item collection to the current context.
        /// </summary>
        /// <param name="context">The <see cref="T:Cedar.Core.ApplicationContexts.ContextItemCollection" /> to attach.</param>
        /// <param name="behavior">The <see cref="T:Cedar.Core.ApplicationContexts.ContextAttachBehavior" />.</param>
        public void AttachContext(ContextItemCollection context, ContextAttachBehavior behavior)
        {
            if (behavior == ContextAttachBehavior.Clear)
            {
                Clear();
            }
            foreach (var current in context)
            {
                if (GetContextItem(current.Key) == null)
                {
                    SetContextItem(current);
                }
                else if (behavior == ContextAttachBehavior.Override)
                {
                    SetContextItem(current);
                }
            }
        }

        /// <summary>
        ///     Check if the context item of the given key exists.
        /// </summary>
        /// <param name="key">The key of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" />.</param>
        /// <returns>
        ///     true if the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> already exists; otherwise, false.
        /// </returns>
        public abstract bool ContextItemExits(string key);

        private void EnsureCanWrite(ContextItem contextItem)
        {
            Guard.ArgumentNotNull(contextItem, "contextItem");
            var contextItem2 = GetContextItem(contextItem.Key);
            if (contextItem2 != null && contextItem2.ReadOnly)
            {
                throw new InvalidOperationException(
                    ResourceUtility.Format(Resources.ExceptionCannotChangeReadonlyContextItem));
            }
        }

        /// <summary>
        ///     Set context item inernally.
        /// </summary>
        /// <param name="contextItem">The context item.</param>
        protected abstract void SetContextItemCore(ContextItem contextItem);
    }
}