using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DefyClinicWeb.Startup))]
namespace DefyClinicWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
