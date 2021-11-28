using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CeeveeSoftWebProj.Models
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public int PortfolioId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/yyyy}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Organization/Company")]
        public string Organization { get; set; }


        [Display(Name = "Period of Experience")]
        public string Period
        {
            get
            {
                if(EndDate != null)
                {
                    return StartDate.ToString("MM/yyyy") + " - " + EndDate.Value.ToString("MM/yyyy");
                }

                return StartDate.ToString("MM/yyyy") + " - Till Date";


            }
        }
        [Display(Name = "Role Description")]
        public string RoleFunction { get; set; }

        [Required]
        public string Role { get; set; }
        public Portfolio portfolio { get; set; }
    }
    
}
