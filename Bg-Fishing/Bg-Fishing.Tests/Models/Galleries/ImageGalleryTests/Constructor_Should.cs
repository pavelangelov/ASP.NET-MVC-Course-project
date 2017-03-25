using System;

using NUnit.Framework;

using Bg_Fishing.Models.Galleries;

namespace Bg_Fishing.Tests.Models.Galleries.ImageGalleryTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_IfCalledParameterlessConstructor()
        {
            // Arrange & Act
            var imageGallery = new ImageGallery();

            // Assert
            Assert.AreNotEqual(Guid.Empty, imageGallery.Id);
            Assert.IsNotNull(imageGallery.Images);
        }

        [Test]
        public void NotThrow_AndSetCorrectValues()
        {
            // Arrange
            var name = "Test";
            var lakeId = Guid.NewGuid().ToString();

            // Act
            var imageGallery = new ImageGallery(name, lakeId);

            // Assert
            Assert.AreEqual(name, imageGallery.Name);
            Assert.AreEqual(lakeId, imageGallery.LakeId);
        }
    }
}
