using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification_System.Models;

namespace Notification_System.Controllers
{
    public class HRController : Controller
    {
        public NtfSystem db;
        public HRController(NtfSystem _db)
        {
            this.db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task <IActionResult> CreateNotsToAll()
        {

            int? id = HttpContext.Session.GetInt32("session");
            var data = (from item in db.Employee
                        where item.Id == id
                        select item).FirstOrDefault();
            ViewBag.allemployees = db.Employee;
            ViewBag.currentemployee = data;
            return View();
        }
    }
}