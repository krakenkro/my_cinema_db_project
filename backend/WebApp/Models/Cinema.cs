namespace WebApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cinema")]
    public partial class Cinema
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cinema()
        {
            Halls = new HashSet<Hall>();
        }

        [Key]
        [StringLength(20)]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        public string city { get; set; }

        [Required]
        [StringLength(30)]
        public string address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hall> Halls { get; set; }
    }
}
