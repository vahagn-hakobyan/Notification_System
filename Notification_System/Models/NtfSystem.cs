using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification_System.Models
{
    public class NtfSystem:DbContext
    {
        public DbSet<Employees> Employee { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public NtfSystem(DbContextOptions x) : base(x) { }
    }
}
