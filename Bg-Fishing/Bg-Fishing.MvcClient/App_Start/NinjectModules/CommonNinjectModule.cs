using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.App_Start.NinjectModules
{
    public class CommonNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDateProvider>().To<DateProvider>();
        }
    }
}