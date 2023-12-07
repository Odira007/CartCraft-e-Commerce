using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunningJac.IDP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningJac.IDP.Quickstart.Activation
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class ActivationController : Controller
    {
        private readonly ILocalUserService _localUserService;

        public ActivationController(ILocalUserService localUserService)
        {
            _localUserService = localUserService;
        }

        [HttpGet]
        public IActionResult ActivationCodeSent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Activate(string securityCode)
        {
            ActivationInputModel vm;
            if (await _localUserService.ActivateUserAsync(securityCode))
                vm = new ActivationViewModel { Message = "Account successfully activated" };
            else
            {
                vm = new ActivationViewModel { Message = "Activation Unsuccessful" };
            }

            bool savechanges = await _localUserService.SaveChangesAsync();
            await _localUserService.SaveChangesAsync();
            return View(vm);
        }
    }
}
