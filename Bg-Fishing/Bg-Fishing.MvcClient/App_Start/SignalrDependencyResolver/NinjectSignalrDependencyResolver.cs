using Microsoft.AspNet.SignalR;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bg_Fishing.MvcClient.App_Start.SignalrDependencyResolver
{
    internal class NinjectSignalrDependencyResolver : DefaultDependencyResolver
    {
        private readonly IKernel kernel;
        public NinjectSignalrDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}