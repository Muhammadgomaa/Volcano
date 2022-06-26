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
            Carts = new HashSet<Cart>();
        }

        [Key]
        public long Prod_ID { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [Remote("CheckProduct", "Admin", ErrorMessage = "This Product is Already Exist", AdditionalFields = "Prod_ID")]
        [StringLength(250, ErrorMessage = "Invalid Product Name", MinimumLength = 3)]
        public string Prod_Name { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        public long Cat_ID { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product Image is Required")]
        public string Prod_Image { get; set; }

        public bool? IsFeatured { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1, 90000, ErrorMessage = "Invalid Quantity")]
        public long Quantity { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1, 200000, ErrorMessage = "Invalid Price")]
        public double Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        public virtual Category_Detail Category_Detail { get; set; }
    }
}
