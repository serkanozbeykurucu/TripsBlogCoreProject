using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class MissionController : Controller
    {
        private MissionManager _missionManager = new MissionManager(new EfMissionDal());
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult MissionList()
        {
            var result = _missionManager.GetList();
            return View(result);
        }

        [HttpGet]
        public IActionResult MissionAdd()
        {
            return View();
        } 
        [HttpPost]
        public IActionResult MissionAdd(Mission mission)
        {
            if (ModelState.IsValid)
            {
                _missionManager.TAdd(mission);
                return RedirectToAction("MissionList");
            }            
            return View(mission);
        }


        [HttpGet]
        public IActionResult MissionUpdate(int id)
        {
            var result = _missionManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult MissionUpdate(Mission mission)
        {
            if (ModelState.IsValid)
            {
                _missionManager.TUpdate(mission);
                return RedirectToAction("MissionList");
            }
            return View(mission);
        }

        [HttpGet]
        public IActionResult MissionDelete(int id)
        {
            var result = _missionManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult MissionDelete(Mission mission)
        {
            if (ModelState.IsValid)
            {
                _missionManager.TDelete(mission);
                return RedirectToAction("MissionList");
            }
            return View(mission);
        }
    }
}
