using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string BlogName { get; set; } //1
        public string BlogDescription { get; set; } //1
        public string BlogShortDescription { get; set; } //1
        public DateTime BlogDate { get; set; } //1
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string ThumbNail { get; set; } 
        public string ImageUrl { get; set; } 
        //public string BlogCategory { get; set; } //1
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool Status { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
