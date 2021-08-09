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
    public class SignupSigninController : Controller
    {
        public NtfSystem db;
        public SignupSigninController(NtfSystem _db)
        {
            this.db = _db;
        }
        public async Task <IActionResult> SignupPage()
        {
            return View();
        }
        public async Task <IActionResult> Register(Employees obj)
        {
            string error = "";
            if (string.IsNullOrEmpty(obj.Name) || string.IsNullOrEmpty(obj.Surname)
                || string.IsNullOrEmpty(obj.Email) || string.IsNullOrEmpty(obj.Password)
                || string.IsNullOrEmpty(obj.Department))
            {
                return Ok("Please fill all fields");
            }
            else if (obj.Password.Length < 6)
            {
                return Ok("Password should contain minimum 6 character");
            }
           
            string email = obj.Email;

            //foreach (Employees elm in db.Employee)
            //{
            //    if (elm.Email == email)
            //    {
            //        return Ok("You have already signed up by this email");
            //    }
            //}
            var q = (from item in db.Employee where item.Email == email select item).Count();
            if (q > 0)
            {
                return Ok("You have already signed up by this email");
            }

           else if (q==0)
            {
                obj.Password = SecurePasswordHasher.Hash(obj.Password);
                db.Employee.Add(obj);
                db.SaveChanges();

                

            }
            else
            {
                TempData["Namak"] = error;
            }

            return Redirect("/SignupSignin/Loginpage");
        }

        public async Task <IActionResult> Loginpage()
        {
            return View();
        }
        public async Task <IActionResult> Login(Employees obj)
        {
            if (string.IsNullOrEmpty(obj.Email) || string.IsNullOrEmpty(obj.Password))
            {
                return Ok("Please fill all fields");
            }
            var user = (from item in db.Employee where 
                        item.Email == obj.Email select item).FirstOrDefault();
            if (user == null)
            {
                return Ok("Please use correct email");

            }
          

            else if (SecurePasswordHasher.Verify(obj.Password, user.Password) == false)
            {
                return Ok("Password is wrong");
            }

           
            else
            {
              
                HttpContext.Session.SetInt32("session", user.Id);
          
                return Redirect("/Userpage/Profile");
            }
            return View();
        }

        

    }
}