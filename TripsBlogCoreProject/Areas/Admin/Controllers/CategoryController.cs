using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class CategoryController : Controller
    {
        private CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());
        private BlogManager _blogManager = new BlogManager(new EfBlogDal());

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryList()
        {
            var result = _categoryManager.GetList();
            return View(result);
        }
        [HttpGet]  
        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category category)
        {
            category.Status = true;
            _categoryManager.TAdd(category);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult CategoryUpdate(int id)
        {
            var result = _categoryManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult CategoryUpdate(Category category)
        {
            _categoryManager.TUpdate(category);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult CategoryDelete(int id)
        {
            var result = _categoryManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult CategoryDelete(Category category)
        {
            _categoryManager.TDelete(category);
            return RedirectToAction("CategoryList");
        }
    }
}
