namespace Volcano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Product_Detail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product_Detail()
        {
            Invoice_Detail = new HashSet<Invoice_Detail>();
        }

        [Key]
        public long Prod_ID { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [Remote("CheckProduct", "Admin", ErrorMessage = "This Product is Already Exist", AdditionalFields = "Prod_ID")]
        [StringLength(50, ErrorMessage = "Please Enter Valid Product Name", MinimumLength = 3)]
        public string Prod_Name { get; set; }


        public long Cat_ID { get; set; }

        [StringLength(50)]
        public string Prod_Image { get; set; }

        [Required(ErrorMessage = "Product Price is Required")]
        [RegularExpression(@"^-?[0-9][0-9,\.]+$", ErrorMessage = "Please Enter Valid Price")]
        [Range(1, 1000000000)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Product Status is Required")]
        [StringLength(50)]
        public string Status { get; set; }
        [Required(ErrorMessage = "Product Name is Required")]

        public virtual Category_Detail Category_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice_Detail> Invoice_Detail { get; set; }
    }
}
