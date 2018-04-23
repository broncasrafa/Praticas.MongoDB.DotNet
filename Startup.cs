using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MongoDB.DotNet.Startup))]
namespace MongoDB.DotNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
