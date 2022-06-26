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
            Shipping_Detail = new HashSet<Shipping_Detail>();
        }

        [Key]
        public long Member_ID { get; set; }

        [Required(ErrorMessage ="First Name is Required")]
        [StringLength(250)]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [StringLength(250)]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [Remote("CheckMember","Home",ErrorMessage ="This Email is Already Exist",AdditionalFields = "Member_ID")]
        [StringLength(250)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(300,ErrorMessage ="The Password Must be 6 Digits at Least",MinimumLength =6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [StringLength(11, ErrorMessage = "The Phone Number Must be 11 Numbers", MinimumLength = 11)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        [StringLength(250)]
        public string Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipping_Detail> Shipping_Detail { get; set; }
    }
}
