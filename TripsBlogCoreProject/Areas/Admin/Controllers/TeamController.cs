using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using TripsBlogCoreProject.Areas.Admin.Models;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class TeamController : Controller
    {
        private TeamManager _teamManager = new TeamManager(new EfTeamDal());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TeamList()
        {
            var result = _teamManager.GetList();
            return View(result);
        } 
        public IActionResult TeamDetails(int id)
        {
            var result = _teamManager.GetById(id);
            ViewBag.ImageUrl = result.ImageUrl;
            return View(result);
        }
        [HttpGet]
        public IActionResult TeamAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeamAdd(TeamAddViewModel t)
        {
            string ImageName;
            if (ModelState.IsValid)
            {
                var resoruce = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(t.Image.FileName);
                ImageName = Guid.NewGuid() + extension;
                var saveLocation = resoruce + "/wwwroot/TeamImage/" + ImageName;
                var stream = new FileStream(saveLocation, FileMode.Create);
                await t.Image.CopyToAsync(stream);
                t.ImageUrl = ImageName;
                stream.Dispose();
                Team team = new Team
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Degree = t.Degree,
                    Resume = t.Resume,
                    FacebookUrl = t.FacebookUrl,
                    InstagramUrl = t.InstagramUrl,
                    TwitterUrl = t.TwitterUrl,
                    ImageUrl = "TeamImage/" + t.ImageUrl
                };
                _teamManager.TAdd(team);
                return RedirectToAction("TeamList");
            }
            return View(t);
        }

        [HttpGet]
        public IActionResult TeamUpdate(int id)
        {
            var result = _teamManager.GetById(id);
            ViewBag.ImageUrl = result.ImageUrl;
            return View(result);
        }
        [HttpPost]
        public IActionResult TeamUpdate(Team team)
        {
            if (ModelState.IsValid)
            {
                _teamManager.TUpdate(team);
                return RedirectToAction("TeamList");
            }
            return View(team);
        }
        [HttpGet]
        public IActionResult TeamDelete(int id)
        {
            var result = _teamManager.GetById(id);
            ViewBag.ImageUrl = result.ImageUrl;
            return View(result);
        }
        [HttpPost]
        public IActionResult TeamDelete(Team team)
        {
            if (ModelState.IsValid)
            {
                _teamManager.TDelete(team);
                return RedirectToAction("TeamList");
            }
            return View(team);
        }
    }
}
