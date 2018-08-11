using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoadScholar.Startup))]
namespace RoadScholar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
