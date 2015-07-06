using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Budget.Web.Startup))]
namespace Budget.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
