using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Drink_Finder.Startup))]
namespace Drink_Finder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
