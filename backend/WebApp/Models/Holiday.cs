namespace WebApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Holiday
    {
        [Key]
        [StringLength(20)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public int discount { get; set; }
    }
}
