using NUnit.Framework;
using UnityFeedback.Configuration;

namespace UnityFeedbackTest.Configuration
{
	[TestFixture]
	public class AppSettingsTest
	{
		private AppSettings _appSettings;


		[SetUp]
		public void Setup()
		{
			this._appSettings = new AppSettings(new ConfigReaderString(UnityFeedbackTest.Properties.Resources.Valid));
		}

		[Test]
		public void ShouldReadConnectionString()
		{
			var actual = _appSettings.ConnectionString();
			var expected = @"Server=BMO\SQLEXPRESS;Database=kgeleta;User Id=admin;Password=Test1234!;";

			Assert.AreEqual(expected, actual);
		}
	}
}