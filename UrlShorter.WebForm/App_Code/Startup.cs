using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UrlShorter.WebForm.Startup))]
namespace UrlShorter.WebForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            
        }
    }
}
