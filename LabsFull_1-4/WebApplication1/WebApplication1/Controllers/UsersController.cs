using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class UsersController : ApiController
    {
        Context db = new Context();



        public IEnumerable<User> GetUsers()
        {
            return db.dbusers;
        }

        

        public bool GetUser(string name, string pass)
        {
            string query = "SELECT * FROM dbo.dbusers WHERE name=@login AND password=@password;";
            string login = name;
            string password = pass;

            try
            {

                using (SqlConnection connectdb = new SqlConnection("Data Source=LAPTOP-21O7MB7R; " +
                                           "Trusted_Connection=yes; " +
                                           "Initial Catalog=case-test;"))
                {
                    SqlCommand command = new SqlCommand(query, connectdb);

                    command.Parameters.Add(new SqlParameter("@login", SqlDbType.VarChar, 45));
                    command.Parameters["@login"].Value = login;
                    command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 45));
                    command.Parameters["@password"].Value = password;

                    if (connectdb.State != ConnectionState.Open)
                    {
                        connectdb.Open();
                    }

                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        return false;
                    }

                    reader.Close();

                    connectdb.Close();
                    connectdb.Dispose();
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }



        /*[HttpPost]
        public void CreateBook([FromBody]User book)
        {
            db.Users.Add(book);
            db.SaveChanges();
        }


        [HttpPut]
        public void EditBook(int id, [FromBody]User book)
        {
            if (id == book.Id)
            {
                db.Entry(book).State = EntityState.Modified;

                db.SaveChanges();
            }
        }



        public void DeleteBook(int id)
        {
            User book = db.Users.Find(id);
            if (book != null)
            {
                db.Users.Remove(book);
                db.SaveChanges();
            }
        }*/


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
