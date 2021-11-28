using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CeeveeSoftWebProj.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [Display(Name ="Upload Picture")]
        public byte[] ProfilePicture { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }


        [Required(ErrorMessage = "Please enter your Proffesional summary")]
        [Display(Name = "Professional Summary")]
        [StringLength(400)]
        public string ProfessionalSummary { get; set; }
        

        [Required(ErrorMessage = "Please enter your Address")]
        public string Address { get; set; }
        public string LinkdinHandle  { get; set; }
        public string Proffesion { get; set; }
        public byte[] CV { get; set; }
        public string IdentityId { get; set; }

        public ICollection<Education> Educations { get; set; }
        public ICollection<Experience> Experiences { get; set; }
        public ICollection<Certification> Certifications { get; set; }
        public ICollection<Skill> Skills { get; set; }



    }
    
}
