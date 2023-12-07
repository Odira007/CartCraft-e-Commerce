using CartCraft.Client.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CartCraft.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            await GetIdentityDetails();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task GetIdentityDetails()
        {
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            _logger.LogInformation($"Identity Token: {identityToken}\n\n\tAccess Token: {accessToken}");

            foreach (var claim in User.Claims)
                Console.WriteLine($"\tClaimType:{claim.Type}, ClaimValue:{claim.Value}");

            
        }
    }
}
