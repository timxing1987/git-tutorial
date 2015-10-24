namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    ///     The enumeration of ContextAttachBehavior gives following three options to attach a new context collection to the
    ///     current ones
    /// </summary>
    public enum ContextAttachBehavior
    {
        /// <summary>
        ///     Clear all of the current context items before attaching new ones.
        /// </summary>
        Clear,

        /// <summary>
        ///     If the concurrent context item of the same key as the one to attach, nothing will be done.
        /// </summary>
        Ignore,

        /// <summary>
        ///     Override the current context if it has the same key as the one to attach.
        /// </summary>
        Override
    }
}