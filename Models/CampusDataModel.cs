using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CampusCom.Models
{
    public partial class CampusDataModel : DbContext
    {
        public CampusDataModel()
            : base("name=CampusDataModel")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<ATTENDING> ATTENDINGs { get; set; }
        public virtual DbSet<COMMUNITY> COMMUNITies { get; set; }
        public virtual DbSet<EVENTING> EVENTINGs { get; set; }
        public virtual DbSet<MEMBERLIST> MEMBERLISTs { get; set; }
        public virtual DbSet<POST> POSTs { get; set; }
        public virtual DbSet<USER> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
