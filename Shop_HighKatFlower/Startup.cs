using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shop_HighKatFlower.Startup))]
namespace Shop_HighKatFlower
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
