using System.Linq;
using System.Reflection;

using Ninject.Modules;
using Ninject.Web.Common;

using Bg_Fishing.Data;
using Bg_Fishing.Services.Contracts;

namespace Bg_Fishing.MvcClient.App_Start.NinjectModules
{
    public class FishingServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IDatabaseContext>().To<FishingContext>().InRequestScope();
            
            var kernel = this;
            var a = Assembly.GetAssembly(typeof(IVideoService));

            a.GetTypes()
             .Where(type => type.IsInterface && type.Name.EndsWith("Service"))
             .ToList()
             .ForEach(serviceInterface =>
             {
                 var serviceClass = a.GetTypes()
                                     .First(t => t.IsClass && serviceInterface.IsAssignableFrom(t));

                 kernel.Bind(serviceInterface).To(serviceClass);
             });
        }
    }
}