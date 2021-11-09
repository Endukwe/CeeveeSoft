using CeeveeSoftWebProj.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CeeveeSoftWebProj.ViewModels
{
    public class PortfolioViewModel
    {

        public int Id { get; set; }
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        public IFormFile Upload { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        
        public string PhoneNumber { get; set; }
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }
        [Required(ErrorMessage = "Please enter your Proffesional summary")]
        [Display(Name = "Professional Summary")]
        public string ProfessionalSummary { get; set; }
       

        [Required(ErrorMessage = "Please enter your Address")]
        public string Address { get; set; }
        public string LinkdinHandle { get; set; }
        public string Proffesion { get; set; }
        public IFormFile CVfile { get; set; }

        public string IdentityId { get; set; }
    }
}

