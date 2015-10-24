using System.Configuration;
using Cedar.Core.SettingSource;
using Cedar.Core.SettingSource.Configuration;

namespace Cedar.Core.EntLib.SettingSource.Configuration
{
    public class ConfigurationFileSettingSourceData : SettingSourceDataBase
    {
        private const string FilePathPropertyName = "filePath";

        /// <summary>
        ///     Gets or sets the file path.
        /// </summary>
        /// <value>
        ///     The file path.
        /// </value>
        [ConfigurationProperty("filePath", IsRequired = false, DefaultValue = "")]
        public string FilePath
        {
            get { return (string) base["filePath"]; }
            set { base["filePath"] = value; }
        }

        /// <summary>
        ///     Creates the setting source.
        /// </summary>
        /// <returns>
        ///     The created setting source.
        /// </returns>
        public override ISettingSource CreateSettingSource()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return new ConfigurationFileSettingSource();
            }
            return new ConfigurationFileSettingSource(FilePath);
        }
    }
}