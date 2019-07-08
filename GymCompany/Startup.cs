using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymCompany.Startup))]
namespace GymCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
