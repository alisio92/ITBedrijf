using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITBedrijf.Models
{
    public class OrganisationRegister
    {
        [Required]
        [DisplayName("Vereniging")]
        public int OrganisationID { get; set; }

        [Required]
        [DisplayName("Kassa")]
        public int RegisterID { get; set; }

        [Required]
        [DisplayName("Van")]
        public DateTime FromDate { get; set; }

        [Required]
        [DisplayName("Tot")]
        public DateTime UntilDate { get; set; }
    }
}