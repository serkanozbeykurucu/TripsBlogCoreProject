using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Views.ViewComponents.Default
{
    public class _Categories : ViewComponent
    {
        private CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());
        private BlogManager _blogManager = new BlogManager(new EfBlogDal());
        public IViewComponentResult Invoke()
        {
            var result = _categoryManager.GetList();
            return View(result);
        }
    }
}
