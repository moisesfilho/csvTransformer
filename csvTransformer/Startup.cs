using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(csvTransformer.Startup))]
namespace csvTransformer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
