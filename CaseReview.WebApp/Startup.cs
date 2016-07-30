using Owin;

//[assembly: OwinStartupAttribute(typeof(CaseReview.WebApp.Startup))]
namespace IdentitySample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
