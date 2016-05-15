using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaseReview.WebApp.Startup))]
namespace CaseReview.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
