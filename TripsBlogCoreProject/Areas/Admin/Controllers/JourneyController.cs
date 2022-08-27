using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using TripsBlogCoreProject.Areas.Admin.Models;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class JourneyController : Controller
    {
        private JourneyManager _journeyManager = new JourneyManager(new EfJourneyDal());
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JourneyList()
        {
            var result = _journeyManager.GetList();
            return View(result);
        }
        [HttpGet]
        public IActionResult JourneyAdd()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JourneyAdd(JourneyAddViewModel journeyAddViewModel)
        {
            string ImageName;
            string ThumbnailImage;
            if (ModelState.IsValid)
            {
                if (journeyAddViewModel.Image != null && journeyAddViewModel.ThumbNail != null)
                {
                    var resoruce = Directory.GetCurrentDirectory();
                    //kök dizini bulduk.
                    var extension = Path.GetExtension(journeyAddViewModel.Image.FileName);
                    // dosya yolu uzantısını aldık
                    ImageName = Guid.NewGuid() + extension;
                    // dosyaya rastgele isim oluşturp, dosya yoluyla birleşirdik.
                    var saveLocation = resoruce + "/wwwroot/JourneyImage/" + ImageName;
                    //kayıt konumu
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    //kopyalama
                    await journeyAddViewModel.Image.CopyToAsync(stream);
                    //kaydetme
                    journeyAddViewModel.ImageUrl = ImageName;
                    stream.Dispose();

                    var extensionThumnail = Path.GetExtension(journeyAddViewModel.ThumbNail.FileName);
                    //jpg yada png uzantısını aldık.
                    ThumbnailImage = Guid.NewGuid() + extensionThumnail;
                    var saveLocationThumbail = resoruce + "/wwwroot/JourneyImage/Thumbnail/" + ThumbnailImage;
                    var streamThumnail = new FileStream(saveLocationThumbail, FileMode.Create);
                    await journeyAddViewModel.ThumbNail.CopyToAsync(streamThumnail);
                    journeyAddViewModel.ThumbNailUrl = ThumbnailImage;
                    streamThumnail.Dispose();

                    Journey j = new Journey
                    {
                        JourneyName = journeyAddViewModel.JourneyName,
                        JourneyShortDescription = journeyAddViewModel.JourneyShortDescription,
                        JourneyDescription = journeyAddViewModel.JourneyDescription,
                        Status = true,
                        ImageUrl = "JourneyImage/" + journeyAddViewModel.ImageUrl,
                        ThumbNail = "JourneyImage/Thumbnail/" + journeyAddViewModel.ThumbNailUrl
                    };
                    _journeyManager.TAdd(j);
                    return RedirectToAction("JourneyList");
                }
                else
                {
                    return View(journeyAddViewModel);
                }

            }
            return View(journeyAddViewModel);
        }

        [HttpGet]
        public IActionResult JourneyUpdate(int id)
        {
            var result = _journeyManager.GetById(id);
            JourneyUpdateViewModel journeyUpdateViewModel = new JourneyUpdateViewModel
            {
                ID = result.ID,
                JourneyName = result.JourneyName,
                JourneyShortDescription = result.JourneyShortDescription,
                JourneyDescription = result.JourneyDescription,
                OldImageUrl = result.ImageUrl,
                OldThumbNailUrl = result.ThumbNail,
                Status = result.Status,
                ImageUrl = "",
                ThumbNailUrl = ""
            };
            return View(journeyUpdateViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JourneyUpdate(JourneyUpdateViewModel journeyUpdateViewModel)
        {
            string ImageName, ThumnailName;
            if (ModelState.IsValid)
            {
                Journey j = new Journey
                {
                    ID = journeyUpdateViewModel.ID,
                    JourneyName = journeyUpdateViewModel.JourneyName,
                    JourneyShortDescription = journeyUpdateViewModel.JourneyShortDescription,
                    JourneyDescription = journeyUpdateViewModel.JourneyDescription,
                    Status = journeyUpdateViewModel.Status
                };
                if (journeyUpdateViewModel.Image != null && journeyUpdateViewModel.ThumbNail != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(journeyUpdateViewModel.Image.FileName);
                    var extensionThumnail = Path.GetExtension(journeyUpdateViewModel.ThumbNail.FileName);
                    ImageName = Guid.NewGuid() + extension;
                    ThumnailName = Guid.NewGuid() + extensionThumnail;
                    var saveLocationImage = resource + "/wwwroot/JourneyImage/" + ImageName;
                    var saveLocationThumbnail = resource + "/wwwroot/JourneyImage/Thumbnail/" + ThumnailName;
                    var stream = new FileStream(saveLocationImage, FileMode.Create);
                    var streamThumbnail = new FileStream(saveLocationThumbnail, FileMode.Create);
                    await journeyUpdateViewModel.Image.CopyToAsync(stream);
                    await journeyUpdateViewModel.ThumbNail.CopyToAsync(streamThumbnail);
                    journeyUpdateViewModel.ImageUrl = ImageName;
                    journeyUpdateViewModel.ThumbNailUrl = ThumnailName;
                    stream.Dispose();
                    streamThumbnail.Dispose();

                    string OldImage = resource + "/wwwroot/" + journeyUpdateViewModel.OldImageUrl;
                    string OldThumnnail = resource + "/wwwroot/" + journeyUpdateViewModel.OldThumbNailUrl;
                    if (System.IO.File.Exists(OldImage) && System.IO.File.Exists(OldThumnnail))
                    {
                        System.IO.File.Delete(OldImage);
                        System.IO.File.Delete(OldThumnnail);
                    }
                    j.ImageUrl = "JourneyImage/" + journeyUpdateViewModel.ImageUrl;
                    j.ThumbNail = "JourneyImage/Thumbnail/" + journeyUpdateViewModel.ThumbNailUrl;
                    _journeyManager.TUpdate(j);
                }
                else
                {
                    journeyUpdateViewModel.ImageUrl = "";
                    journeyUpdateViewModel.ThumbNailUrl = "";
                    j.ImageUrl = "JourneyImage/" + journeyUpdateViewModel.OldImageUrl;
                    j.ThumbNail = "JourneyImage/Thumbnail/" + journeyUpdateViewModel.OldThumbNailUrl;
                    _journeyManager.TUpdate(j);
                }

                return RedirectToAction("JourneyList");
            }
            return View(journeyUpdateViewModel);
        }

        [HttpGet]
        public IActionResult JourneyDelete(int id)
        {
            var result = _journeyManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult JourneyDelete(Journey j)
        {
            if (ModelState.IsValid)
            {
                var resource = Directory.GetCurrentDirectory();
                var ImageLocation = resource + "/wwwroot/JourneyImage/" + j.ImageUrl;
                var ThumnailImageLocation = resource + "/wwwroot/JourneyImage/Thumbnail/" + j.ThumbNail;
                if (System.IO.File.Exists(ImageLocation) && System.IO.File.Exists(ThumnailImageLocation))
                {
                    System.IO.File.Delete(ImageLocation);
                    System.IO.File.Delete(ThumnailImageLocation);
                }
                _journeyManager.TDelete(j);
                return RedirectToAction("JourneyList");
            }
            return View(j);
        }

    }
}
