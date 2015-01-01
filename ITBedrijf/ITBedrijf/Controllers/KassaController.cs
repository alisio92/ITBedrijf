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
        public ActionResult Index(int? offset)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (!offset.HasValue) offset = 0;
            int aantal = 10;
            List<Register> register = DARegister.GetRegisters(offset.Value, aantal);
            ViewBag.Register = register;
            ViewBag.Numbers = LimitList.GetNumberList(LimitList.GetAantal(DARegister.GetRegistersCount(), aantal));
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
        public ActionResult NewRegister(PMRegister register, DateTime? PurchaseTime, DateTime? ExpireTime)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");

            if (ModelState.IsValid)
            {
                if (register.PurchaseDate >= register.ExpireDate) return View(register);
                register.PurchaseDate = new DateTime(register.PurchaseDate.Year, register.PurchaseDate.Month, register.PurchaseDate.Day, PurchaseTime.Value.Hour, PurchaseTime.Value.Minute, 0);
                register.ExpireDate = new DateTime(register.ExpireDate.Year, register.ExpireDate.Month, register.ExpireDate.Day, ExpireTime.Value.Hour, ExpireTime.Value.Minute, 0);
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
        public ActionResult Log(int id, int? offset)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (!offset.HasValue) offset = 0;
            int aantal = 10;
            List<Errorlog> log = DALog.GetLogsById(id, offset.Value, aantal);
            ViewBag.Log = log;
            ViewBag.Numbers = LimitList.GetNumberList(LimitList.GetAantal(DALog.GetErrorlogsCount(id), aantal));
            ViewBag.Id = id;
            return View();
        }

        [HttpGet]
        [OutputCache(CacheProfile = "CacheDefault")]
        public ActionResult Logs(int? offset)
        {
            if (User.Identity.Name == "") return RedirectToAction("ErrorLogin", "Home");
            if (!offset.HasValue) offset = 0;
            int aantal = 10;
            List<Errorlog> log = DALog.GetErrorlog(offset.Value, aantal);
            ViewBag.Log = log;
            ViewBag.Numbers = LimitList.GetNumberList(LimitList.GetAantal(DALog.GetErrorlogsCount(), aantal));
            return View();
        }
    }
}