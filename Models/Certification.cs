    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CeeveeSoftWebProj.Models
{
    public class Certification
    {
        public int CertificationId { get; set; }
        public int PortfolioId { get; set; }
        public string CertificationNo { get; set; }

        public string NameOfCertification { get; set; }
        public string IssuingBody { get; set; }
        public DateTime IsuueDate { get; set; }

        public Portfolio Portfolio { get; set; }
    }
}
