using ITBedrijf.DataAccess;
using ITBedrijf.Extra;
using ITBedrijf.Hubs;
using ITBedrijf.Models;
using ITBedrijf.PresentationModels;
using ITBedrijfProject.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ITBedrijf.Controllers
{
    public class VerenigingController : Controller
    {
        // GET: Vereneging
        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult Index(int? offset)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (!offset.HasValue) offset = 0;
            int aantal = 10;
            List<Organisation> organisation = DAOrganisation.GetOrganisations(offset.Value, aantal);
            ViewBag.Organisation = organisation;
            ViewBag.Numbers = LimitList.GetNumberList(LimitList.GetAantal(DAOrganisation.GetOrganisationsCount(), aantal));
            return View();

        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult NewOrganisation()
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult NewOrganisation(PMOrganisation organisation)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");

            if (ModelState.IsValid)
            {
                if (DAOrganisation.InsertOrganisation(organisation) < 0)
                {
                    organisation.Error = "Login Naam Database bestaat al.";
                    return View(organisation);
                }
                var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Counters>();
                int amountOrganisations = DAOrganisation.GetOrganisationsCount();
                hub.Clients.All.NumberOffOrganisations(amountOrganisations);
                return RedirectToAction("Index");
            }
            return View(organisation);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult Details(int id)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            PMOrganisation organisation = DAOrganisation.GetOrganisationById(id);
            return View(organisation);
        }

        [HttpPost]
        public ActionResult Details()
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            return View("Index");
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult Edit(int id)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            PMOrganisation organisation = DAOrganisation.GetOrganisationById(id);
            return View(organisation);
        }

        [HttpPost]
        public ActionResult Edit(PMOrganisation organisation, int id)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (ModelState.IsValid)
            {
                DAOrganisation.UpdateOrganisation(id, organisation);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { organisation = organisation, id = id });
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult Register(int id, int? offset)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (!offset.HasValue) offset = 0;
            int aantal = 10;
            List<OrganisationRegister> register = DAOrganisationRegister.GetOrganisationRegisterById(id, offset.Value, aantal);
            ViewBag.Register = register;
            ViewBag.Organisation = DAOrganisation.GetOrganisationById(id);
            ViewBag.Numbers = LimitList.GetNumberList(LimitList.GetAantal(DAOrganisationRegister.GetOrganisationRegisterCount(id), aantal));
            return View();
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult NewRegister(int id)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            PMOrganisationRegister organisationRegister = new PMOrganisationRegister();
            organisationRegister.NewRegister = new MultiSelectList(DARegister.GetRegisters(0, DARegister.GetRegistersCount()), "Id", "RegisterName", "Device");
            organisationRegister.OrganisationID = id;
            ViewBag.Organisation = DAOrganisation.GetOrganisationById(id);
            return View(organisationRegister);
        }

        [HttpPost]
        public ActionResult NewRegister(int organisationID, int registerID, PMOrganisationRegister organisationRegister, DateTime? FromTime, DateTime? UntilTime)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            organisationRegister.OrganisationID = organisationID;
            if (ModelState.IsValid)
            {
                if (organisationRegister.FromDate >= organisationRegister.UntilDate) return RedirectToAction("Register", new { id = organisationID });
                organisationRegister.FromDate = new DateTime(organisationRegister.FromDate.Year, organisationRegister.FromDate.Month, organisationRegister.FromDate.Day, FromTime.Value.Hour, FromTime.Value.Minute, 0);
                organisationRegister.UntilDate = new DateTime(organisationRegister.UntilDate.Year, organisationRegister.UntilDate.Month, organisationRegister.UntilDate.Day, UntilTime.Value.Hour, UntilTime.Value.Minute, 0);

                DAOrganisationRegister.InsertOrganisationRegister(organisationRegister);
                return RedirectToAction("Register", new { id = organisationID });
            }
            return RedirectToAction("NewRegister", new { id = organisationID });
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult EditRegister(int organisationID, int registerID)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            PMOrganisationRegister organisationRegister = new PMOrganisationRegister();
            organisationRegister.NewOrganisation = new MultiSelectList(DAOrganisation.GetOrganisations(0, DAOrganisation.GetOrganisationsCount()), "Id", "Login", "OrganisationName");
            OrganisationRegister or = DAOrganisationRegister.GetOrganisationRegisterByIds(organisationID, registerID);
            organisationRegister.Device = or.Device;
            organisationRegister.FromDate = or.FromDate;
            organisationRegister.Login = or.Login;
            organisationRegister.OrganisationID = or.OrganisationID;
            organisationRegister.OrganisationName = or.OrganisationName;
            organisationRegister.RegisterID = or.RegisterID;
            organisationRegister.RegisterName = or.RegisterName;
            organisationRegister.UntilDate = or.UntilDate;

            ViewBag.Organisation = DAOrganisation.GetOrganisationById(organisationID);
            ViewBag.oldId = organisationID;
            return View(organisationRegister);
        }

        [HttpPost]
        public ActionResult EditRegister(int oldOrganisationID, int organisationID, DateTime fromDate, DateTime untilDate, int registerID)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            OrganisationRegister organisationRegister = DAOrganisationRegister.GetOrganisationRegisterByIds(organisationID, registerID);
            organisationRegister.FromDate = fromDate;
            organisationRegister.OrganisationID = organisationID;
            organisationRegister.RegisterID = registerID;
            organisationRegister.UntilDate = untilDate;
            DAOrganisationRegister.UpdateOrganisationRegister(oldOrganisationID, organisationRegister);
            return RedirectToAction("Register", new { id = oldOrganisationID });
        }
    }
}