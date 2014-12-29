using ITBedrijf.DataAccess;
using ITBedrijf.Models;
using ITBedrijf.PresentationModels;
using ITBedrijfProject.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITBedrijf.Controllers
{
    public class VerenigingController : Controller
    {
        // GET: Vereneging
        public ActionResult Index()
        {
            ViewBag.Organisation = DAOrganisation.GetOrganisations();
            return View();
        }

        [HttpGet]
        public ActionResult NewOrganisation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewOrganisation(string login, string password, string controlePassword, string dbName, string dbLogin, string dbPassword, string dbControlePassword, string organisationName, string address, string email, string phone)
        {
            if (password == controlePassword && dbPassword == dbControlePassword)
            {
                Organisation organisation = new Organisation();
                organisation.Login = login;
                organisation.Password = password;
                organisation.DbName = dbName;
                organisation.DbLogin = dbLogin;
                organisation.DbPassword = dbPassword;
                organisation.OrganisationName = organisationName;
                organisation.Address = address;
                organisation.Email = email;
                organisation.Phone = phone;

                DAOrganisation.InsertOrganisation(organisation);
                return RedirectToAction("Index");
            }
            return RedirectToAction("NewOrganisation");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            PMOrganisation organisation = DAOrganisation.GetOrganisationById(id);
            return View(organisation);
        }

        [HttpPost]
        public ActionResult Details()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            PMOrganisation organisation = DAOrganisation.GetOrganisationById(id);
            return View(organisation);
        }

        [HttpPost]
        public ActionResult Edit(int id, string login, string password, string controlePassword, string dbName, string dbLogin, string dbPassword, string dbControlePassword, string organisationName, string address, string email, string phone)
        {
            if (password == controlePassword && dbPassword == dbControlePassword)
            {
                DAOrganisation.UpdateOrganisation(id, login, password, dbName, dbLogin, dbPassword, organisationName, address, email, phone);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", new { id = id });
        }

        [HttpGet]
        public ActionResult Register(int id)
        {
            ViewBag.Register = DAOrganisationRegister.GetOrganisationRegisterById(id);
            ViewBag.Organisation = DAOrganisation.GetOrganisationById(id);
            return View();
        }

        [HttpGet]
        public ActionResult NewRegister(int id)
        {
            PMOrganisationRegister organisationRegister = new PMOrganisationRegister();
            organisationRegister.NewRegister = new MultiSelectList(DARegister.GetRegisters(), "Id", "RegisterName", "Device");
            organisationRegister.OrganisationID = id;
            ViewBag.Organisation = DAOrganisation.GetOrganisationById(id);
            return View(organisationRegister);
        }

        [HttpPost]
        public ActionResult NewRegister(int organisationID, int registerID, DateTime FromDate, DateTime FromTime, DateTime UntilDate, DateTime UntilTime)
        {
            if (FromDate >= UntilDate) return RedirectToAction("Register", new { id = organisationID });
            OrganisationRegister organisationRegister = new OrganisationRegister();
            organisationRegister.OrganisationID = organisationID;
            organisationRegister.RegisterID = registerID;
            organisationRegister.FromDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, FromTime.Hour, FromTime.Minute, 0);
            organisationRegister.UntilDate = new DateTime(UntilDate.Year, UntilDate.Month, UntilDate.Day, UntilTime.Hour, UntilTime.Minute, 0);

            DAOrganisationRegister.InsertOrganisationRegister(organisationRegister);
            return RedirectToAction("Register", new { id = organisationID });
        }

        [HttpGet]
        public ActionResult EditRegister(int organisationID, int registerID)
        {
            PMOrganisationRegister organisationRegister = new PMOrganisationRegister();
            organisationRegister.NewOrganisation = new MultiSelectList(DAOrganisation.GetOrganisations(), "Id", "Login", "OrganisationName");
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