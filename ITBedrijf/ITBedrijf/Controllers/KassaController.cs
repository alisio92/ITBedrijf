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
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            ViewBag.Register = DARegister.GetRegisters();
            return View();
        }
        [HttpGet]
        public ActionResult NewRegister()
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult NewRegister(string registerName, string device, DateTime purchaseDate, DateTime purchaseTime, DateTime expireDate, DateTime expireTime)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (purchaseDate >= expireDate) return RedirectToAction("Index");
            Register register = new Register();
            register.RegisterName = registerName;
            register.Device = device;
            register.PurchaseDate = new DateTime(purchaseDate.Year, purchaseDate.Month, purchaseDate.Day, purchaseTime.Hour, purchaseTime.Minute, 0);
            register.ExpireDate = new DateTime(expireDate.Year, expireDate.Month, expireDate.Day, expireTime.Hour, expireTime.Minute, 0);

            DARegister.InsertRegister(register);
            return RedirectToAction("Index");
        }
    }
}