using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusCom.Models
{
    public class CommunityMemberViewModel
    {
        public COMMUNITY Community { get; set; }
        public List<MEMBERLIST> MemberList { get; set; }
    }
}