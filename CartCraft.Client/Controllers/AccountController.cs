using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartCraft.Client.Controllers
{
    public class AccountController : Controller
    {
        public async Task Logout()
        {
            // signOut of client
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // signout of idp
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
