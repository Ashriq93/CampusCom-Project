namespace CampusCom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ATTENDING")]
    public partial class ATTENDING
    {
        [Key]
        public int AttendId { get; set; }

        public int EventId { get; set; }

        public int UserId { get; set; }

        public virtual EVENTING EVENTING { get; set; }

        public virtual USER USER { get; set; }
    }
}
