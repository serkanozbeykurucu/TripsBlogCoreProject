using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TripsBlogCoreProject.Views.ViewComponents.Default
{
    public class _PopularBlog : ViewComponent
    {
        BlogManager _blogManager = new BlogManager(new EfBlogDal());
        public IViewComponentResult Invoke()
        {
            var result = _blogManager.GetList().OrderByDescending(x=> x.Id).Take(3).ToList(); 
            // Take(listelenecek veri adeti) komutu SQL deki Top komutuna benzerdir.  
            return View(result);
        }
    }
}
