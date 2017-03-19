using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Tests.Services.Mocks;

namespace Bg_Fishing.Tests.Services.LakeServiceTests
{
    [TestFixture]
    public class GetAll_Should
    {
        [Test]
        public void ReturnCorrectResult()
        {
            // Arrange
            var mockedCollection = Utils.GetLakesCollection();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Lakes).Returns(mockedDbSet.Object);

            var lakeService = new LakeService(mockedDbContext.Object);

            // Act
            var result = lakeService.GetAll();

            // Assert
            Assert.IsTrue(result.Count() == mockedCollection.Count);
            var index = 0;
            foreach (var lake in result)
            {
                Assert.AreEqual(mockedCollection[index].Name, lake.Name);
                Assert.AreEqual(mockedCollection[index].Id, lake.Id);
                index++;
            }
        }
    }
}
