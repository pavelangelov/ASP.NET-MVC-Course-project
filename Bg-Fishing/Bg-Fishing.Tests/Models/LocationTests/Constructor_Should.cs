using System;

using NUnit.Framework;
using Bg_Fishing.Models;

using Bg_Fishing.Utils;

namespace Bg_Fishing.Tests.Models.LocationTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_AndGenerateId_IfCalledParameterlessConstructor()
        {
            // Arrange, Act & Assert
            var location = new Location();
            Assert.AreNotEqual(location.Id, Guid.Empty);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ThrowArgumentException_IfNameIsNotValid(string invalidName)
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentException>(() => new Location(1, 1, invalidName)).Message;
            StringAssert.Contains("Name", message);
        }

        [Test]
        public void ThrowArgumentException_IfInfoIsNotValid()
        {
            // Arrange, Act & Assert
            var validName = "Test";
            var invalidInfo = new string('a', Constants.InfoMaxLEngth + 1);
            var message = Assert.Throws<ArgumentException>(() => new Location(1, 1, validName, invalidInfo)).Message;
            StringAssert.Contains("Info", message);
        }

        [TestCase("aa")]
        [TestCase("TestName")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void NotThrow_ValuesAreValid(string validName)
        {
            // Arrange, Act & Assert
            var validInfo = new string('a', Constants.InfoMaxLEngth);
           Assert.DoesNotThrow(() => new Location(1, 1, validName, validInfo));
        }
    }
}
