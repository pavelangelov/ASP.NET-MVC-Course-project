using Bg_Fishing.Data;
using Bg_Fishing.Services;
using Bg_Fishing.Services.Contracts;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.App_Start.NinjectModules
{
    public class FishingServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDatabaseContext>().To<FishingContext>();

            this.Bind<IVideoService>().To<VideoService>();
        }
    }
}