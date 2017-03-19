using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Moq;

using Bg_Fishing.Data;
using Bg_Fishing.Models;
using Bg_Fishing.Tests.Services.Mocks;
using Bg_Fishing.Services;

namespace Bg_Fishing.Tests.Services.LakeServiceTests
{
    [TestFixture]
    public class Add_Should
    {
        [Test]
        public void AddLakeToDbContext()
        {
            // Arrange
            var lake = new Lake() { Name = "Test lake" };
            var mockedCollection = new List<Lake>();
            var mockedDbSet = MockDbSet.Mock(mockedCollection.AsQueryable());
            mockedDbSet.Setup(d => d.Add(It.IsAny<Lake>())).Callback<Lake>((l) => mockedCollection.Add(l));

            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Lakes).Returns(mockedDbSet.Object);

            var lakeService = new LakeService(mockedDbContext.Object);

            // Act
            lakeService.Add(lake);

            // Assert
            Assert.IsTrue(mockedCollection.Count == 1);
            Assert.AreEqual(lake, mockedCollection[0]);
        }
    }
}
