using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITBedrijf.Models
{
    public class Register
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Naam Kassa ")]
        public string RegisterName { get; set; }

        [Required]
        [DisplayName("Naam Toestel")]
        public string Device { get; set; }

        [Required]
        [DisplayName("Aankoop Datum")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [DisplayName("Verval Datum")]
        public DateTime ExpiresDate { get; set; }
    }
}