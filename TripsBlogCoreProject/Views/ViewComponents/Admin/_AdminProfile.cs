using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Views.ViewComponents.Admin
{
    public class _AdminProfile : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _AdminProfile(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Name = result.FirstName + " " + result.LastName;
            ViewBag.Image = result.ImageUrl;
            return View();
        }
    }
}
