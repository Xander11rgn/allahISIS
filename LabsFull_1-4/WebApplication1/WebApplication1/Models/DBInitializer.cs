using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context db)
        {
            db.dbusers.Add(new User { id = 1, age = 20, name = "user1", password = "123456" });
            db.dbusers.Add(new User { id = 2, age = 43, name = "user2", password = "qwerty" });
            db.dbusers.Add(new User { id = 2, age = 43, name = "user3", password = "asdfghjkl" });
            db.INI.Add(new INI { id = 1, formSizeX = 840, formSizeY = 676, maxSizeX = 1000, maxSizeY = 1000, minSizeX = 0, minSizeY = 0, locationX = 0, locationY = 0, opacity = 100.0, projectTitle = "MDI" });
            base.Seed(db);
        }
    }
}