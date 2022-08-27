using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TripsBlogCoreProject.Areas.Writer.Models;

namespace TripsBlogCoreProject.Areas.Writer.Controllers
{
    [Area("Writer")]
    [Route("Writer/[controller]/[action]/{id?}")]
    [Authorize(Roles = "Writer,Admin")]
    public class BlogController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        BlogManager _blogManager = new BlogManager(new EfBlogDal());
        CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());
        public BlogController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> BlogList()
        {
            var UserId = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = _blogManager.GetListWithCategory2Filter(x => x.AppUserId == UserId.Id);
            //var result = _blogManager.GetListWithCategory().Where(x => x.AppUserId == UserId.Id).ToList();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> BlogAdd()
        {
            var result = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.i = result.Id;
            List<SelectListItem> categories = (from x in _categoryManager.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.Category = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BlogAdd(BlogAddViewModel blogAddViewModel)
        {
            string ImageName;
            string ThumbnailImage;
            if (blogAddViewModel.Image != null && blogAddViewModel.ThumnailImage != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(blogAddViewModel.Image.FileName); //dosyanın uzantısını aldık.
                ImageName = Guid.NewGuid() + extension; //dosyaya rastgele isim oluşturduk.
                var saveLocation = resource + "/wwwroot/BlogImage/" + ImageName; //kaydedilecek konum
                var stream = new FileStream(saveLocation, FileMode.Create);
                await blogAddViewModel.Image.CopyToAsync(stream);
                blogAddViewModel.ImageUrl = ImageName;
                stream.Dispose();

                var extensionTwo = Path.GetExtension(blogAddViewModel.ThumnailImage.FileName); //dosyanın uzantısını aldık.
                ThumbnailImage = Guid.NewGuid() + extensionTwo;
                var ThumbnailSaveLocation = resource + "/wwwroot/BlogImage/Thumbnail/" + ThumbnailImage;
                var streamTwo = new FileStream(ThumbnailSaveLocation, FileMode.Create);
                await blogAddViewModel.ThumnailImage.CopyToAsync(streamTwo);
                blogAddViewModel.ThumnailImageUrl = ThumbnailImage;
                streamTwo.Dispose();

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
                ThumbNail = "BlogImage/Thumbnail/" + blogAddViewModel.ThumnailImageUrl,
                Status = false
            };
            _blogManager.TAdd(blog);
            return RedirectToAction("BlogList");

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
            ViewBag.Categories = categories;
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogUpdate(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View(blog);
            }
            _blogManager.TUpdate(blog);
            return RedirectToAction("BlogList");
        }
        [HttpGet]
        public IActionResult BlogDelete(int id)
        {
            var result = _blogManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult BlogDelete(Blog blog)
        {
            var resource = Directory.GetCurrentDirectory();
            var filePath = resource + "/wwwroot/" + blog.ImageUrl;
            var ThumnailFilePath = resource + "/wwwroot/" + blog.ThumbNail;
            if (System.IO.File.Exists(filePath) && System.IO.File.Exists(ThumnailFilePath))
            {
                System.IO.File.Delete(filePath);
                System.IO.File.Delete(ThumnailFilePath);
            }
            _blogManager.TDelete(blog);
            return RedirectToAction("BlogList");
        }

    }

}
