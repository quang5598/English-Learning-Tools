using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnglishLearningTools.Startup))]
namespace EnglishLearningTools
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
