using NUnit.Framework;
using WhoIsThatServer.Storage.Utils;

namespace WhoIsThatServer.Storage.UnitTests
{
    public class RegexTests
    {
        [TestCase("Dom_test.jpg")]
        [TestCase("d-test.jpg")]
        [TestCase("d-Test.jpg")]
        [TestCase("d_test-t.jpg")]
        [TestCase("TestDom.jpg")]
        [Test]
        public void IsValidFileName_ShouldReturnTrue(string fileName)
        {
            Assert.IsTrue(fileName.IsFileNameValid());
        }
    }
}