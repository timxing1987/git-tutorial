#region

using System.Configuration;

#endregion

namespace Cedar.Core.SettingSource
{
    /// <summary>
    ///     All of setting resource classes must implement this interface.
    /// </summary>
    public interface ISettingSource
    {
        /// <summary>
        ///     Gets the configuration section based on specified section name.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>The target configuration section.</returns>
        ConfigurationSection GetConfigurationSection(string sectionName);

        /// <summary>
        ///     Sets as current settings source.
        /// </summary>
        void SetAsCurrentSettingsSource();
    }
}