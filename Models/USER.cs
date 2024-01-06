namespace CampusCom.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            ATTENDINGs = new HashSet<ATTENDING>();
            MEMBERLISTs = new HashSet<MEMBERLIST>();
        }

        public int UserID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public DateTime DoB { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sNum { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Contact { get; set; }

        [Required]
        [StringLength(10)]
        public string userRole { get; set; }

        [Required]
        [StringLength(100)]
        public string Passwd { get; set; }

        [Column(TypeName = "image")]
        public byte[] Img { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTENDING> ATTENDINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBERLIST> MEMBERLISTs { get; set; }
    }
}
