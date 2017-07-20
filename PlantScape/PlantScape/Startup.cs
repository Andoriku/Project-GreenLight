using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlantScape.Startup))]
namespace PlantScape
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
