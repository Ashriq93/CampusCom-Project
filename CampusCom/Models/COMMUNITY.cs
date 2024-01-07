namespace CampusCom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMMUNITY")]
    public partial class COMMUNITY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMMUNITY()
        {
            EVENTINGs = new HashSet<EVENTING>();
            MEMBERLISTs = new HashSet<MEMBERLIST>();
            POSTs = new HashSet<POST>();
        }

        [Key]
        public int ComId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(60)]
        [DisplayName("Name")]
        public string ComName { get; set; }

        [StringLength(255)]
        [DisplayName("Description")]
        public string ComDescr { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(50)]
        public string Privacy { get; set; }

        [DisplayName("Created")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Status")]
        public string ComStatus { get; set; }

        [Column(TypeName = "image")]
        [DisplayName("Image")]
        public byte[] ComImg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EVENTING> EVENTINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBERLIST> MEMBERLISTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POST> POSTs { get; set; }
    }
}
