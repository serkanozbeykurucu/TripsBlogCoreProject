using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TripsBlogCoreProject.Areas.Writer.Models;

namespace TripsBlogCoreProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
    [Authorize(Roles = "Writer,Admin")]
    public class ProfileController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult>Index()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);
            if (result != null)
            {
                ViewBag.FirstName = result.FirstName;
                ViewBag.LastName = result.LastName;
                ViewBag.Email = result.Email;
                ViewBag.PhoneNumber = result.PhoneNumber;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ProfileUpdate()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            WriterEditModel p = new WriterEditModel
            {
                FirstName = values.FirstName,
                LastName = values.LastName,
                EMail = values.Email
            };
            return View(p);
        }
        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(WriterEditModel p)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            values.FirstName = p.FirstName;
            values.LastName = p.LastName;
            values.Email = p.EMail;
            var  result = await _userManager.UpdateAsync(values);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }
    }
}
