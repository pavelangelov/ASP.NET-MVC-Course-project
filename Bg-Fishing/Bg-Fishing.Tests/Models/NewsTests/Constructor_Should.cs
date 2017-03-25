using System;

using NUnit.Framework;

using Bg_Fishing.Models;
using Bg_Fishing.Utils;

namespace Bg_Fishing.Tests.Models.NewsTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void NotThrow_IfCalledParameterlessConstructor()
        {
            // Arrange, Act 
            var news = new News();

            // Assert
            Assert.AreNotEqual(Guid.Empty, news.Id);
            Assert.IsNotNull(news.Comments);            
        }

        [Test]
        public void ThrowArgumentException_IfTitleIsNotValid()
        {
            // Arrange
            var invalidMinLengthTitle = new string('a', News.TitleMinLength - 1);
            var invalidMaxLengthTitle = new string('a', News.TitleMaxLength + 1);
            var validContent = new string('a', News.ContentMinLength);
            var validImageurl = "test url";
            var validDate = DateTime.UtcNow;

            // Act
            var messageMinLength = Assert.Throws<ArgumentException>(() => new News(invalidMinLengthTitle, validContent, validImageurl, validDate)).Message;
            var messageMaxLength = Assert.Throws<ArgumentException>(() => new News(invalidMaxLengthTitle, validContent, validImageurl, validDate)).Message;

            // Assert
            StringAssert.Contains("Title", messageMinLength);
            StringAssert.Contains("Title", messageMaxLength);
        }

        [Test]
        public void ThrowArgumentException_IfContentIsNotValid()
        {
            // Arrange
            var invalidMinLengthContent = new string('a', News.ContentMinLength - 1);
            var invalidMaxLengthContent = new string('a', News.ContentMaxLength + 1);
            var validTitle = new string('a', News.TitleMinLength);
            var validImageurl = "test url";
            var validDate = DateTime.UtcNow;

            // Act
            var messageMinLength = Assert.Throws<ArgumentException>(() => new News(validTitle, invalidMinLengthContent, validImageurl, validDate)).Message;
            var messageMaxLength = Assert.Throws<ArgumentException>(() => new News(validTitle, invalidMaxLengthContent, validImageurl, validDate)).Message;

            // Assert
            StringAssert.Contains("Content", messageMinLength);
            StringAssert.Contains("Content", messageMaxLength);
        }

        [Test]
        public void NotThrow_IfImageUrlIsNull_AndSetDefaultValue()
        {
            // Arrange
            var validTitle = new string('a', News.TitleMinLength);
            var validContent = new string('a', News.ContentMinLength);
            var validDate = DateTime.UtcNow;

            // Act
            var news = new News(validTitle, validContent, null, validDate);

            // Assert
            Assert.AreEqual(Constants.NewsDefaultImage, news.ImageUrl);
        }

        [Test]
        public void NotThrow_IfAllParametersAreValid_AndSetCorrectValues()
        {
            // Arrange
            var validTitle = new string('a', News.TitleMinLength);
            var validContent = new string('a', News.ContentMinLength);
            var validImageUrl = "test url";
            var validDate = DateTime.UtcNow;

            // Act
            var news = new News(validTitle, validContent, validImageUrl, validDate);

            // Assert
            Assert.AreEqual(validTitle, news.Title);
            Assert.AreEqual(validContent, news.Content);
            Assert.AreEqual(validImageUrl, news.ImageUrl);
            Assert.AreEqual(validDate, news.PostedOn);
        }
    }
}
