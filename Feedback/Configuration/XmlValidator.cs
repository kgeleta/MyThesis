using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Feedback.Configuration
{
	public class XmlValidator
	{
		/// <summary>
		/// Validates xml against a given schema. Returns true if xml is valid, throws <see cref="XmlException"/> otherwise.
		/// </summary>
		/// <param name="xml">Xml string.</param>
		/// <param name="schema">Xsd string.</param>
		public static bool Validate(string xml, string schema)
		{
//			string schema = Properties.Resources.schema;
//			Console.WriteLine(schema);

			if (string.IsNullOrEmpty(xml) || string.IsNullOrEmpty(schema))
			{
				throw new ArgumentException();
			}

			XmlSchemaSet schemaSet = new XmlSchemaSet();
			schemaSet.Add(null, XmlReader.Create(new StringReader(schema)));

			XmlDocument document = new XmlDocument
			{
				Schemas = schemaSet
			};

			document.LoadXml(xml);
			document.Validate((o, e) => throw new XmlException(e.Message));

			return true;
		}
	}
}