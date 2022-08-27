using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TripsBlogCoreProject.Areas.Admin.Models;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class ProfileController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.FirstName = result.FirstName;
            ViewBag.LastName = result.LastName;
            ViewBag.Email = result.Email;
            ViewBag.PhoneNumber = result.PhoneNumber;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileUpdate()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);
            ProfileUpdateViewModel model = new ProfileUpdateViewModel
            {
                FirstName = result.FirstName,
                Lastname = result.LastName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(ProfileUpdateViewModel profileUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.FirstName = profileUpdateViewModel.FirstName;
                user.LastName = profileUpdateViewModel.Lastname;
                user.Email = profileUpdateViewModel.Email;
                user.PhoneNumber = profileUpdateViewModel.PhoneNumber;
                if (profileUpdateViewModel.IsChecked == true)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, profileUpdateViewModel.Password);
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("SignIn", "login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(profileUpdateViewModel);
                    }

                }
                else
                {
                    //profileUpdateViewModel.Password = "123";
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("SignIn", "login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(profileUpdateViewModel);
                    }
                }
            }
            return View(profileUpdateViewModel);
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }
    }
}
