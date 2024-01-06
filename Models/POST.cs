namespace CampusCom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("POST")]
    public partial class POST
    {
        public int PostId { get; set; }

        public int ComId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        [DisplayName("Message")]
        public string pMessage { get; set; }

        [DisplayName("Post Date")]
        public DateTime PostDate { get; set; }

        public virtual COMMUNITY COMMUNITY { get; set; }
    }
}
