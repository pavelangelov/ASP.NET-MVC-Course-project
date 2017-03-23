using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;

namespace Bg_Fishing.Tests.Services.CommentServiceTests
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

            var commentService = new CommentService(mockedDbContext.Object);

            // Act
            commentService.Save();

            // Assert
            mockedDbContext.Verify(c => c.Save(), Times.Once);
        }
    }
}
