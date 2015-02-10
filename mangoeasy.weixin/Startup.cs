using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mangoeasy.weixin.Startup))]
namespace mangoeasy.weixin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
