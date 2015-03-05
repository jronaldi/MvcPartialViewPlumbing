using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportSchedulerPrototype.Startup))]
namespace ReportSchedulerPrototype
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
