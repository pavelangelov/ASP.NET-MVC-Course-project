using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Models;
using Bg_Fishing.Models.Galleries;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.ImageGalleryServiceTests
{
    [TestFixture]
    public class GetByLake_Should
    {
        [Test]
        public void ReturnCorrectResult_IfNameMatch()
        {
            // Arrange
            var searchedLakeName = "Test lake";

            var mockedCollection = new List<ImageGallery>()
            {
                new ImageGallery() { Name = "Test gallery", Lake = new Lake() { Name = searchedLakeName } },
                new ImageGallery(){ Lake = new Lake() { Name = "Test" } },
               new ImageGallery() { Name = "Test gallery 2", Lake = new Lake() { Name = searchedLakeName } }
            };
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.ImageGalleries).Returns(mockedDbSet.Object);

            var imageGalleryService = new ImageGalleryService(mockedDbContext.Object);

            // Act
            var result = imageGalleryService.GetByLake(searchedLakeName);

            // Assert
            Assert.IsTrue(result.Count() == 2);
        }
    }
}
