namespace WebApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Movy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movy()
        {
            Sessions = new HashSet<Session>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string description { get; set; }

        [Required]
        [StringLength(20)]
        public string age_rest { get; set; }

        [Required]
        [StringLength(15)]
        public string genre { get; set; }

        [StringLength(50)]
        public string duration { get; set; }

        [Column(TypeName = "date")]
        public DateTime start_of_rental { get; set; }

        [Column(TypeName = "date")]
        public DateTime end_of_rental { get; set; }

        [StringLength(500)]
        public string img { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
