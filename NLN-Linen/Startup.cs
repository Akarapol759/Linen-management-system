using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NLN_Linen.Startup))]
namespace NLN_Linen
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
