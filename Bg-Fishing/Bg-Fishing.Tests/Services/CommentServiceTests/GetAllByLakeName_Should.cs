using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.CommentServiceTests
{
    [TestFixture]
    public class GetAllByLakeName_Should
    {
        [Test]
        public void ReturnCorrectResult_IfLakeNameMatched()
        {
            // Arrange
            var mockedCollection = Utils.GetCommentsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Comments).Returns(mockedDbSet.Object);

            var commentService = new CommentService(mockedDbContext.Object);
            var searchedComment = mockedCollection[1];

            // Act
            var result = commentService.GetAllByLakeName(searchedComment.LakeName);

            // Assert
            Assert.IsTrue(result.Count() == 1);
            Assert.AreEqual(searchedComment.LakeName, result.First().LakeName);
            Assert.AreEqual(searchedComment.Username, result.First().Username);
        }

        [Test]
        public void ReturnCorrectResult_IfLakeNameNotMatched()
        {
            // Arrange
            var mockedCollection = Utils.GetCommentsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Comments).Returns(mockedDbSet.Object);

            var commentService = new CommentService(mockedDbContext.Object);

            // Act
            var result = commentService.GetAllByLakeName("invalid name");

            // Assert
            Assert.IsTrue(result.Count() == 0);
        }
    }
}
