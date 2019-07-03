using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefyClinicModels
{
  public class Booking
  {
    [Key]
    [Display(Name = "Appointment ID")]
    public int AppId { get; set; }
    [Display(Name = "Pantient Details")]
    public string PatientDetails { get; set; }
    [Display(Name = "Status")]
    public string AppStatus { get; set; }

    [DataType(DataType.Date)]
    // [Required]

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

    [Display(Name = "Appointment Date")]
    public DateTime AppDate { get; set; }

    [Display(Name = "Department")]
    public string Dep { get; set; }
    [Display(Name = "Appointment Time")]

    //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public string AppTime { get; set; }

    [Display(Name = "Reason(s) for Appointment")]
    public string Reason { get; set; }
    [Display(Name = "Is Free?")]
    public bool IsFree { get; set; }

    public int TimeId { get; set; }
    public virtual PreSlot A_Slot { get; set; }

    public virtual Staff Staff { get; set; }
    public virtual ICollection<Visit> Visits { get; set; }
  }
}
