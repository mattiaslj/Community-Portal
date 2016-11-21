using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CommunityPortal.Startup))]
namespace CommunityPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
