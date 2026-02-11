using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Configuration;
using System.Text;
using System.Web.Configuration;

[assembly: OwinStartup(typeof(VelarisBackend.Startup))]
namespace VelarisBackend
{
    public class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            System.Diagnostics.Trace.WriteLine("OWIN STARTUP HIT");
            var secret = ConfigurationManager.AppSettings["JwtSecret"];
            var key = Encoding.UTF8.GetBytes(secret);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions{
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true
                    }
            });
        }
    }
}