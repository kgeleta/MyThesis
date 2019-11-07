using System;
using UnityEngine;

namespace UnityFeedback.Configuration
{
	/// <summary>
	/// This class provides thread safe access to configuration file.
	/// </summary>
	public class AppSettings
	{
		private readonly IConfigReader _configReader;
		private readonly object _lockObject = new object();

		#region Cache

		private string[] _scenes;
		private string _connectionString;
		private Persistence.DatabaseProvider? _databaseProvider;
		private string _powerShellPath;

		#endregion

		public AppSettings(IConfigReader configReader)
		{
			this._configReader = configReader;
		}

		/// <summary>
		/// Reads scene paths from the configuration file. By default this method uses cached values.
		/// </summary>
		/// <param name="useCache">True to return cached value if exists; False to force reading from file.</param>
		/// <returns>Array of scene paths</returns>
		public string[] Scenes(bool useCache = true)
		{
			if (_scenes == null || !useCache)
			{
				lock (_lockObject)
				{
					if (_scenes == null || !useCache)
					{
						if (!useCache)
						{
							this._configReader.RefreshConfiguration();
						}
						_scenes = _configReader.ReadArrayNode(ConfigurationConstants.NodeName.SCENES, ConfigurationConstants.Attribute.PATH);
					}
				}
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
				lock (_lockObject)
				{
					if (_connectionString == null || !useCache)
					{
						if (!useCache)
						{
							this._configReader.RefreshConfiguration();
						}
						_connectionString = _configReader.ReadSingleNode(ConfigurationConstants.NodeName.CONNECTION_STRING, ConfigurationConstants.Attribute.VALUE);
					}
				}
			}

			return _connectionString;
		}

		/// <summary>
		/// Reads database provider from the configuration file. By default this method uses cached values.
		/// </summary>
		/// <param name="useCache">True to return cached value if exists; False to force reading from file.</param>
		/// <returns>Database provider.</returns>
		/// <exception cref="ArgumentException">Throws when value can't be parsed to <see cref="Persistence.DatabaseProvider"/></exception>
		public Persistence.DatabaseProvider DatabaseProvider(bool useCache = true)
		{
			if (_databaseProvider == null || !useCache)
			{
				lock (_lockObject)
				{
					if (_databaseProvider == null || !useCache)
					{
						if (!useCache)
						{
							this._configReader.RefreshConfiguration();
						}
						var dbString = _configReader.ReadSingleNode(ConfigurationConstants.NodeName.DATABASE_PROVIDER,
							ConfigurationConstants.Attribute.VALUE);
						_databaseProvider = (Persistence.DatabaseProvider) Enum.Parse(typeof(Persistence.DatabaseProvider), dbString);
					}
				}
			}

			return _databaseProvider.Value;
		}

		/// <summary>
		/// Reads PowerShell path from the configuration file. By default this method uses cached values.
		/// </summary>
		/// <param name="useCache">True to return cached value if exists; False to force reading from file.</param>
		/// <returns>Path to PowerShell.</returns>
		public string PowerShellPath(bool useCache = true)
		{
			if (_powerShellPath == null || !useCache)
			{
				lock (_lockObject)
				{
					if (_powerShellPath == null || !useCache)
					{
						if (!useCache)
						{
							this._configReader.RefreshConfiguration();
						}
						_powerShellPath = _configReader.ReadSingleNode(ConfigurationConstants.NodeName.POWERSHELL_PATH, ConfigurationConstants.Attribute.PATH);
					}
				}
			}

			return _powerShellPath;
		}

		//		/// <summary>
		//		/// Validates configuration file.
		//		/// </summary>
		//		/// <returns>True if configuration file is valid; False otherwise.</returns>
		//		public static bool ValidateConfiguration()
		//		{
		//			var xml = Resources.Load<TextAsset>(ConfigurationConstants.CONFIG_FILE_NAME).text;
		//			var schema = Properties.Resources.Schema;
		//
		//			return XmlValidator.Validate(xml, schema);
		//		}
	}
}