using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Views.ViewComponents.Default
{
    public class _Mission : ViewComponent
    {
        MissionManager _missionManager = new MissionManager(new EfMissionDal());
        public IViewComponentResult Invoke()
        {
            var result = _missionManager.GetList();
            if (result != null)
            {
                foreach (var item in result)
                {
                    ViewBag.MissionDescription = item.MissionDescription;
                }
            }
            else
            {
                ViewBag.MissionDescription = "";
            }
            return View();
        }
    }
}
