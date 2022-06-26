namespace Volcano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartStatu
    {
        [Key]
        public long CartStatus_ID { get; set; }

        [Required]
        [StringLength(250)]
        public string CartStatus { get; set; }
    }
}
