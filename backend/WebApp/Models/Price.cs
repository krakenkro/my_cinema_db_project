namespace WebApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Price")]
    public partial class Price
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string category { get; set; }

        [Column("price")]
        public int? price1 { get; set; }

        public virtual Category Category1 { get; set; }
    }
}
