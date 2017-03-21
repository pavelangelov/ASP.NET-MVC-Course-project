using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Models;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Tests.Models.LakeTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_WhenCalledParameterlessConstructor()
        {
            // Arrange & Act
            var lake = new Lake();

            // Assert
            Assert.AreNotEqual(lake.Id, Guid.Empty);
            Assert.IsNotNull(lake.Fish);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ThrowArgumentException_IfNameIsNotValid(string invalidName)
        {
            // Arrange, Act & Act
            var message = Assert.Throws<ArgumentException>(() => new Lake(invalidName, It.IsAny<Location>())).Message;
            StringAssert.Contains("Name", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfLocationIsNull()
        {
            // Arrange, Act & Act
            var message = Assert.Throws<ArgumentNullException>(() => new Lake("some name", null)).Message;
            StringAssert.Contains("Location", message);
        }

        [TestCase("aa")]
        [TestCase("Sofia")]
        [TestCase("1000 Lulin-Center, Sofia, Bulgaria")]
        public void NotThrow_IfNameAndLocationAreValid(string validName)
        {
            // Arrange, Act & Act
            var validLocation = new Location();
            Assert.DoesNotThrow(() => new Lake(validName, validLocation));
        }

        [Test]
        public void ThrowArgumentException_IfInfoIsNotValid()
        {
            // Arrange, Act & Assert
            var invalidInfo = new string('a', Constants.InfoMaxLEngth + 1);
            var validName = "Valid";
            var validlocation = new Location();

            var message = Assert.Throws<ArgumentException>(() => new Lake(validName, validlocation, invalidInfo)).Message;
            StringAssert.Contains("Info", message);
        }

        [Test]
        public void NotThrow_IfInfoIsValid()
        {
            // Arrange, Act & Assert
            var validInfo = new string('a', Constants.InfoMaxLEngth);
            var validName = "Valid";
            var validLocation = new Location();

            Assert.DoesNotThrow(() => new Lake(validName, validLocation, validInfo));
            Assert.DoesNotThrow(() => new Lake(validName, validLocation, ""));
        }
    }
}
