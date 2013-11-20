using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Exam.Mvc.Startup))]
namespace Exam.Mvc
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
