using UnityEngine;

namespace UnityFeedback.Configuration
{
	/// <summary>
	/// Configuration reader with cache.
	/// </summary>
    public class AppSettings
    {
        private readonly IConfigReader _configReader;

        private AppSettings() => _configReader = new ConfigReaderString(Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text);

        private static AppSettings _instance;
        public static AppSettings Instance => _instance ?? (_instance = new AppSettings());

        private string[] _scenes;
        private string _connectionString;

        public string[] Scenes => _scenes ?? (_scenes = _configReader.ReadArrayNode(ConfigurationConstants.NodeName.SCENES, ConfigurationConstants.Attribute.SCENES));
        public string ConnectionString => _connectionString ?? (_connectionString =
                                              _configReader.ReadSingleNode(
                                                  ConfigurationConstants.NodeName.CONNECTION_STRING,
                                                  ConfigurationConstants.Attribute.CONNECTION_STRING));

        public static bool ValidateConfiguration()
        {
	        var xml = Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text;
	        var schema = Properties.Resources.Schema;

	        return XmlValidator.Validate(xml, schema);
        }
    }
}