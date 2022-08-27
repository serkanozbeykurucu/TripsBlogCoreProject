using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using TripsBlogCoreProject.Areas.Admin.Models;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class CommentController : Controller
    {
        private CommentManager _commentManager = new CommentManager(new EfCommentDal());
        private BlogManager _blogManager = new BlogManager(new EfBlogDal());

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CommentList()
        {
            var result = _commentManager.GetListWithBlog();
            return View(result);
        }
        [HttpGet]
        public IActionResult CommentAdd()
        {
            List<SelectListItem> blog = (from x in _blogManager.GetList()
                                         select new SelectListItem
                                         {
                                             Text = x.BlogName,
                                             Value = x.Id.ToString()
                                         }).ToList();
            ViewBag.Blogs = blog;
            return View();
        }
        [HttpPost]
        public IActionResult CommentAdd(CommentAddViewModel commentAddViewModel)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment
                {
                    CommentUser = commentAddViewModel.CommentUser,
                    CommentContent = commentAddViewModel.CommentContent,
                    CommentDate = commentAddViewModel.CommentDate,
                    Status = true,
                    BlogId = commentAddViewModel.BlogId
                };
                _commentManager.TAdd(comment);
                return RedirectToAction("CommentList");
            }
            return View(commentAddViewModel);
        }

        [HttpGet]
        public IActionResult CommentUpdate(int id)
        {
            var result = _commentManager.GetById(id);
            List<SelectListItem> blog = (from x in _blogManager.GetList()
                                         select new SelectListItem
                                         {
                                             Text = x.BlogName,
                                             Value = x.Id.ToString()
                                         }).ToList();
            ViewBag.Blogs = blog;
            return View(result);
        }
        [HttpPost]
        public IActionResult CommentUpdate(Comment comment)
        {
            _commentManager.TUpdate(comment);
            return RedirectToAction("CommentList");
        }

        [HttpGet]
        public IActionResult CommentDelete(int id)
        {
            var result = _commentManager.GetById(id);
            List<SelectListItem> blog = (from x in _blogManager.GetList()
                                         select new SelectListItem
                                         {
                                             Text = x.BlogName,
                                             Value = x.Id.ToString()
                                         }).ToList();
            ViewBag.Blogs = blog;
            return View(result);
        }
        [HttpPost]
        public IActionResult CommentDelete(Comment comment)
        {
            _commentManager.TDelete(comment);
            return RedirectToAction("CommentList");
        }
    }
}
