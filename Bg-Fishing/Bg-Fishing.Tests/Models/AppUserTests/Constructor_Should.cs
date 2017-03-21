using System;

using NUnit.Framework;

using Bg_Fishing.Models;

namespace Bg_Fishing.Tests.Models.AppUserTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrowAndGenerateId_WhenCalledParameterlessConstructor()
        {
            // Arrange, Act
            var user = new AppUser();

            // Assert
            Assert.AreNotEqual(Guid.Empty, user.Id);
        }

        [Test]
        public void SetUserNameCorrectlly()
        {
            // Arrange & Act
            var username = "Username";
            var user = new AppUser(username);

            // Assert
            Assert.AreEqual(username, user.UserName);
        }
    }
}
