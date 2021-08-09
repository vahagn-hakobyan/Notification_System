using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification_System.Models;

namespace Notification_System.Controllers
{
  
    public class NotificationController : Controller
    {
        public NtfSystem db;
        public NotificationController(NtfSystem _db)
        {
            this.db = _db;
        }
        public async Task <IActionResult> Notificationspage()
        {
            return View();
        }
        public async Task <IActionResult> CreateNotification()
        {
            
            int? id = HttpContext.Session.GetInt32("session");
            var data = (from item in db.Employee 
                        where item.Id == id select item).FirstOrDefault();
            ViewBag.allemployees = db.Employee;
            ViewBag.currentemployee = data;
            if (data.Department == "Hr")
            {
                return Redirect("/HR/CreateNotsToAll");
            }
            else if (data.Department == "DevOps")
            {
                return Redirect("/DevOps/NotsToMngDvp");
            }
            else if (data.Department == "Development")
            {
                return Redirect("/Developer/NotsToManagement");
            }
            else if (data.Department == "Management")
            {
                return Redirect("/Management/NotesFromMng");
            }
            else
            {
                TempData["Namak"] = "error";
            }
   
            return Redirect("/Userpage/Profile");
        }

   
        public async Task <IActionResult> SendNots([FromBody] NotificationViewModel id)
        {
            int? me = HttpContext.Session.GetInt32("session");
            var data = (from item in db.Employee where item.Id == (int) me select item).FirstOrDefault();
            Notification_System.Models.Notification n = new Notification_System.Models.Notification();
            n.FromDepartment = data.Department;
            n.Department = id.department;
            n.Content = id.text;
            db.Notifications.Add(n);
            db.SaveChanges();
            return Json(n.Content);

        }
     


        public async Task <ActionResult<IEnumerable<Notification>>> MyNotifications()
        {
            int? id = HttpContext.Session.GetInt32("session");
            var data = (from item in db.Employee where item.Id == id select item).FirstOrDefault();
            var nots = (from elm in db.Notifications
                        where
                        elm.Department == "ToAll" ||
                        elm.Department == data.Department
                        select elm).ToArray();
            ViewBag.allnotifications = nots;
            ViewBag.currentuser = data;
            return View();
        }
        

    }
}