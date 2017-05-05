using Dashboard.App;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Dashboard.App
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
