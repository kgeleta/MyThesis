using System;
using System.IO;
using System.Text;

namespace UnityFeedback.Configuration
{
	/// <summary>
	/// This class can be used to generate default configuration file
	/// </summary>
	public class ConfigFileCreator
	{
		/// <summary>
		/// This method creates configuration file with default properties.
		/// </summary>
		/// <param name="path">Path to the location in which file should be created.</param>
		/// <param name="fileName">Name of the configuration file without extension.</param>
		/// <param name="overrideFileIfExists">Optional flag, by default set to false.</param>
		/// <returns>True if configuration file was successfully created; false otherwise.</returns>
		public static bool Create(string path, string fileName, bool overrideFileIfExists = false)
		{
			var fullPath = $@"{path}\{fileName}.xml";
			if (!overrideFileIfExists && File.Exists(fullPath))
			{
				return false;
			}

			try
			{
				using (var fileStream = File.Create(fullPath))
				{
					var configBytes = new UTF8Encoding(true).GetBytes(UnityFeedback.Properties.Resources.DefaultConfiguration);
					fileStream.Write(configBytes, 0, configBytes.Length);
				}
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}
	}
}