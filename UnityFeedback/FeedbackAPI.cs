using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityFeedback.Configuration;
using UnityFeedback.Persistence;

namespace UnityFeedback
{
	public class FeedbackAPI
	{
		private static readonly Lazy<Settings> LazySettings = new Lazy<Settings>(InitSettings);
		private static readonly XmlValidator Validator = new XmlValidator(Properties.Resources.Schema);

		#region Events

		/// <summary>
		/// Subscribe to receive standard output and standard error from <see cref="ModelCreator"/>.
		/// </summary>
		public static event EventHandler<OutputEventArgs> ModelCreatorOutput;

		#endregion

		#region Properties

		/// <summary>
		/// Lazy loaded settings.
		/// </summary>
		public static Settings Settings => LazySettings.Value;

		#endregion

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
		/// Creates task creating model classes from database specified in connection string in configuration.
		/// </summary>
		/// <returns>Task ready to start.</returns>
		public static Task<ModelCreator.ResultInformation> GetModelCreatorTask()
		{
			// This is because UnityEngine methods can be only called from main thread
			var powerShellPath = Settings.PowerShellPath(false);
			var databaseProvider = Settings.DatabaseProvider(false);
			var connectionString = Settings.ConnectionString(false);
			var pathToModelDirectory = $@"{Application.dataPath}/Model";

			return new Task<ModelCreator.ResultInformation>(() =>
				{
					var modelCreator = new ModelCreator(powerShellPath);
					modelCreator.OutputReceived += OnModelCreatorOutput;
					var result = modelCreator.Create(databaseProvider, connectionString);

					if (result.ExitStatus == ExitStatus.ExitSuccess)
					{
						ReplaceConnectionStringInModelClasses(connectionString, pathToModelDirectory);
					}

					return result;
				});
		}

		/// <summary>
		/// Validates configuration file.
		/// </summary>
		/// <returns>Struct with information about validation.</returns>
		public static XmlValidator.ResultInformation ValidateConfigurationFile()
		{
			return Validator.Validate(Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text);
		}

		#endregion

		protected static void OnModelCreatorOutput(object sender, OutputEventArgs e)
		{
			var handler = ModelCreatorOutput;
			handler?.Invoke(sender, e);
		}

		private static void ReplaceConnectionStringInModelClasses(string connectionString, string pathToModelDirectory)
		{
			var stringReplacer = new StringReplacer(pathToModelDirectory, "*.cs");

			stringReplacer.StringsToReplace.Add(ConfigurationConstants.WARNING_MESSAGE, string.Empty);
			stringReplacer.StringsToReplace.Add(ToLiteral(connectionString),
				$@"{typeof(FeedbackAPI).FullName}.{nameof(Settings)}.{nameof(Settings.ConnectionString)}()");

			stringReplacer.Replace(".*Context.*");
		}

		private static Settings InitSettings()
		{
			var configReader = new ConfigReaderString(() => Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text);
			return new Settings(configReader);
		}

		private static string ToLiteral(string input)
		{
			using (var writer = new StringWriter())
			{
				using (var provider = CodeDomProvider.CreateProvider("CSharp"))
				{
					provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
					return writer.ToString();
				}
			}
		}
	}
}