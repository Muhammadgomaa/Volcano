namespace Volcano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice_Detail
    {
        [Key]
        public long Invo_ID { get; set; }

        public long Shipping_ID { get; set; }

        public long Prod_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Quantity { get; set; }

        public double Price { get; set; }

        public virtual Product_Detail Product_Detail { get; set; }

        public virtual Shipping_Detail Shipping_Detail { get; set; }
    }
}
