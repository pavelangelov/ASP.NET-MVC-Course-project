using Ninject.Modules;
using Ninject.Extensions.Factory;

using Bg_Fishing.Factories.Contracts;

namespace Bg_Fishing.MvcClient.App_Start.NinjectModules
{
    public class FishingFactoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IVideoFactory>().ToFactory();
            this.Bind<IVideoGalleryFactory>().ToFactory();
            this.Bind<IImageFactory>().ToFactory();
            this.Bind<IImageGalleryFactory>().ToFactory();
            this.Bind<IFishFactory>().ToFactory();
            this.Bind<ILakeFactory>().ToFactory();
            this.Bind<ILocationFactory>().ToFactory();
        }
    }
}