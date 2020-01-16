using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace WebApplication1.Models
{
    public class Context : DbContext
    {
        public DbSet<User> dbusers { get; set; }
        public DbSet<INI> INI { get; set; }
        public DbSet<Result> result { get; set; }
    }
}