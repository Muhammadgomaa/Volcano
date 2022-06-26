namespace Volcano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Category_Detail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category_Detail()
        {
            Product_Detail = new HashSet<Product_Detail>();
        }

        [Key]
        public long Cat_ID { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        [Remote("CheckCategory", "Admin", ErrorMessage = "This Category is Already Exist", AdditionalFields = "Cat_ID")]
        [StringLength(250, ErrorMessage = "Invalid Category Name", MinimumLength = 3)]
        public string Cat_Name { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Detail> Product_Detail { get; set; }
    }
}
