using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CampusCom.Data
{
    public class CampusComContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CampusComContext() : base("name=CampusComContext")
        {
        }

        public System.Data.Entity.DbSet<CampusCom.Models.COMMUNITY> COMMUNITies { get; set; }

        public System.Data.Entity.DbSet<CampusCom.Models.POST> POSTs { get; set; }

        public System.Data.Entity.DbSet<CampusCom.Models.EVENTING> EVENTINGs { get; set; }

        public System.Data.Entity.DbSet<CampusCom.Models.USER> USERs { get; set; }

        public System.Data.Entity.DbSet<CampusCom.Models.MEMBERLIST> MEMBERLISTs { get; set; }
    }
}
