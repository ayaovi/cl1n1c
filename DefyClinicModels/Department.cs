using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefyClinicModels
{
  public  class Department
    {
        [Key]
        [DisplayName("Department ID")]
        public string D_ID { get; set; }
        [DisplayName("Department Name")]
        [Required]
        public string D_Name { get; set; }
        [Required]
        [DisplayName("Department Manager")]
        [DisplayFormat(DataFormatString = "MR NS Smith")]
        public string Manager { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
