using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Views.ViewComponents.Writer
{
    public class _WriterSidebarProfile : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        
        public _WriterSidebarProfile(UserManager<AppUser> userManager)
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
