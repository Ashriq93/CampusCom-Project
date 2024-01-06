using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusCom.Models
{
    public class CommunityDetailsViewModel
    {
        public COMMUNITY Community { get; set; }
        public List<POST> Posts { get; set; }
    }
}