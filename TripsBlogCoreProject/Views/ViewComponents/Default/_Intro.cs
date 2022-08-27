using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Views.ViewComponents.Default
{
    public class _Intro : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
