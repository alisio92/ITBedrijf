using ITBedrijf.DataAccess;
using ITBedrijf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITBedrijf.Controllers
{
    public class KassaController : Controller
    {
        // GET: Kassa
        public ActionResult Index()
        {
            ViewBag.Register = DARegister.GetRegisters();
            return View();
        }
        [HttpGet]
        public ActionResult NewRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewRegister(string registerName, string device, DateTime purchaseDate, DateTime purchaseTime, DateTime expiresDate, DateTime expiresTime)
        {
            if (purchaseDate >= expiresDate) return RedirectToAction("NewRegister");
            Register register = new Register();
            register.RegisterName = registerName;
            register.Device = device;
            register.PurchaseDate = new DateTime(purchaseDate.Year, purchaseDate.Month, purchaseDate.Day, purchaseTime.Hour, purchaseTime.Minute, 0);
            register.ExpiresDate = new DateTime(expiresDate.Year, expiresDate.Month, expiresDate.Day, expiresTime.Hour, expiresTime.Minute, 0);

            DARegister.InsertRegister(register);
            return RedirectToAction("NewRegister");
        }
    }
}