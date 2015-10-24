#region

using Cedar.Core.Configuration;

#endregion

namespace Cedar.Core.SettingSource.Configuration
{
    /// <summary>
    ///     This is base class of all concrete setting source configuration element classes.
    /// </summary>
    public abstract class SettingSourceDataBase : NameTypeConfigurationElement
    {
        /// <summary>
        ///     Creates the setting source.
        /// </summary>
        /// <returns>The created setting source.</returns>
        public abstract ISettingSource CreateSettingSource();
    }
}