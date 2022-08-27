using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager _blogManager = new BlogManager(new EfBlogDal());
        public IActionResult BlogList()
        {
            var result = _blogManager.GetListWithCategory2Filter(x=> x.Status == true);
            return View(result);
        }
        [HttpGet]
        public IActionResult BlogDetails(int id)
        {
            ViewBag.i = id;
            var result = _blogManager.GetWithUser(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult BlogDetails(Blog blog)
        {
            return View();
        }
    }
}
