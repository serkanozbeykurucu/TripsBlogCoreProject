using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentUser { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool Status { get; set; }
    }
}
