using System.Linq;

using Moq;
using NUnit.Framework;

using Bg_Fishing.Factories.Contracts;
using Bg_Fishing.MvcClient.Areas.Moderator;
using Bg_Fishing.MvcClient.Areas.Moderator.Controllers;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.Tests.MvcClient.Areas.Moderator.Controllers.LakeControllerTests
{
    [TestFixture]
    public class LakeControllerClass_Should
    {
        [Test]
        public void HaveAuthenticationAttribute_AndCheckForModeratorRole()
        {
            var baseType = typeof(ModeratorBaseController);

            var mockedLakeFactory = new Mock<ILakeFactory>();
            var mockedLocationFactory = new Mock<ILocationFactory>();
            var mockedLakeService = new Mock<ILakeService>();
            var mockedLocationService = new Mock<ILocationService>();

            var lakeController = new LakeController(mockedLakeFactory.Object, mockedLocationFactory.Object, mockedLakeService.Object, mockedLocationService.Object);

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
