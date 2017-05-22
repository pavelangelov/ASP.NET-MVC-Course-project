﻿using System;

using Moq;
using NUnit.Framework;

using Bg_Fishing.MvcClient.ApiControllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.ApiControllers.ImagesControllerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentnullException_IfImageGalleryServiceIsNull()
        {
            // Arrange, Act & Assert

            var message = Assert.Throws<ArgumentNullException>(() => new ImagesController(null)).Message;
            StringAssert.Contains("imageGalleryService", message);
        }

        [Test]
        public void NotThrow_IfAllDependenciesAreValid()
        {
            // Arrange
            var mockedImageGalleryService = new Mock<IImageGalleryService>();

            // Act & Assert
            Assert.DoesNotThrow(() => new ImagesController(mockedImageGalleryService.Object));
        }
    }
}
