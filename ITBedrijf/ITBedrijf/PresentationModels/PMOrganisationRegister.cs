using ITBedrijf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITBedrijf.PresentationModels
{
    public class PMOrganisationRegister : OrganisationRegister
    {
        public SelectList NewOrganisation { get; set; }
        public SelectList NewRegister { get; set; }
    }
}