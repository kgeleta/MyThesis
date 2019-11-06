using NUnit.Framework;
using System;
using System.Xml;
using UnityFeedback.Configuration;

namespace UnityFeedbackTest.Configuration
{
	[TestFixture]
	public class XmlValidatorTest
	{
		[Test]
		public void ShouldReturnTrueWhenValidXml()
		{
			Assert.True(XmlValidator.Validate(UnityFeedbackTest.Properties.Resources.Valid, UnityFeedbackTest.Properties.Resources.Schema));
		}

		[Test]
		public void ShouldThrowXmlExceptionWhenEmptyScenes()
		{
			var e = Assert.Throws<XmlException>(() => XmlValidator.Validate(UnityFeedbackTest.Properties.Resources.EmptyScenes,
				UnityFeedbackTest.Properties.Resources.Schema));
			Console.WriteLine(e.Message);
		}

		[Test]
		public void ShouldThrowXmlExceptionWhenNoConnectionString()
		{
			var e = Assert.Throws<XmlException>(() => XmlValidator.Validate(UnityFeedbackTest.Properties.Resources.NoConnectionString,
				UnityFeedbackTest.Properties.Resources.Schema));
			Console.WriteLine(e.Message);
		}

		[Test]
		public void ShouldThrowXmlExceptionWhenNoRootElement()
		{
			var e = Assert.Throws<XmlException>(() => XmlValidator.Validate(UnityFeedbackTest.Properties.Resources.NoRootElement,
				UnityFeedbackTest.Properties.Resources.Schema));
			Console.WriteLine(e.Message);
		}

		[Test]
		public void ShouldThrowXmlExceptionWhenNoScenes()
		{
			var e = Assert.Throws<XmlException>(() => XmlValidator.Validate(UnityFeedbackTest.Properties.Resources.NoScenes,
				UnityFeedbackTest.Properties.Resources.Schema));
			Console.WriteLine(e.Message);
		}

		[Test]
		public void ShouldThrowXmlExceptionWhenInvalidDatabaseProvider()
		{
			var e = Assert.Throws<XmlException>(() =>
				XmlValidator.Validate(UnityFeedbackTest.Properties.Resources.InvalidDatabaseProvider,
					UnityFeedbackTest.Properties.Resources.Schema));
			Console.WriteLine($@"Error at line {e.LineNumber}, position {e.LinePosition}: {e.Message}");
		}
	}
}