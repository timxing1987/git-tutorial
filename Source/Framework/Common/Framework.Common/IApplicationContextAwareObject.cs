#region

using Cedar.Core.ApplicationContexts;

#endregion

namespace Cedar.Framework.Common
{
    public interface IApplicationContextAwareObject
    {
        /// <summary>
        ///     Gets the current application context.
        /// </summary>
        /// <value>The current application context.</value>
        ApplicationContext ApplicationContext { get; }
    }
}