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
        public int OrganisationID { get; set; }
        public int Index { get; set; }

        [DisplayName("Naam Vereniging")]
        public string OrganisationName { get; set; }

        [DisplayName("Login")]
        public string Login { get; set; }

        [Required]
        public int RegisterID { get; set; }

        [DisplayName("Naam Kassa")]
        public string RegisterName { get; set; }

        [DisplayName("Naam Toestel")]
        public string Device { get; set; }

        [Required]
        [DisplayName("Van")]
        public DateTime FromDate { get; set; }

        [Required]
        [DisplayName("Tot")]
        public DateTime UntilDate { get; set; }
    }
}