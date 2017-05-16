using Ninject.Modules;

using Bg_Fishing.Utils;
using Bg_Fishing.Utils.Contracts;

namespace Bg_Fishing.MvcClient.App_Start.NinjectModules
{
    public class CommonNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDateProvider>().To<DateProvider>();
            this.Bind<IDirectoryHelper>().To<DirectoryHelper>();
        }
    }
}