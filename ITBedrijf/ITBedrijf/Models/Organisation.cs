using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITBedrijf.Models
{
    public class Organisation
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Login")]
        public string Login { get; set; }

        [Required]
        [DisplayName("Wachtwoord")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Naam Database")]
        public string DbName { get; set; }

        [Required]
        [DisplayName("Login Database")]
        public string DbLogin { get; set; }

        [Required]
        [DisplayName("Database Wachtwoord")]
        public string DbPassword { get; set; }

        [Required]
        [DisplayName("Naam Organisatie")]
        public string OrganisationName { get; set; }

        [Required]
        [DisplayName("Adres")]
        public string Address { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Telefoon Nummer")]
        public string Phone { get; set; }
    }
}