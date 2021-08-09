using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification_System.Models;
using Notification_System.Password;

namespace Notification_System.Controllers
{
    public class UserpageController : Controller
    {
        public NtfSystem db;
        public UserpageController(NtfSystem _db)
        {
            this.db = _db;
        }
        public async Task <IActionResult> Profile()
        {
            int? id = HttpContext.Session.GetInt32("session");
       
            if (id == null)
            {
                TempData["Namak"] = "please login before...";
                return Redirect("/Userpage/Profile");
            }
            var data = (from item in db.Employee where item.Id == id select item).FirstOrDefault();
            ViewBag.currentUser = data;
            return View();
        }
        public async Task <IActionResult> ChangePasswordpage()
        {
            return View();
        }
        public async Task <IActionResult> ChangePassword(PasswordViewModel obj)
        {
            int? id = HttpContext.Session.GetInt32("session");
            var pas = (from item in db.Employee
                       where item.Id == id
                       select item).FirstOrDefault();


            if (SecurePasswordHasher.Verify(obj.OldPassword, pas.Password) == false)
            {
                TempData["Namak"] = "Password sxal e";
                return Redirect("/userpage/ChangePassword");

            }
            else if (SecurePasswordHasher.Verify(obj.OldPassword, pas.Password) == true)
            {
                pas.Password = obj.NewPassword;
                pas.Password = SecurePasswordHasher.Hash(pas.Password);
                db.SaveChanges();
            }

            return Redirect("/userpage/Profile");
        }

        public async Task <IActionResult> Logout()
        {
            HttpContext.Session.Clear();
           
            return Redirect("/SignupSignin/Loginpage");
        }
    }
}