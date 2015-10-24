using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Cedar.Core.ApplicationContexts.Configuration;
using Cedar.Core.Configuration;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    ///     The <see cref="T:Cedar.Core.ApplicationContexts.ContextLocator" /> which use the
    ///     <see cref="T:System.Runtime.Remoting.Messaging.CallContext" /> as the context storage.
    /// </summary>
    [ConfigurationElement(typeof (CallContextLocatorData))]
    public class CallContextLocator : ContextLocator
    {
        [ThreadStatic] private static List<string> keys;

        /// <summary>
        ///     Gets the keys.
        /// </summary>
        /// <value>
        ///     The keys.
        /// </value>
        public static IList<string> Keys
        {
            get
            {
                List<string> arg_14_0;
                if ((arg_14_0 = keys) == null)
                {
                    arg_14_0 = (keys = new List<string>());
                }
                return arg_14_0;
            }
        }

        /// <summary>
        ///     Get an existing context item by given key.
        /// </summary>
        /// <param name="key">The key of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> to get.</param>
        /// <returns>
        ///     The <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> object to get.
        /// </returns>
        public override ContextItem GetContextItem(string key)
        {
            Guard.ArgumentNotNullOrEmpty(key, "key");
            return CallContext.GetData(key) as ContextItem;
        }

        /// <summary>
        ///     Add a new context item or use the new context item to override the exiting one.
        /// </summary>
        /// <param name="contextItem">The new <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> to set.</param>
        protected override void SetContextItemCore(ContextItem contextItem)
        {
            Guard.ArgumentNotNull(contextItem, "contextItem");
            CallContext.FreeNamedDataSlot(contextItem.Key);
            CallContext.SetData(contextItem.Key, contextItem);
            if (!Keys.Contains(contextItem.Key))
            {
                Keys.Add(contextItem.Key);
            }
        }

        /// <summary>
        ///     Get all current context item collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:Cedar.Core.ApplicationContexts.ContextItemCollection" /> containg all of the current context items.
        /// </returns>
        public override ContextItemCollection GetCurrentContext()
        {
            var contextItemCollection = new ContextItemCollection();
            foreach (var current in Keys)
            {
                var contextItem = GetContextItem(current);
                if (contextItem != null)
                {
                    contextItemCollection.Add(contextItem);
                }
            }
            return contextItemCollection;
        }

        /// <summary>
        ///     Clear the current context item collection.
        /// </summary>
        public override void Clear()
        {
            foreach (var current in Keys)
            {
                CallContext.FreeNamedDataSlot(current);
            }
            Keys.Clear();
        }

        /// <summary>
        ///     Check if the context item of the given key exists.
        /// </summary>
        /// <param name="key">The key of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" />.</param>
        /// <returns>
        ///     true if the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> already exists; otherwise, false.
        /// </returns>
        public override bool ContextItemExits(string key)
        {
            Guard.ArgumentNotNullOrEmpty(key, "key");
            return Keys.Contains(key);
        }
    }
}