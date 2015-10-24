#region

using System.Collections.Generic;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    /// </summary>
    internal static class Settings
    {
        static Settings()
        {
            SectionNames = new[]
            {
                "cedar.applicationContexts"
            };
        }

        /// <summary>
        /// </summary>
        public static IEnumerable<string> SectionNames { get; private set; }
    }
}