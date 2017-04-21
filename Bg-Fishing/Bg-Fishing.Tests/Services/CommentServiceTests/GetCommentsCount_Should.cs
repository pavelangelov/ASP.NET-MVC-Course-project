using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Models.Comments;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.CommentServiceTests
{
    [TestFixture]
    public class GetCommentsCount_Should
    {
        [Test]
        public void ReturnCorrectValue()
        {
            // Arrange
            var mockedCollection = Utils.GetCommentsCollection();
            var mockedLake = mockedCollection.First();
            mockedLake.Comments.Add(new InnerComment());
            var lakeName = mockedLake.LakeName;

            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Comments).Returns(mockedDbSet.Object);

            var commentService = new CommentService(mockedDbContext.Object);

            // Act
            var result = commentService.GetCommentsCount(lakeName);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
