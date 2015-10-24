namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    /// </summary>
    public interface IContextLocator
    {
        /// <summary>
        ///     Get an existing context item by given key.
        /// </summary>
        /// <param name="key">The key of the ContextItem to get.</param>
        /// <returns>TheContextItem object to get.</returns>
        ContextItem GetContextItem(string key);

        /// <summary>
        ///     Add a new context item or use the new context item to override the exiting one.
        /// </summary>
        /// <param name="contextItem">The new ContextItem to set.</param>
        void SetContextItem(ContextItem contextItem);

        /// <summary>
        ///     Get all current context item collection.
        /// </summary>
        /// <returns>A ContextItemCollection containg all of the current context items.</returns>
        ContextItemCollection GetCurrentContext();

        /// <summary>
        ///     Attach a new context item collection to the current context.
        /// </summary>
        /// <param name="context">The ContextItemCollection to attach.</param>
        /// <param name="behavior">The ContextAttachBehavior.</param>
        void AttachContext(ContextItemCollection context, ContextAttachBehavior behavior);

        /// <summary>
        ///     Clear the current context item collection.
        /// </summary>
        void Clear();

        /// <summary>
        ///     Check if the context item of the given key exists.
        /// </summary>
        /// <param name="key">The key of the ContextItem.</param>
        /// <returns>true if the ContextItem already exists; otherwise, false.</returns>
        bool ContextItemExits(string key);
    }
}