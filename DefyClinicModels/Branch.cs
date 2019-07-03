using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace DefyClinicModels
{
   public class Branch
    {
        [Key]
        [DisplayName("Branch Code")]
        [Required]
        public string BranchCode { get; set; }
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [DisplayName("Branch Name")]
        [Required]

        public string BranchName { get; set; }
        [DisplayName("Location")]
        [Required]
        public string BranchLocation { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
