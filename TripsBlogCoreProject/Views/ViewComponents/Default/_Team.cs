using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Views.ViewComponents.Default
{
    public class _Team : ViewComponent
    {
        TeamManager _teamManager = new TeamManager(new EfTeamDal());
        public IViewComponentResult Invoke()
        {
            var result = _teamManager.GetList();
            return View(result);
        }
    }
}
