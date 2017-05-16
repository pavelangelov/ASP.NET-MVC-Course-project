using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;

namespace Bg_Fishing.Tests.Services.ImageGalleryServiceTests
{
    [TestFixture]
    public class Save_Should
    {
        [Test]
        public void CallSaveMethod_FromDbContext()
        {
            // Arrange
            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(d => d.Save()).Verifiable();

            var imageGalleryService = new ImageGalleryService(mockedDbContext.Object);

            // Act
            imageGalleryService.Save();

            // Assert
            mockedDbContext.Verify(d => d.Save(), Times.Once);
        }
    }
}
