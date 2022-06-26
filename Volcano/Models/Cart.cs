namespace Volcano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        [Key]
        public long Cart_ID { get; set; }

        public long Prod_ID { get; set; }

        public long Member_ID { get; set; }

        public long CartStatus_ID { get; set; }

        public virtual Product_Detail Product_Detail { get; set; }
    }
}
