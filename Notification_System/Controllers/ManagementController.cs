using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification_System.Models;

namespace Notification_System.Controllers
{
    public class ManagementController : Controller
    {
        public NtfSystem db;
        public ManagementController(NtfSystem _db)
        {
            this.db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public async Task <IActionResult> NotesFromMng()
        {

            int? id = HttpContext.Session.GetInt32("session");
            var data = (from item in db.Employee
                        where item.Id == id
                        select item).FirstOrDefault();
            ViewBag.allemployees = db.Employee;
            ViewBag.currentemployee = data;
            return View();
        }

        public async Task <IActionResult> Dashboard()
        {
            int? id = HttpContext.Session.GetInt32("session");
            var data = (from item in db.Employee
                        where item.Id == id
                        select item).FirstOrDefault();
            ViewBag.currentemployee = data;
            ViewBag.allemployees = db.Employee;
            return View();
        }
        public async Task <IActionResult> ChangeToDeVops(int id)
        {
            var data = (from elm in db.Employee where elm.Id == id select elm).FirstOrDefault();
        
            if (data.Department == "DeVops")
            {
                
                return Ok("Employee is already at this department");
            }
            else
            {
                data.Department = "DeVops";
                db.SaveChanges();
            }
            return Redirect("/Management/Dashboard");
        }
        public async Task <IActionResult> ChangeToHr(int id)
        {
            var data = (from elm in db.Employee where elm.Id == id select elm).FirstOrDefault();

            if (data.Department == "Hr")
            {
                
                return Ok("Employee is already at this department");
            }
            else
            {
                data.Department = "Hr";
                db.SaveChanges();
            }
            return Redirect("/Management/Dashboard");
        }
      
        public async Task <IActionResult> ChangeToDevelopment(int id)
        {
            var data = (from elm in db.Employee where elm.Id == id select elm).FirstOrDefault();

            if (data.Department == "Development")
            {
                return Ok("Employee is already at this department");
                //TempData["Namak"] = "Employee is already at this department";
            }
            else
            {
                data.Department = "Development";
                db.SaveChanges();
            }
            return Redirect("/Management/Dashboard");
        }
    }
}