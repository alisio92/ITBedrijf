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
    }
}