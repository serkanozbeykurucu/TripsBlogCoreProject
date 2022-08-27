using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class OurStory
    {
        public int Id { get; set; }
        public string StoryName { get; set; }
        public string StoryDescription { get; set; }
        public bool Status { get; set; }
    }
}
