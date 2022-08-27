using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;
using TripsBlogCoreProject.Areas.Admin.Models;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class BlogController : Controller
    {
        private BlogManager _blogManager = new BlogManager(new EfBlogDal());
        private CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());
        private readonly UserManager<AppUser> _userManager;

        public BlogController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlogList()
        {
            var result = _blogManager.GetListWithCategory();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> BlogAdd()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.i = result.Id;
            List<SelectListItem> value = (from x in _categoryManager.GetList()
                                          select new SelectListItem
                                          {
                                              Text = x.CategoryName,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.Category = value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BlogAdd(BlogAddViewModel blogAddViewModel)
        {
            string ImageName;
            string ThumbNailImageName;
            if (ModelState.IsValid)
            {
                if (blogAddViewModel.Image != null)
                {
                    var resource = Directory.GetCurrentDirectory(); //kaynağını bulduk.
                    var extension = Path.GetExtension(blogAddViewModel.Image.FileName); //dosya uzantısını aldık.
                    var extension2 = Path.GetExtension(blogAddViewModel.ThumbNailImage.FileName);

                    if (extension != ".mp4" && extension != ".mp3" && extension2 != ".mp4" && extension2 != ".mp3")
                    {
                        ImageName = Guid.NewGuid() + extension; // dosyaya rastgele isim oluşturudu.
                        var savelocation = resource + "/wwwroot/BlogImage/" + ImageName;
                        var stream = new FileStream(savelocation, FileMode.Create);
                        await blogAddViewModel.Image.CopyToAsync(stream);
                        blogAddViewModel.ImageUrl = ImageName;
                        stream.Dispose();

                        ThumbNailImageName = Guid.NewGuid() + extension2;
                        var ThumNailsaveLocation = resource + "/wwwroot/BlogImage/Thumbnail/" + ThumbNailImageName;
                        var stream2 = new FileStream(ThumNailsaveLocation, FileMode.Create);
                        await blogAddViewModel.ThumbNailImage.CopyToAsync(stream2);
                        blogAddViewModel.ThumbNailImageURl = ThumbNailImageName;
                        stream2.Dispose();
                    }
                    else
                    {
                        List<SelectListItem> value = (from x in _categoryManager.GetList()
                                                      select new SelectListItem
                                                      {
                                                          Text = x.CategoryName,
                                                          Value = x.Id.ToString()
                                                      }).ToList();
                        ViewBag.Category = value;
                        ModelState.AddModelError("", "Dosya uzantısı jpg olamaz");
                        return View(blogAddViewModel);
                    }
                    Blog blog = new Blog
                    {
                        BlogName = blogAddViewModel.BlogName,
                        BlogShortDescription = blogAddViewModel.BlogShortDescription,
                        BlogDate = blogAddViewModel.BlogDate,
                        AppUserId = blogAddViewModel.AppUserId,
                        BlogDescription = blogAddViewModel.BlogDescription,
                        CategoryId = blogAddViewModel.CategoryId,
                        ImageUrl = "BlogImage/" + blogAddViewModel.ImageUrl,
                        ThumbNail = "BlogImage/Thumbnail/" + blogAddViewModel.ThumbNailImageURl,
                        Status = true
                    };
                    _blogManager.TAdd(blog);
                    return RedirectToAction("BlogList");
                }
                else
                {
                    List<SelectListItem> value = (from x in _categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.Id.ToString()
                                                  }).ToList();
                    ViewBag.Category = value;
                    return View(blogAddViewModel);
                }

            }
            else
            {
                List<SelectListItem> value = (from x in _categoryManager.GetList()
                                              select new SelectListItem
                                              {
                                                  Text = x.CategoryName,
                                                  Value = x.Id.ToString()
                                              }).ToList();
                ViewBag.Category = value;
                return View(blogAddViewModel);
            }
        }

        [HttpGet]
        public IActionResult BlogUpdate(int id)
        {
            var result = _blogManager.GetById(id);
            List<SelectListItem> categories = (from x in _categoryManager.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.Category = categories;
            return View(result);
        }
        [HttpPost]
        public IActionResult BlogUpdate(Blog blog)
        {
            if (ModelState.IsValid)
            {
                _blogManager.TUpdate(blog);
                return RedirectToAction("BlogList");
            }
            else
            {
                List<SelectListItem> categories = (from x in _categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
                ViewBag.Category = categories;
                return View(blog);
            }
        }

        private List<SelectListItem> CategoryList()
        {
            return (from x in _categoryManager.GetList()
                    select new SelectListItem
                    {
                        Text = x.CategoryName,
                        Value = x.Id.ToString()
                    }).ToList();
        }

        [HttpGet]
        public IActionResult BlogDelete(int id)
        {
            var result = _blogManager.GetWithUser(id);
            Blog blog = new Blog
            {
                BlogName = result.BlogName,
                BlogShortDescription = result.BlogShortDescription,
                BlogDate = result.BlogDate,
                AppUserId = result.AppUserId,
                BlogDescription = result.BlogDescription,
                CategoryId = result.CategoryId,
                ImageUrl = result.ImageUrl,
                ThumbNail = result.ThumbNail,
            };
            return View(blog);
        }
        [HttpPost]
        public IActionResult BlogDelete(Blog blog)
        {
            var resource = Directory.GetCurrentDirectory(); //İlgili dosyanın kök yolunu bulduk.
            var FilePath = resource + "/wwwroot/" + blog.ImageUrl; //dosyanın adresini bulduk
            var FilePathThumbnail = resource + "/wwwroot/" + blog.ThumbNail;
            if (System.IO.File.Exists(FilePath) && System.IO.File.Exists(FilePathThumbnail))
            {
                System.IO.File.Delete(FilePath);
                System.IO.File.Delete(FilePathThumbnail);
            }
            _blogManager.TDelete(blog);
            return RedirectToAction("BlogList");
        }
    }
}
