using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notification_System.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Department { get; set; }
        public string Content { get; set; }
       // public int User1Id { get; set; }
        public string FromDepartment { get; set; }
        public Employees Employe1 { get; set; }
        public Employees Employe2 { get; set; }
    }
}
