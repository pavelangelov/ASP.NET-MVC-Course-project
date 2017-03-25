using System;

using NUnit.Framework;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Tests.Models.Galleries.ImageTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_IfCalledParameterlessConstructor()
        {
            // Arrange & Act
            var image = new Image();

            // Assert
            Assert.AreNotEqual(Guid.Empty, image.Id);
        }

        [Test]
        public void NotThrow_AndSetCorrectValues()
        {
            // Arrange
            var url = "Test url";
            var date = DateTime.UtcNow;
            var info = "Test info";

            // Act
            var image = new Image(url, date, info);

            // Assert
            Assert.AreEqual(url, image.ImageUrl);
            Assert.AreEqual(date, image.Date);
            Assert.AreEqual(info, image.Info);
        }
    }
}
