using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        BlogManager _blogManager = new BlogManager(new EfBlogDal());
        CommentManager _commentManager = new CommentManager(new EfCommentDal());
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var BlocCount = _blogManager.GetList().Count();
            ViewBag.BlogCount = BlocCount;
            ViewBag.CommentCount = _commentManager.GetList().Count();
            ViewBag.UserCount = _userManager.Users.Count();
            var result = _blogManager.GetListByFilter(x => x.BlogDate == DateTime.Today && x.Status == false).
                OrderByDescending(x=>x.Id).
                ToList();
            string apikey = "your api key";
            string city = "Adana";
            string apiurl = "https://api.openweathermap.org/data/2.5/weather?q="+ city+ "&mode=xml&units=metric&appid=" + apikey;
            XDocument document = XDocument.Load(apiurl);
            //ViewBag.City = document.Descendants("city").ElementAt(0).Attribute("name").Value;
            ViewBag.City = city;
            ViewBag.Degree = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View(result);
        }
    }
}
