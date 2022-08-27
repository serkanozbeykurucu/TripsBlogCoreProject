using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Views.ViewComponents.Default
{
    public class _OurStory : ViewComponent
    {
        OurStoryManager _ourStoryManager = new OurStoryManager(new EfOurStoryDal());
        public IViewComponentResult Invoke()
        {
            var result = _ourStoryManager.GetList();
            foreach (var item in result)
            {
                ViewBag.StoryDescription = item.StoryDescription;
            }
            return View();
        }
    }
}
