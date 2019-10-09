using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Xml;

namespace UnityFeedback.Configuration
{
    public class ConfigReaderFile : IConfigReader
    {
        private readonly string _filePath;

        /// <summary>
        /// Creates new instance of <see cref="ConfigReaderFile"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"><param name="filePath"/> is null.</exception>
        /// <exception cref="SecurityException">The <see cref="XmlReader"/> does not have sufficient permissions to access the location of the XML data.</exception>
        /// <exception cref="FileNotFoundException">The file identified by the <param name="filePath"/> does not exist.</exception>
        /// <param name="filePath"></param>
        public ConfigReaderFile(string filePath)
        {
            XmlReader.Create(filePath);
            _filePath = filePath;
        }

        /// <inheritdoc cref="IConfigReader"/>
        public string ReadSingleNode(string nodeName, string attribute)
        {
            using (var reader = XmlReader.Create(_filePath))
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
            using (var reader = XmlReader.Create(_filePath))
            {
                var result = new List<string>();
                while (reader.ReadToFollowing(nodeName))
                {
                    result.Add(reader.GetAttribute(attribute));
                }

                return result.ToArray();
            }
        }
    }
}