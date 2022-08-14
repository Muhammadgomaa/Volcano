namespace Volcano.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Refund_Detail = new HashSet<Refund_Detail>();
            Shipping_Detail = new HashSet<Shipping_Detail>();
        }

        [Key]
        public long Member_ID { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Please Enter Valid Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please Enter Valid Name")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Please Enter Valid Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please Enter Valid Name")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [Remote("CheckMember", "Home", ErrorMessage = "This Email is Already Exist", AdditionalFields = "Member_ID")]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$", ErrorMessage = "Please Enter Valid Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(50, ErrorMessage = "The Password Must be 6 Digits at Least", MinimumLength = 6)]
        public string Password { get; set; }

        [NotMapped]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The Password Must be Match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Phone is Required")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please Enter Valid Phone Number")]
        [StringLength(11, ErrorMessage = "The Password Must be 11 Digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        [StringLength(50)]
        public string Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Refund_Detail> Refund_Detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipping_Detail> Shipping_Detail { get; set; }
    }
}
