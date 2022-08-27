using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Journey
    {
        public int ID { get; set; }
        public string JourneyName { get; set; }
        public string ThumbNail { get; set; }
        public string ImageUrl { get; set; }
        public string JourneyShortDescription { get; set; }
        public string JourneyDescription { get; set; }
        public bool Status { get; set; }
    }
}
