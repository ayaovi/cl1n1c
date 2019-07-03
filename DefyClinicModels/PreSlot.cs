using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicModels
{
    public class PreSlot
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Time)]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [Display(Name = "Start Time:")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [Display(Name = "End Time:")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Minutes Per Slot:")]
        [Required]
        public int Minutes { get; set; }
        [Display(Name = "Time Slot:")]
        [Required]
        [DataType(DataType.Time)]
        public DateTime A_Slot { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date:")]
        [Required]
        public DateTime Date { get; set; }

        public bool Status { get; set; }

        [Display(Name = "Is Public")]
        public string PublicPostDisplayText
        {
            get { return Status ? "Yes" : "No"; }
        }
    }
}
