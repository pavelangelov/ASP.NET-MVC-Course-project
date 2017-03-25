using Bg_Fishing.Models;
using Bg_Fishing.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg_Fishing.Tests.Models.AppUserTests
{
    [TestFixture]
    public class PropertySet_Should
    {
        [TestCase(Constants.AgeMinValue - 1)]
        [TestCase(Constants.AgeMaxValue + 1)]
        public void ThrowArgumentException_IfAgeAreIsValid(int invalidAge)
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentException>(() => new AppUser() { Age = invalidAge }).Message;
            StringAssert.Contains("Age", message);
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ThrowArgumentException_IfFirstNameIsNotValid(string invalidName)
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentException>(() => new AppUser() { FirstName = invalidName }).Message;
            StringAssert.Contains("FirstName", message);
        }
        
        [Test]
        public void ThrowArgumentException_IfMiddleNameIsNotValid()
        {
            // Arrange, Act & Assert
            var invalidName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var message = Assert.Throws<ArgumentException>(() => new AppUser() { MiddleName = invalidName }).Message;
            StringAssert.Contains("MiddleName", message);
        }

        [Test]
        public void NotThrow_IfMiddleNameIsNull()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => new AppUser() { MiddleName = null });
        }

        [TestCase("")]
        [TestCase("a")]
        [TestCase("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void ThrowArgumentException_IfLastNameIsNotValid(string invalidName)
        {
            // Arrange, Act & Assert
            var message = Assert.Throws<ArgumentException>(() => new AppUser() { LastName = invalidName }).Message;
            StringAssert.Contains("LastName", message);
        }
    }
}
