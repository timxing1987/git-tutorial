#region

using System.Globalization;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace System
{
    /// <summary>
    ///     This class defines a series of extension methods against string type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Formats the specified template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The formatted string.</returns>
        public static string Format(this string template, params object[] arguments)
        {
            Guard.ArgumentNotNullOrEmpty(template, "template");
            return string.Format(CultureInfo.CurrentCulture, template, arguments);
        }
    }
}