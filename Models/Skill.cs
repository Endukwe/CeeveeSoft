using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CeeveeSoftWebProj.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public int PortfolioId { get; set; }

        [Required]
        [Display(Name = "Skills")]
        public string SkillContent { get; set; }
        public string Skilllevelpercent { get { return SkillLevel + "%"; } }

        public string SkillLevel { get; set; }
        public Portfolio portfolio { get; set; }
    }

}
