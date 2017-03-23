using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.CommentServiceTests
{
    [TestFixture]
    public class GetAllByUsername_Should
    {
        [Test]
        public void ReturnCorrectResult_IfUsernameMatched()
        {
            // Arrange
            var mockedCollection = Utils.GetCommentsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Comments).Returns(mockedDbSet.Object);

            var commentService = new CommentService(mockedDbContext.Object);
            var searchedComment = mockedCollection[1];

            // Act
            var result = commentService.GetAllByUsername(searchedComment.Username);

            // Assert
            Assert.IsTrue(result.Count() == 1);
            Assert.AreEqual(searchedComment.LakeName, result.First().LakeName);
            Assert.AreEqual(searchedComment.Username, result.First().Username);
        }

        [Test]
        public void ReturnCorrectResult_IfUsernameNotMatched()
        {
            // Arrange
            var mockedCollection = Utils.GetCommentsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Comments).Returns(mockedDbSet.Object);

            var commentService = new CommentService(mockedDbContext.Object);

            // Act
            var result = commentService.GetAllByUsername("invalid name");

            // Assert
            Assert.IsTrue(result.Count() == 0);
        }
    }
}
