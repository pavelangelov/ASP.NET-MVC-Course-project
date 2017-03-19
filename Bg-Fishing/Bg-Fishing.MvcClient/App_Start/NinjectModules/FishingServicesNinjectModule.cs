using Ninject.Modules;

using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.MvcClient.App_Start.NinjectModules
{
    public class FishingServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDatabaseContext>().To<FishingContext>();

            this.Bind<IVideoService>().To<VideoService>();
            this.Bind<IFishService>().To<FishService>();
            this.Bind<ILocationService>().To<LocationService>();
            this.Bind<ILakeService>().To<LakeService>();
        }
    }
}