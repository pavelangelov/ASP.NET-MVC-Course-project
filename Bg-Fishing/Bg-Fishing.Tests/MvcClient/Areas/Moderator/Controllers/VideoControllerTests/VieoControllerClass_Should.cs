using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.VideoControllerTests
{
    [TestFixture]
    public class VieoControllerClass_Should
    {
        [Test]
        public void HaveAuthenticationAttribute_AndCheckForModeratorRole()
        {
            var baseType = typeof(ModeratorBaseController);

            var mockedVideoService = new Mock<IVideoService>();
            var mockedVideoFactory = new Mock<IVideoFactory>();
            var mockedDateProvider = new Mock<IDateProvider>();

            var lakeController = new VideoController(mockedVideoService.Object, mockedVideoFactory.Object, mockedDateProvider.Object);

            Assert.IsTrue(baseType.IsAssignableFrom(lakeController.GetType()));
            var methodsInfo = lakeController.GetType().GetMethods();

            foreach (var method in methodsInfo)
            {
                if (method.Name == "Add")
                {
                    var attributes = method.DeclaringType.BaseType.CustomAttributes.Any(a => a.NamedArguments.Any(n => n.MemberName == "Roles" && n.TypedValue.Value.ToString() == "Moderator"));
                    Assert.IsTrue(attributes);
                }
            }
        }
    }
}
