using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripsBlogCoreProject.Email;
using TripsBlogCoreProject.Models;

namespace TripsBlogCoreProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        JourneyManager _journeyManager = new JourneyManager(new EfJourneyDal());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Trips()
        {
            return View();
        }
        public IActionResult TripsDetail(int id)
        {
            var result = _journeyManager.GetById(id);
            return View(result);
        }
        public IActionResult Blog()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                string content = "Hi! I am " + contactModel.FirstName + " " + contactModel.LastName + "<br>" + contactModel.Content + "<br>" 
                    + "Ad-Soyad: " + contactModel.FirstName + " " + contactModel.LastName 
                    +"<br>" +"E-Mail: " + contactModel.EMail + "<br>" + "Bilgilerinize.";
                EmailHelper emailHelper = new EmailHelper();
                bool emailResponse = emailHelper.SendEmail(contactModel.EMail, contactModel.FirstName + " " + contactModel.LastName, content);
                if (emailResponse)
                {
                    TempData["Message"] = "Mesajınız iletilmiştir. En kısa zamanda size geri dönüş sağlanacaktır.";
                    return RedirectToAction("Contact");
                }
                else
                {

                }
            }
            ModelState.AddModelError("", "Lütfen ilgili alanları doldurunuz!");
            return View(contactModel);
        }
    }
}
