using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bg_Fishing.MvcClient.Startup))]
namespace Bg_Fishing.MvcClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
