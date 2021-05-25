using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FantasyCyclingWeb.Startup))]
namespace FantasyCyclingWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
