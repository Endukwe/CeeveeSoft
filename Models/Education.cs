using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CeeveeSoftWebProj.Models
{
    public enum ClassOfDegree
    {
        OND, HND, BSC, MSC, PHD
    }
    public class Education
    {

        public int EducationId { get; set; }
        public int PortfolioId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:MM-yyyy}")]
        [Display(Name ="Date of Graduation")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name ="School")]
        public string School { get; set; } //School or Institution attended

        [Required]
        [Display(Name = "Course Of Study")]
        public string Course { get; set; }

        [Required]
        [Display(Name ="Class of Degree")]
        public ClassOfDegree ClassOfDegree { get; set; }
        public Portfolio portfolio { get; set; }
    }
}

