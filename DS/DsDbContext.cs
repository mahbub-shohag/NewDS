using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DS.Models;
namespace DS
{
    public class DsDbContext : DbContext
    {
        public DbSet<Service> services { get; set; }
        public DbSet<Menu> menus { get; set; }
        public DbSet<Contact> contacts { get; set; }

        public System.Data.Entity.DbSet<DS.Models.Message> Messages { get; set; }
    }
}