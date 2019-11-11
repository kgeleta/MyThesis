using NUnit.Framework;
using System;
using UnityFeedback.Configuration;

namespace UnityFeedbackTest.Configuration
{
	[TestFixture]
	public class XmlValidatorTest
	{
		private XmlValidator _validator;

		[SetUp]
		public void Setup()
		{
			// Arrange
			this._validator = new XmlValidator(UnityFeedbackTest.Properties.Resources.Schema);
		}

		[Test]
		public void ShouldReturnTrueWhenValidXml()
		{
			// Act
			var result = this._validator.Validate(UnityFeedbackTest.Properties.Resources.Valid);

			// Assert
			Assert.True(result.IsValid);
		}

		[Test]
		public void ShouldReturnFalseWhenEmptyScenes()
		{
			// Act
			var result = this._validator.Validate(UnityFeedbackTest.Properties.Resources.EmptyScenes);

			// Assert
			Assert.False(result.IsValid);
			Console.WriteLine(result.ErrorMessage);
		}

		[Test]
		public void ShouldReturnFalseWhenNoConnectionString()
		{
			// Arrange & Act
			var result = this._validator.Validate(UnityFeedbackTest.Properties.Resources.NoConnectionString);

			// Assert
			Assert.False(result.IsValid);
			Console.WriteLine(result.ErrorMessage);
		}

		[Test]
		public void ShouldReturnFalseWhenNoRootElement()
		{
			// Arrange & Act
			var result = this._validator.Validate(UnityFeedbackTest.Properties.Resources.NoRootElement);

			// Assert
			Assert.False(result.IsValid);
			Console.WriteLine(result.ErrorMessage);
		}

		[Test]
		public void ShouldReturnFalseWhenNoScenes()
		{
			// Arrange & Act
			var result = this._validator.Validate(UnityFeedbackTest.Properties.Resources.NoScenes);

			// Assert
			Assert.False(result.IsValid);
			Console.WriteLine(result.ErrorMessage);
		}

		[Test]
		public void ShouldReturnFalseWhenInvalidDatabaseProvider()
		{
			// Arrange & Act
			var result = this._validator.Validate(UnityFeedbackTest.Properties.Resources.InvalidDatabaseProvider);

			// Assert
			Assert.False(result.IsValid);
			Console.WriteLine(result.ErrorMessage);
		}
	}
}