using System;
using Moq;
using NUnit.Framework;
using UnityFeedback.Configuration;
using UnityFeedback.Persistence;

namespace UnityFeedbackTest.Configuration
{
	[TestFixture]
	public class AppSettingsTest
	{
		private AppSettings _appSettings;
		private readonly Mock<IConfigReader> _mockConfigReader = new Mock<IConfigReader>();
		private bool _refreshedConfiguration;

		[SetUp]
		public void Setup()
		{
			// Arrange
			this._refreshedConfiguration = false;
			this._mockConfigReader.Setup(foo => foo.ReadSingleNode(ConfigurationConstants.NodeName.DATABASE_PROVIDER,
				ConfigurationConstants.Attribute.VALUE)).Returns("SqlServer");
			this._mockConfigReader.Setup(foo => foo.RefreshConfiguration())
				.Callback(() => { this._refreshedConfiguration = true; });
			this._appSettings = new AppSettings(_mockConfigReader.Object);
		}

		[TestCase("SqlServer", DatabaseProvider.SqlServer)]
		[TestCase("MySql", DatabaseProvider.MySql)]
		[TestCase("SQLite", DatabaseProvider.SQLite)]
		[TestCase("PostgreSQL", DatabaseProvider.PostgreSQL)]
		public void ShouldGetDatabaseProvider(string databaseProvider, DatabaseProvider expected)
		{
			// Arrange
			_mockConfigReader.Setup(foo => foo.ReadSingleNode(ConfigurationConstants.NodeName.DATABASE_PROVIDER,
				ConfigurationConstants.Attribute.VALUE)).Returns(databaseProvider);

			// Act
			var actual = _appSettings.DatabaseProvider();
			
			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestCase("INVALID")]
		[TestCase("SQLSERVER")]
		[TestCase("")]
		public void ShouldThrowExceptionWhenInvalidDatabaseProvider(string databaseProvider)
		{
			// Arrange
			_mockConfigReader.Setup(foo => foo.ReadSingleNode(ConfigurationConstants.NodeName.DATABASE_PROVIDER,
				ConfigurationConstants.Attribute.VALUE)).Returns(databaseProvider);

			// Act & Assert
			Assert.Throws<ArgumentException>(() => _appSettings.DatabaseProvider());
		}

		[Test]
		public void ShouldNotRefreshConfigurationWhenUseCache()
		{
			// Act
			this._appSettings.DatabaseProvider(useCache: true);

			// Assert
			Assert.False(this._refreshedConfiguration);
		}

		[Test]
		public void ShouldRefreshConfiguration()
		{
			// Act
			this._appSettings.DatabaseProvider(useCache: false);

			// Assert
			Assert.True(this._refreshedConfiguration);
		}
	}
}