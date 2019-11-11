using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace UnityFeedback.Configuration
{
	public class XmlValidator
	{
		private readonly XmlSchemaSet _schema;

		public XmlValidator(string schema)
		{
			this._schema = new XmlSchemaSet();
			this._schema.Add(null, XmlReader.Create(new StringReader(schema)));
		}

		/// <summary>
		/// Validates xml against a schema.
		/// </summary>
		/// <param name="xml">Xml string.</param>
		/// <returns>Struct indicating weather xml is valid and error message if it's not.</returns>
		public ResultInformation Validate(string xml)
		{
			if (string.IsNullOrEmpty(xml))
			{
				throw new ArgumentException();
			}

			try
			{
				XmlDocument document = new XmlDocument
				{
					Schemas = this._schema
				};

				document.LoadXml(xml);

				document.Validate((o, e) => throw new XmlException(e.Message));
			}
			catch (Exception e)
			{
				return new ResultInformation
					{ IsValid = false, ErrorMessage = e.Message };
			}

			return new ResultInformation { IsValid = true, ErrorMessage = string.Empty};
		}

		public struct ResultInformation
		{
			public bool IsValid;
			public string ErrorMessage;
		}
	}
}