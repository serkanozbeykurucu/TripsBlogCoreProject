using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace TripsBlogCoreProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]")]
    [Authorize(Roles = "Admin,Writer")]
    public class DashboardController : Controller
    {
        BlogManager _blogManager = new BlogManager(new EfBlogDal());
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public DashboardController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            Context c = new Context();
            try
            {
                var result2 = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.Name = result2.FirstName + " " + result2.LastName;
                ViewBag.WriterImage = result2.ImageUrl;
                var result = _blogManager.GetListWithCategory().Where(x => x.AppUserId == result2.Id).ToList();
                if (c.Blogs.Count() != null)
                {
                    ViewBag.BlogCount = c.Blogs.Count();
                }
                else
                {
                    ViewBag.BlogCount = "";
                }
                
                string apikey = "279ab030962429cbb21b0eaf212de199";
                string city = "Adana";
                string apiurl = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&mode=xml&units=metric&appid=" + apikey;
                XDocument document = XDocument.Load(apiurl);
                ViewBag.Degree = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
                ViewBag.City = city;
                return View(result);
            }
            catch
            {
                return View();
            }

        }
    }
}
