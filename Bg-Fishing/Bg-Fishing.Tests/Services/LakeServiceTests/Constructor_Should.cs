using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;

namespace Bg_Fishing.Tests.Services.LakeServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfDbContextIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new LakeService(null)).Message;
            StringAssert.Contains("dbContext", message);
        }

        [Test]
        public void NotThrow_IfDbContextIsNotNull()
        {
            // Arrange
            var mockedDbContext = new Mock<IDatabaseContext>();
            Assert.DoesNotThrow(() => new LakeService(mockedDbContext.Object));
        }
    }
}
