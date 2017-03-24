using System;

using NUnit.Framework;

using Bg_Fishing.Models;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Tests.Models.CommentTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_WhenCalledParameterlessConstructor()
        {
            // Arrange & Act
            var comment = new Comment();

            // Assert
            Assert.AreNotEqual(comment.Id, Guid.Empty);
            Assert.IsNotNull(comment.Comments);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ThrowArgumentException_IfLakeNameIsNotValid(string invalidLakeName)
        {
            // Arrange
            var validUsername = "test user";
            var validContent = "test content";
            var validDate = DateTime.UtcNow;

            // Act & Assert
            var message = Assert.Throws<ArgumentException>(() => new Comment(invalidLakeName, validUsername, validContent, validDate)).Message;
            StringAssert.Contains("LakeName", message);
        }

        [Test]
        public void ThrowArgumentNullException_IfUsernameIsNull()
        {
            // Arrange
            string invalidUserame = null;
            var validLakeName = "test lake";
            var validContent = "test content";
            var validDate = DateTime.UtcNow;

            // Act & Assert
            var message = Assert.Throws<ArgumentNullException>(() => new Comment(validLakeName, invalidUserame, validContent, validDate)).Message;
            StringAssert.Contains("Username", message);
        }

        [Test]
        public void ThrowArgumentException_IfContentIsNotValid()
        {
            // Arrange
            var validLakeName = "test lake";
            var validUserame = "test user";
            var invalidContent = new string('a', Constants.CommentContentMaxLength + 1);
            var validDate = DateTime.UtcNow;

            // Act & Assert
            var message = Assert.Throws<ArgumentException>(() => new Comment(validLakeName, validUserame, invalidContent, validDate)).Message;
            StringAssert.Contains("Content", message);
        }

        [Test]
        public void NotThrow_IfPassedParametersAreValid()
        {
            // Arrange
            var validLakeName = "test lake";
            var validUserame = "test user";
            var validContent = new string('a', Constants.CommentContentMaxLength);
            var validDate = DateTime.UtcNow;

            // Act
            var comment =  new Comment(validLakeName, validUserame, validContent, validDate);

            // Assert
            Assert.AreEqual(validLakeName, comment.LakeName);
            Assert.AreEqual(validUserame, comment.Username);
            Assert.AreEqual(validContent, comment.Content);
            Assert.AreEqual(validDate, comment.PostedDate);
        }
    }
}
