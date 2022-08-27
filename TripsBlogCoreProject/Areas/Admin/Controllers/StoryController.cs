using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class StoryController : Controller
    {
        private OurStoryManager _ourStoryManager = new OurStoryManager(new EfOurStoryDal());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StoryList()
        {
            var result = _ourStoryManager.GetList();
            return View(result);
        }

        [HttpGet]
        public IActionResult StoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StoryAdd(OurStory ourStory)
        {
            if (ModelState.IsValid)
            {
                ourStory.Status = true;
                _ourStoryManager.TAdd(ourStory);
                return RedirectToAction("StoryList");
            }
            return View(ourStory);
        }

        [HttpGet]
        public IActionResult StoryUpdate(int id)
        {
            var result = _ourStoryManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult StoryUpdate(OurStory ourStory)
        {
            if (ModelState.IsValid)
            {
                _ourStoryManager.TUpdate(ourStory);
                return RedirectToAction("StoryList");
            }
            return View(ourStory);
        }


        [HttpGet]
        public IActionResult StoryDelete(int id)
        {
            var result = _ourStoryManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult StoryDelete(OurStory ourStory)
        {
            if (ModelState.IsValid)
            {
                _ourStoryManager.TDelete(ourStory);
                return RedirectToAction("StoryList");
            }
            return View(ourStory);
        }
    }
}
