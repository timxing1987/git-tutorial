using System.Globalization;

namespace Cedar.Core
{
    /// <summary>
    ///     This unity class is used to Resource string formatting.
    /// </summary>
    public static class ResourceUtility
    {
        /// <summary>
        ///     Formats the specified resource string based on the current culture.
        /// </summary>
        /// <param name="resourceString">The resource string.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string Format(string resourceString, params object[] parameters)
        {
            return string.Format(CultureInfo.CurrentCulture, resourceString, parameters);
        }
    }
}