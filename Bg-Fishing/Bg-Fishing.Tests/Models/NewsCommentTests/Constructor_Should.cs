using System;

using NUnit.Framework;

using Bg_Fishing.Models.Comments;

namespace Bg_Fishing.Tests.Models.NewsCommentTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_IfCalledParameterlessConstructor()
        {
            // Arrange, Act
            var comment = new NewsComment();

            // Assert
            Assert.AreNotEqual(Guid.Empty, comment.Id);
        }

        [Test]
        public void NotThrow_IfAllParametersAreValid_AndSetCorrectValues()
        {
            // Arrange
            var username = "test user";
            var content = "test content";
            var date = DateTime.UtcNow;

            // Act
            var comment = new NewsComment(content, username, date);

            // Assert
            Assert.AreEqual(content, comment.Content);
            Assert.AreEqual(username, comment.Username);
            Assert.AreEqual(date, comment.PostedOn);
        }
    }
}
