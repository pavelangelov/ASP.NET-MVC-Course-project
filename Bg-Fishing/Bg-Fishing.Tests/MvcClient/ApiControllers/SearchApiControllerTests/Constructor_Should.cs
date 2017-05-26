using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.SearchApiControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_IfLakeServiceIsNull()
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new SearchApiController(null)).Message;
            StringAssert.Contains("lakeService", message);
        }

        [Test]
        public void NotThrow_IfLakeServiceIsNotNull()
        {
            // Arrange
            var mockedLakeService = new Mock<ILakeService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new SearchApiController(mockedLakeService.Object));
        }
    }
}
