using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IFilantrope.Startup))]
namespace IFilantrope
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
