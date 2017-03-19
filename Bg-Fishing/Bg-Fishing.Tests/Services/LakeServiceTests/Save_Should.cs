using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;

namespace Bg_Fishing.Tests.Services.LakeServiceTests
{
    [TestFixture]
    public class Save_Should
    {
        [Test]
        public void CallSaveMethod_FromDbContext()
        {
            // Arrange
            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Save()).Verifiable();

            var lakeService = new LakeService(mockedDbContext.Object);

            // Act
            lakeService.Save();

            // Assert
            mockedDbContext.Verify(c => c.Save(), Times.Once);
        }
    }
}
