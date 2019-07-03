using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicModels
{
   public class Visit
    {
        [Key]
        [Display(Name = "Visit ID")]
        public int V_Id { get; set; }
        [DisplayName("Employee Number")]
        public string EmpNo { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        //  [Required]
        public string P_Name { get; set; }
        [DisplayName("Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        //  [Required]
        public string P_Surname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Appointment Date")]
        public DateTime App_Date { get; set; }
        [Display(Name = "Status")]
        public string App_Status { get; set; }
        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime App_Time { get; set; }

        [Display(Name = "Appointment ID")]
        public int App_Id { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
