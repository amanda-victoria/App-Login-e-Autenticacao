using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(AppLoginAutenticacao.App_Start.Startup))]

namespace AppLoginAutenticacao.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions{
            AuthenticationType = "AppAplicationCookies",
            LoginPath = new PathString("/Autenticacao/Login")

            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";
        }
    }
}
