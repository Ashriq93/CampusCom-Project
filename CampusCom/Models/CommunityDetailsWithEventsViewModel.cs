using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusCom.Models
{
    public class CommunityDetailsWithEventsViewModel
    {
        public COMMUNITY Community { get; set; }
        public List<EVENTING> Events { get; set; }
    }
}