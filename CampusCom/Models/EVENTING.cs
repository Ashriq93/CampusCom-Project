namespace CampusCom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EVENTING")]
    public partial class EVENTING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENTING()
        {
            ATTENDINGs = new HashSet<ATTENDING>();
        }

        [Key]
        public int EventId { get; set; }

        public int ComId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [DisplayName("Event Date")]
        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(300)]
        [DisplayName("Description")]
        public string EventDescr { get; set; }

        [Required]
        [StringLength(300)]
        public string Venue { get; set; }

        [StringLength(10)]
        public string Privacy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTENDING> ATTENDINGs { get; set; }

        public virtual COMMUNITY COMMUNITY { get; set; }
    }
}
