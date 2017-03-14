using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;

namespace Bg_Fishing.Tests.Services.FishServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfDatabaseContextIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new FishService(null)).Message;
            StringAssert.Contains("dbContext", message);
        }

        [Test]
        public void NotToThrow_IfDatabaseContextIsNotNull()
        {
            // Arrange
            var mockedDbContext = new Mock<IDatabaseContext>();

            // Act & Assert
            Assert.DoesNotThrow(() => new FishService(mockedDbContext.Object));
        }
    }
}
