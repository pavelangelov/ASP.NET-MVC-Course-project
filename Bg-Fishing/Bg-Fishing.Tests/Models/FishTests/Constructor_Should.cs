using System;

using NUnit.Framework;

using Bg_Fishing.Models;
using Bg_Fishing.Models.Enums;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Tests.Models.FishTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrowAndGenerateId_WhenCalledParameterlessConstructor()
        {
            // Arrange, Act
            var fish = new Fish();

            // Assert
            Assert.AreNotEqual(Guid.Empty, fish.Id);
            Assert.IsNotNull(fish.Lakes);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ThrowArgumentException_IfNameIsNotValid(string invalidName)
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentException>(() => new Fish(invalidName, FishType.FreshAndSaltWather, "url")).Message;
            StringAssert.Contains("Name", message);
        }

        [Test]
        public void ThrowArgumentException_IfInfoIsNotValid()
        {
            // Arrange, Act & Assert
            var validName = "Test";
            var invalidInfo = new string('a', Constants.InfoMaxLEngth + 1);

            var message = Assert.Throws<ArgumentException>(() => new Fish(validName, FishType.FreshAndSaltWather, "url", invalidInfo)).Message;
            StringAssert.Contains("Info", message);
        }

        [Test]
        public void NotThrow_IfAllParametersAreValid()
        {
            // Arrange, Act
            var validName = "Test";
            var fishType = FishType.FreshAndSaltWather;
            var url = "url";
            var validInfo = new string('a', Constants.InfoMaxLEngth);
            
            var fish =  new Fish(validName, fishType, url, validInfo);

            // Assert
            Assert.AreEqual(validName, fish.Name);
            Assert.AreEqual(fishType, fish.FishType);
            Assert.AreEqual(url, fish.ImageUrl);
            Assert.AreEqual(validInfo, fish.Info);
        }
    }
}
