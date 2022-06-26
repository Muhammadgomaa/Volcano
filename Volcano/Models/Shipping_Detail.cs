namespace Volcano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shipping_Detail
    {
        [Key]
        public long Shipping_ID { get; set; }

        public long Member_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(250)]
        public string Area { get; set; }

        [Required]
        [StringLength(250)]
        public string City { get; set; }

        [Required]
        [StringLength(250)]
        public string Country { get; set; }

        public double AmountPaid { get; set; }

        [Required]
        [StringLength(250)]
        public string Payment_Type { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public virtual Member Member { get; set; }
    }
}
