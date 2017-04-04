using System.Linq;
using System.Reflection;

using Ninject.Modules;
using Ninject.Extensions.Factory;

using Bg_Fishing.Factories.Contracts;

namespace Bg_Fishing.MvcClient.App_Start.NinjectModules
{
    public class FishingFactoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            var kernel = this;
            Assembly.GetAssembly(typeof(IVideoFactory))
                                       .GetTypes()
                                       .Where(t => t.IsInterface)
                                       .ToList()
                                       .ForEach(t => kernel.Bind(t).ToFactory(t));
        }
    }
}