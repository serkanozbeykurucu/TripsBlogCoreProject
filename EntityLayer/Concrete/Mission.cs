﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Mission
    {
        public int Id { get; set; }
        public string MissionDescription { get; set; }
        public bool Status { get; set; }
    }
}
