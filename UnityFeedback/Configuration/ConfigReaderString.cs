using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace UnityFeedback.Configuration
{
	public class ConfigReaderString : IConfigReader
	{
		private readonly string _configString;

		public ConfigReaderString(string configString)
		{
			if (string.IsNullOrEmpty(configString))
			{
				throw new ArgumentException();
			}
			// save string
			_configString = configString;
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