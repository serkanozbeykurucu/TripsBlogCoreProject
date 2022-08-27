using EntityLayer.Concrete;

namespace TripsBlogCoreProject.Areas.Admin.Models
{
    public class CommentAddViewModel
    {
        public string CommentUser { get; set; }
        public int BlogId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
    }
}
