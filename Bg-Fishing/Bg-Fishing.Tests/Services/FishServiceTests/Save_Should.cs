using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Tests.Services.FishServiceTests
{
    [TestFixture]
    public class Save_Should
    {
        [Test]
        public void CallSaveMethod_FromDatabaseContext()
        {
            // Arrange
            var mockedDbContext = new Mock<IDatabaseContext>();
            mockedDbContext.Setup(c => c.Save()).Verifiable();

            var fishService = new FishService(mockedDbContext.Object);

            // Act
            fishService.Save();

            // Assert
            mockedDbContext.Verify(c => c.Save(), Times.Once);
        }
    }
}
