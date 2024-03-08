using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Google.Apis.Auth;
using IdentityServer4;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityServerHost.Quickstart.UI
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class GoogleController : Controller
    {
        private readonly ILogger<GoogleController> _logger;
        private readonly TestUserStore _users;

        public GoogleController(ILogger<GoogleController> logger,
            TestUserStore users = null)
        {
            _logger = logger;
            _users = users ?? new TestUserStore(TestUsers.Users);
        }

        /// <summary>
        /// Google one tap sign in handler
        /// </summary>
        /// <returns></returns>
        [HttpPost("/api/one-tap-google")]
        public async Task<IActionResult> GoogleOneTapAsync()
        {
            var idToken = Request.Form["credential"];
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(idToken).ConfigureAwait(false);
            
            AuthenticationProperties props = null;
            if (AccountOptions.AllowRememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                };
            };
            var user = _users.FindByUsername(payload.Name);

            // issue authentication cookie with subject ID and username
            var isuser = new IdentityServerUser(user.SubjectId)
            {
                DisplayName = user.Username
            };
            
            var redirectUrl = Request.Form["redirect_uri"].ToString();

            await HttpContext.SignInAsync(isuser, props);

            return RedirectPermanent(redirectUrl);
            // return Redirect(redirectUrl2);

            // var returnUrl = Request.Headers["Referer"].ToString();
            //
            // if (result.Succeeded)
            //     return Redirect(returnUrl);
            // if (result.IsLockedOut)
            //     return RedirectToPage("./Lockout");
            // else
            // {
            //     // If the user does not have an account, then create an account.
            //     var user = new IdentityUser { UserName = payload.Email, Email = payload.Email,  EmailConfirmed = true,  };
            //     await _userManager.CreateAsync(user).ConfigureAwait(false);
            //     // Add external Google login
            //     await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerKey, provider)).ConfigureAwait(false);
            //     // Sign-in the user
            //     await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
            //
            //     return Redirect(returnUrl);
            // }
        }
    }
}