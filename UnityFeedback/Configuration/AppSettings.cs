using UnityEngine;

namespace UnityFeedback.Configuration
{
	/// <summary>
	/// This class provides access to configuration file.
	/// </summary>
    public class AppSettings
    {
        private readonly IConfigReader _configReader;
        private AppSettings() => _configReader = new ConfigReaderString(Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text);
        private static AppSettings _instance;

        private string[] _scenes;
        private string _connectionString;

		/// <summary>
		/// Returns instance of <see cref="AppSettings"/> class.
		/// </summary>
		public static AppSettings Instance => _instance ?? (_instance = new AppSettings());

		/// <summary>
		/// Reads scene paths from the configuration file. By default this method uses cached values.
		/// </summary>
		/// <param name="useCache">True to return cached value if exists; False to force reading from file.</param>
		/// <returns>Array of scene paths</returns>
        public string[] Scenes(bool useCache = true)
        {
	        if (_scenes == null || !useCache)
	        {
				_scenes = _configReader.ReadArrayNode(ConfigurationConstants.NodeName.SCENES, ConfigurationConstants.Attribute.SCENES);
			}

	        return _scenes;
        }

		/// <summary>
		/// Reads connection string from the configuration file. By default this method uses cached values.
		/// </summary>
		/// <param name="useCache">True to return cached value if exists; False to force reading from file.</param>
		/// <returns>Connection string</returns>
		public string ConnectionString(bool useCache = true)
		{
			if (_connectionString == null || !useCache)
			{
				_connectionString = _configReader.ReadSingleNode(ConfigurationConstants.NodeName.CONNECTION_STRING, ConfigurationConstants.Attribute.CONNECTION_STRING);
			}

			return _connectionString;
		}

		/// <summary>
		/// Validates configuration file.
		/// </summary>
		/// <returns>True if configuration file is valid; False otherwise.</returns>
		public static bool ValidateConfiguration()
        {
	        var xml = Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text;
	        var schema = Properties.Resources.Schema;

	        return XmlValidator.Validate(xml, schema);
        }
    }
}