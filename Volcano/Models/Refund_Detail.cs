namespace Volcano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Refund_Detail
    {
        [Key]
        public long Refund_ID { get; set; }

        public long Member_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string Area { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        public double AmountPaid { get; set; }

        [Required]
        [StringLength(50)]
        public string Payment_Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Order_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Refund_Date { get; set; }

        [Required]
        [StringLength(50)]
        public string Order_Time { get; set; }

        [Required]
        [StringLength(50)]
        public string Refund_Time { get; set; }

        public virtual Member Member { get; set; }
    }
}
