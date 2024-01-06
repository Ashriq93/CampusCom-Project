namespace CampusCom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MEMBERLIST")]
    public partial class MEMBERLIST
    {
        [Key]
        public int MemId { get; set; }

        public int UserId { get; set; }

        public int ComId { get; set; }

        public bool isOwner { get; set; }

        public virtual COMMUNITY COMMUNITY { get; set; }

        public virtual USER USER { get; set; }
    }
}
