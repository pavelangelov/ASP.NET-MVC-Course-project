using NUnit.Framework;

using Bg_Fishing.Models;

namespace Bg_Fishing.Tests.Models.AppUserTests
{
    [TestFixture]
    public class PropertyGet_Should
    {
        [Test]
        public void ReturnCorrectValue_WhenGetUserType()
        {
            // Arrange & Act
            var userType = UserType.Moderator;
            var user = new AppUser() { UserType = userType };

            // Assert
            Assert.AreEqual(userType, user.UserType);
        }

        [Test]
        public void ReturnCorrectValue_WhenGetAge()
        {
            // Arrange & Act
            var age = 22;
            var user = new AppUser() { Age = age };

            // Assert
            Assert.AreEqual(age, user.Age);
        }

        [Test]
        public void ReturnCorrectValue_WhenGetFirstName()
        {
            // Arrange & Act
            var name = "Test";
            var user = new AppUser() { FirstName = name };

            // Assert
            Assert.AreEqual(name, user.FirstName);
        }

        [Test]
        public void ReturnCorrectValue_WhenGetMiddleName()
        {
            // Arrange & Act
            var name = "Test";
            var user = new AppUser() { MiddleName = name };

            // Assert
            Assert.AreEqual(name, user.MiddleName);
        }

        [Test]
        public void ReturnCorrectValue_WhenGetLastName()
        {
            // Arrange & Act
            var name = "Test";
            var user = new AppUser() { LastName = name };

            // Assert
            Assert.AreEqual(name, user.LastName);
        }

        [Test]
        public void ReturnCorrectValue_WhenGetAvvatarUrl()
        {
            // Arrange & Act
            var url = "Test url";
            var user = new AppUser() { AvatarUrl = url };

            // Assert
            Assert.AreEqual(url, user.AvatarUrl);
        }
    }
}
