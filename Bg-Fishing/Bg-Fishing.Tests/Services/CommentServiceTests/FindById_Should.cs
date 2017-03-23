using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.CommentServiceTests
{
    [TestFixture]
    public class FindById_Should
    {
        [Test]
        public void ReturnCorrectResult_IfIdMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetCommentsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => mockedCollection.FirstOrDefault(d => d.Id == ids[0].ToString()));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Comments).Returns(mockedDbSet.Object);

            var commentService = new CommentService(mockedDbContext.Object);
            var searchedComment = mockedCollection[1];

            // Act
            var result = commentService.FindById(searchedComment.Id);

            // Assert
            Assert.AreEqual(searchedComment, result);
        }

        [Test]
        public void ReturnNull_IfIdNotMatch()
        {
            // Arrange
            var mockedCollection = Utils.GetCommentsCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Find(It.IsAny<object[]>())).Returns<object[]>(ids => mockedCollection.FirstOrDefault(d => d.Id == ids[0].ToString()));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Comments).Returns(mockedDbSet.Object);

            var commentService = new CommentService(mockedDbContext.Object);

            // Act
            var result = commentService.FindById("invalid id");

            // Assert
            Assert.IsNull(result);
        }
    }
}
