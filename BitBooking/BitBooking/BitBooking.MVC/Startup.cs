using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BitBooking.MVC.Startup))]
namespace BitBooking.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
