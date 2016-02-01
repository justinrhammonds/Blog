using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Blog.Models;
using Owin.Security.Providers.LinkedIn;
using Owin.Security.Providers.GitHub;
using Owin.Security.Providers.Yahoo;

namespace Blog
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
                clientId: "0000000040181C5C",
                clientSecret: "gmydiwWtqcot01cJ2XyZoI2KBnKTBUdm");

            app.UseTwitterAuthentication(
               consumerKey: "SK3LDbUmOap2Ar6pcmDGNKSwe",
               consumerSecret: "je1NuzQXsVTcEgBOg7EJ4n2iuYVamk9lnmo3J9ht5bGAInYEEO");

            app.UseFacebookAuthentication(
               appId: "750180768446665",
               appSecret: "38e213a04e757f301095ddaf5e1da5b8");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "991404432161-h4rom7b9niup83f4n8sockesnsuekfga.apps.googleusercontent.com",
                ClientSecret = "fTiLMtI45pFs3P9AexBjcit3"
            });

            app.UseLinkedInAuthentication(
                "77tiymodj809nm",
                "z2gout8EuDo4IHHt");

            app.UseGitHubAuthentication(new GitHubAuthenticationOptions()
            {
                ClientId = "a3c17e9c4e0215cd117d",
                ClientSecret = "64d88d8c65ad4af2663289ac8abcebe760864ff0"
            });

            //app.UseYahooAuthentication(new YahooAuthenticationOptions
            //{
            //    ConsumerKey = "dj0yJmk9N1RVQXJOUko4aEtxJmQ9WVdrOU5UWXhOVkI2TXpBbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD1mYw--",
            //    ConsumerSecret = "9f7c7ad36277ad1d36b6237dfbdb7283af4611fa"
            //});
        }
    }
}