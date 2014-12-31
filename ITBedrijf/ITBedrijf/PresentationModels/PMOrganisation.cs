using ITBedrijf.DataAccess;
using ITBedrijf.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITBedrijf.PresentationModels
{
    public class PMOrganisation : Organisation
    {
        [Required]
        [DisplayName("Controle Wachtwoord")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en het controle wachtwoord zijn niet gelijk.")]
        public string ControlePassword { get; set; }

        [Required]
        [DisplayName("Database Controle Wachtwoord")]
        [Compare("DbPassword", ErrorMessage = "Het wachtwoord en het controle wachtwoord zijn niet gelijk.")]
        public string DbControlePassword { get; set; }

        public string Error { get; set; }
    }
}