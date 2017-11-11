using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DS.Startup))]
namespace DS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
