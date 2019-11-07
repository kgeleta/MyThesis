using NUnit.Framework;
using System;
using UnityFeedback.Configuration;

namespace UnityFeedbackTest.Configuration
{
	[TestFixture]
	public class ConfigReaderStringTest
	{
		private IConfigReader _configReader;

		[SetUp]
		public void Setup()
		{
			// Arrange
			_configReader = new ConfigReaderString(() => UnityFeedbackTest.Properties.Resources.Valid);
		}

		[Test]
		public void ShouldReadExistingArrayNode()
		{
			// Act
			var actual = _configReader.ReadArrayNode("scene", "path");
			var expected = new[]
			{
				"Assets/Scenes/Scene1.unity", "Assets/Scenes/Scene2.unity", "Assets/Scenes/Scene3.unity",
				"Assets/Scenes/Scene4.unity", "Assets/Scenes/Scene5.unity", "Assets/Scenes/Scene6.unity",
				"Assets/Scenes/Scene7.unity", "Assets/Scenes/Scene8.unity"
			};

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ShouldReadExistingSingleNode()
		{
			// Act
			var actual = _configReader.ReadSingleNode("connectionString", "value");
			var expected = @"Server=BMO\SQLEXPRESS;Database=kgeleta;User Id=admin;Password=Test1234!;";

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ShouldReturnNullIfNodeNotExist()
		{
			// Act & Assert
			Assert.Null(_configReader.ReadSingleNode("aaa", "aaa"));
		}

		[Test]
		public void ShouldThrowExceptionWhenNullOrEmpty()
		{
			// Act & Assert
			Assert.Throws<ArgumentException>(() => _configReader.ReadSingleNode(null, null));
			Assert.Throws<ArgumentException>(() => _configReader.ReadSingleNode(string.Empty, string.Empty));
		}

		[Test]
		public void ShouldRefreshConfiguration()
		{
			// Arrange
			var methodCallsCount = 0;
			this._configReader = new ConfigReaderString(() => {
				methodCallsCount++;
				return UnityFeedbackTest.Properties.Resources.Valid;
			});

			var beforeRefresh = methodCallsCount;

			// Act
			_configReader.RefreshConfiguration();

			// Assert
			Assert.True(methodCallsCount > beforeRefresh);
		}
	}
}