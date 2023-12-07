using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunningJac.IDP.Services;
using RunningJac.IDP.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using IdentityServerHost.Quickstart.UI;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using RunningJac.IDP.Models;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4;
using System.ComponentModel.DataAnnotations;

namespace RunningJac.IDP.Quickstart.Registration
{
    [AllowAnonymous]
    [SecurityHeaders]
    public class RegistrationController : Controller
    {
        private readonly ILocalUserService _localUserService;
        private readonly IMailService _mailService;

        public RegistrationController(ILocalUserService localUserService, IMailService mailService)
        {
            _localUserService = localUserService;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var model = BuildRegistrationViewModel(returnUrl);
            return View(model);
        }

        public RegistrationInputModel BuildRegistrationViewModel(string returnUrl)
        {
            return new RegistrationInputModel()
            {
                ReturnUrl = returnUrl
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationInputModel model)
        {
            if (!ModelState.IsValid)
            {
                BuildRegistrationViewModel(model.ReturnUrl);
                return View(model);
            }

            #region Create New User
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = Guid.NewGuid().ToString(),
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Username = model.Username,
                Email = model.Email,
                IsActive = false,
            };
            user.Claims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Type = JwtClaimTypes.GivenName,
                Value = model.Firstname,
            });
            user.Claims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Type = JwtClaimTypes.FamilyName,
                Value = model.Lastname,
            });
            user.Claims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Type = JwtClaimTypes.Name,
                Value = model.Username,
            });
            user.Claims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Type = JwtClaimTypes.Email,
                Value = model.Email,
            });

            await _localUserService.CreateUserAsync(user, password: model.Password);
            await _localUserService.SaveChangesAsync();

            #endregion

            await SendMailAsync(user);
            return Redirect("~/Activation/ActivationCodeSent");
        }

        public async Task SendMailAsync(User user)
        {
            var emailViewModel = new EmailViewModel
            {
                Name = user.Username,
                VerificationLink = Url.Action("Activate", "Activation", new { securityCode = user.SecurityCode}, "https", "localhost:5001")
            };

            var originalHtml = _mailService.GetEmailTemplate("Welcome", emailViewModel);
            string modified = _localUserService.ReplaceText(originalHtml, "DynamicUsernameImplementation", user.Username);
            modified = _localUserService.ReplaceText(modified, "DynamicVerificationLinkImplementation", emailViewModel.VerificationLink);

            var mailData = new MailData(new List<string> { user.Email },"Welcome to CartCraft", body:modified);
            await _mailService.SendAsync(mailData);
        }
    }
}
