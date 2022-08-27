using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Views.ViewComponents.Default
{
    public class _Journey : ViewComponent
    {
        JourneyManager _journeyManager = new JourneyManager(new EfJourneyDal());
        public IViewComponentResult Invoke()
        {
            var result = _journeyManager.GetList().OrderByDescending(x=> x.ID).Take(3).ToList();
            return View(result);
        }
    }
}
