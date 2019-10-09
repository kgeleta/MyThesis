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
            _configReader = new ConfigReaderString(UnityFeedbackTest.Properties.Resources.Valid);
        }

        [Test]
        public void ShouldReadExistingArrayNode()
        {
            var actual = _configReader.ReadArrayNode("scene", "path");
            var expected = new[]
            {
                "Assets/Scenes/Scene1.unity", "Assets/Scenes/Scene2.unity", "Assets/Scenes/Scene3.unity",
                "Assets/Scenes/Scene4.unity", "Assets/Scenes/Scene5.unity", "Assets/Scenes/Scene6.unity",
                "Assets/Scenes/Scene7.unity", "Assets/Scenes/Scene8.unity"
            };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldReadExistingSingleNode()
        {
            var actual = _configReader.ReadSingleNode("connectionString", "value");
            var expected = @"Server=BMO\SQLEXPRESS;Database=kgeleta;User Id=admin;Password=Test1234!;";

			Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldReturnNullIfNodeNotExist()
        {
			Assert.Null(_configReader.ReadSingleNode("aaa", "aaa"));
        }

        [Test]
        public void ShouldThrowExceptionWhenNullOrEmpty()
        {
	        Assert.Throws<ArgumentException>(() => _configReader.ReadSingleNode(null, null));
	        Assert.Throws<ArgumentException>(() => _configReader.ReadSingleNode(string.Empty, string.Empty));
        }
	}
}