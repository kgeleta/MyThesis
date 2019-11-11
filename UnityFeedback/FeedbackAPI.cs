using System;
using UnityEngine;
using UnityFeedback.Configuration;
using UnityFeedback.Persistence;

namespace UnityFeedback
{
	public class FeedbackAPI
	{
		#region Properties

		/// <summary>
		/// Lazy loaded settings.
		/// </summary>
		public static Settings Settings => LazySettings.Value;

		#endregion

		private static readonly Lazy<Settings> LazySettings = new Lazy<Settings>(InitSettings);
		private static XmlValidator _validator = new XmlValidator(Properties.Resources.Schema);

		#region Public Methods

		/// <summary>
		/// Generates configuration file with default values.
		/// </summary>
		/// <param name="overrideFileIfExists">Old configuration file will be replaced when set to true.</param>
		/// <returns>True if configuration file was successfully created; false otherwise.</returns>
		public static bool CreateDefaultConfigurationFile(bool overrideFileIfExists)
		{
			var path = $@"{Application.dataPath}\Resources";
			var fileName = ConfigurationConstants.CONFIG_FILE_NAME;

			// Create Resources directory if not exists
			System.IO.Directory.CreateDirectory(path);

			return ConfigFileCreator.Create(path, fileName, overrideFileIfExists);
		}

		/// <summary>
		/// Creates model classes from database specified in connection string in configuration.
		/// </summary>
		/// <returns>Exit status and error message if exist.</returns>
		public static ModelCreator.ResultInformation CreateModelClasses()
		{
			var modelCreator = new ModelCreator(Settings.PowerShellPath(false));
			var result = modelCreator.Create(Settings.DatabaseProvider(false), Settings.ConnectionString(false));

			if (result.ExitStatus == ExitStatus.ExitSuccess)
			{
				ReplaceConnectionStringInModelClasses();
			}

			return result;
		}

		/// <summary>
		/// Validates configuration file.
		/// </summary>
		/// <returns>Struct with information about validation.</returns>
		public static XmlValidator.ResultInformation ValidateConfigurationFile()
		{

			return _validator.Validate(Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text);
		}

		#endregion

		private static void ReplaceConnectionStringInModelClasses()
		{
			var stringReplacer = new StringReplacer($@"{Application.dataPath}/Model", "*.cs");

			stringReplacer.StringsToReplace.Add(ConfigurationConstants.WARNING_MESSAGE, string.Empty);
			stringReplacer.StringsToReplace.Add(Settings.ConnectionString(),
				$@"{typeof(FeedbackAPI).FullName}.{nameof(Settings)}.{nameof(Settings.ConnectionString)}()");

			stringReplacer.Replace(".*Context.*");
		}

		private static Settings InitSettings()
		{
			var configReader = new ConfigReaderString(() => Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text);
			return new Settings(configReader);
		}
	}
}