namespace WebApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        public int id { get; set; }

        public int account { get; set; }

        public int session { get; set; }

        public int seat { get; set; }

        public int? price { get; set; }

        public virtual Account Account1 { get; set; }

        public virtual Seat Seat1 { get; set; }

        public virtual Session Session1 { get; set; }
    }
}
