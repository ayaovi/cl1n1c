
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicModels
{
  public  class Booking
    {
        [Key]
        [Display(Name = "Appointment ID")]
        public int App_Id { get; set; }
        [Display(Name = "Pantient Details")]
        public string P_Details { get; set; }
        [Display(Name = "Status")]
        public string App_Status { get; set; }

        [DataType(DataType.Date)]
        // [Required]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        [Display(Name = "Appointment Date")]
        public DateTime App_Date { get; set; }

        [Display(Name = "Department")]
        public string Dep { get; set; }
        [Display(Name = "Appointment Time")]

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public string App_Time { get; set; }

        [Display(Name = "Reason(s) for Appointment")]
        public string Reason { get; set; }
        [Display(Name = "Is Free?")]
        public bool isFree { get; set; }

        public int TimeId { get; set; }
        public virtual PreSlot A_Slot { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
