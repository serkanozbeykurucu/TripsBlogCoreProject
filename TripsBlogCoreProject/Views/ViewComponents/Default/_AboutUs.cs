using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace TripsBlogCoreProject.Views.ViewComponents.Default
{
    public class _AboutUs : ViewComponent
    {
        private MissionManager _missionManager = new MissionManager(new EfMissionDal());
        public IViewComponentResult Invoke()
        {
            var result = _missionManager.GetList();
            return View(result);
        }
    }
}
