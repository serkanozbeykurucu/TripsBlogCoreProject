using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using TripsBlogCoreProject.Email;
using TripsBlogCoreProject.Models;

namespace TripsBlogCoreProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(WriterSignInModel writer)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByNameAsync(writer.Username);
                if (appUser != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(writer.Username, writer.Password, writer.RememberMe, true);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Writer" });
                    }
                    bool emailStatus = await _userManager.IsEmailConfirmedAsync(appUser);
                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(writer.Username), "E-posta onaylanmadı, lütfen önce onaylayın.");
                    }
                }
                ModelState.AddModelError("", "Giriş Başarısız: Geçersiz E-posta veya şifre");
            }
            return View(writer);
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(WriterSignUpModel writer)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    FirstName = writer.FirstName,
                    LastName = writer.LastName,
                    UserName = writer.Username,
                    Email = writer.EMail,
                    PhoneNumber = writer.PhoneNumber,
                    ImageUrl = "UserImage/man.png",
                    TwoFactorEnabled = false
                };
                if (writer.Password == writer.ConfirmPassword)
                {
                    var result = await _userManager.CreateAsync(appUser, writer.Password);
                    var addRoleToWriter = await _userManager.AddToRoleAsync(appUser, "Writer");
                    if (result.Succeeded && addRoleToWriter.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                        var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = writer.EMail }, Request.Scheme);
                        EmailHelper emailHelper = new EmailHelper();
                        bool emailResponse = emailHelper.SendEmailConfirmation(writer.EMail, confirmationLink);
                        if (emailResponse)
                            return RedirectToAction("SignIn", "Login");
                        else
                        {
                            //
                        }

                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
            }
            return View(writer);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            if (!ModelState.IsValid)
                return View(email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction("ForgotPasswordConfirmation");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Login", new { token, email = user.Email }, Request.Scheme);
            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);
            if (emailResponse)
                return RedirectToAction("ForgotPasswordConfirmation");
            else
            {

            }
            return View(email);
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, 
                resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


    }
}
