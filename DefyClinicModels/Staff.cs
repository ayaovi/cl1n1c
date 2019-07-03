using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicModels
{
  public  class Staff
    {
        [Key]
        [DisplayName("Employee Number")]
        public string EmpNo { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        public string EmpName { get; set; }
        [DisplayName("Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        public string EmpSurname { get; set; }
        [DisplayName("ID/Passport")]
        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Invailde ID/Passport Number")]
        // [Range(13,13, ErrorMessage = "Invailde ID/Passport Number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "UPRN must be numeric")]

        public string ID_Pass { get; set; }
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [DisplayName("Mobile")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Required]
        public string EmpTel { get; set; }
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmpEmail { get; set; }
        [DisplayName("Address")]
        [Required]
        public string EmpAddress { get; set; }
        [DisplayName("Department")]
        public string D_ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        [DisplayName("Position")]
        public string P_ID { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        [DisplayName("Role")]
        public string Role { get; set; }
        [Display(Name = "Picture")]
        //[DataType(DataType.Upload)]
        public byte[] Picture { get; set; }
        [DisplayName("Branch")]
        public string BranchCode { get; set; }
        public virtual Branch Branch { get; set; }
        //  public ICollection<Branch> Branches { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
