using ITBedrijf.DataAccess;
using ITBedrijf.Extra;
using ITBedrijf.Hubs;
using ITBedrijf.Models;
using ITBedrijf.PresentationModels;
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
        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult Index(int? offset, int? aantal)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (!offset.HasValue) offset = 0;
            if (!aantal.HasValue) aantal = 10;
            List<Register> register = DARegister.GetRegisters(offset.Value, aantal.Value);
            List<int> numbers = LimitList.GetNumberList(LimitList.GetAantal(DARegister.GetRegistersCount(), aantal.Value));
            ViewBag.Register = register;
            ViewBag.Numbers = numbers;
            ViewBag.Length = numbers.Count();
            return View();
        }
        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult NewRegister()
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult NewRegister(PMRegister register, DateTime? purchaseTime, DateTime? expireTime)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (ModelState.IsValid)
            {
                if (register.PurchaseDate >= register.ExpireDate) return View(register);
                if (purchaseTime.HasValue) register.PurchaseDate = new DateTime(register.PurchaseDate.Year, register.PurchaseDate.Month, register.PurchaseDate.Day, purchaseTime.Value.Hour, purchaseTime.Value.Minute, 0);
                if (expireTime.HasValue) register.ExpireDate = new DateTime(register.ExpireDate.Year, register.ExpireDate.Month, register.ExpireDate.Day, expireTime.Value.Hour, expireTime.Value.Minute, 0);
                DARegister.InsertRegister(register);
                var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Counters>();
                int amountRegisters = DARegister.GetRegistersCount();
                hub.Clients.All.NumberOffRegisters(amountRegisters);
                return RedirectToAction("Index");
            }
            return View(register);
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult Log(int id, int? offset, int? aantal)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (!offset.HasValue) offset = 0;
            if (!aantal.HasValue) aantal = 10;
            List<Errorlog> log = DALog.GetLogsById(id, offset.Value, aantal.Value);
            List<int> numbers = LimitList.GetNumberList(LimitList.GetAantal(DALog.GetErrorlogsCount(id), aantal.Value));
            ViewBag.Log = log;
            ViewBag.Numbers = numbers;
            ViewBag.Length = numbers.Count();
            ViewBag.Id = id;
            return View();
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult Logs(int? offset, int? aantal)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (!offset.HasValue) offset = 0;
            if (!aantal.HasValue) aantal = 10;
            List<Errorlog> log = DALog.GetErrorlog(offset.Value, aantal.Value);
            List<int> numbers = LimitList.GetNumberList(LimitList.GetAantal(DALog.GetErrorlogsCount(), aantal.Value));
            ViewBag.Log = log;
            ViewBag.Length = numbers.Count();
            ViewBag.Numbers = numbers;
            return View();
        }
    }
}