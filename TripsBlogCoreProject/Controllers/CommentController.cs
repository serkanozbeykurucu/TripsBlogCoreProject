using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TripsBlogCoreProject.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        CommentManager _commentManager = new CommentManager(new EfCommentDal());
        public IActionResult Index()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult CommentAdd()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CommentAdd(Comment comment)
        {
            comment.CommentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            comment.Status = true;
            //comment.BlogId = blogid;
            _commentManager.TAdd(comment);
            return RedirectToAction("BlogList", "Blog");
        }
    }
}
