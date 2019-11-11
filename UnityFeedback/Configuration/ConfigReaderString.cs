using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

namespace UnityFeedback.Configuration
{
	public class ConfigReaderString : IConfigReader
	{
		private string _configString;
		private readonly Func<string> _getConfigurationMethod;

		public ConfigReaderString(Func<string> getConfiguration)
		{
			this._configString = getConfiguration();
			this._getConfigurationMethod = getConfiguration;

			if (string.IsNullOrEmpty(this._configString))
			{
				throw new ArgumentException();
			}
		}

		/// <inheritdoc cref="IConfigReader"/>
		public string ReadSingleNode(string nodeName, string attribute)
		{
			if (string.IsNullOrEmpty(nodeName) || string.IsNullOrEmpty(attribute))
			{
				throw new ArgumentException();
			}

			using (var configStream = GetStream())
			using (var reader = XmlReader.Create(configStream))
			{
				string result = null;
				if (reader.ReadToFollowing(nodeName))
				{
					result = reader.GetAttribute(attribute);
				}

				return result;
			}
		}

		/// <inheritdoc cref="IConfigReader"/>
		public string[] ReadArrayNode(string nodeName, string attribute)
		{
			if (string.IsNullOrEmpty(nodeName) || string.IsNullOrEmpty(attribute))
			{
				throw new ArgumentException();
			}

			using (var configStream = GetStream())
			using (var reader = XmlReader.Create(configStream))
			{
				var result = new List<string>();
				while (reader.ReadToFollowing(nodeName))
				{
					result.Add(reader.GetAttribute(attribute));
				}

				return result.ToArray();
			}
		}

		///  <inheritdoc cref="IConfigReader"/>
		public void RefreshConfiguration()
		{
			this._configString = this._getConfigurationMethod();
		}

		/// <summary>
		/// Converts <see cref="string"/> to <see cref="Stream"/>.
		/// </summary>
		/// <returns><see cref="MemoryStream"/> from configuration string.</returns>
		private Stream GetStream()
		{
			var byteArray = Encoding.UTF8.GetBytes(_configString);
			return new MemoryStream(byteArray);
		}
	}
}