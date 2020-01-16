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
    public class ResultController : ApiController
    {
        Context db = new Context();


        [HttpPost]
        public void CreateResult([FromBody]Result result)
        {
            string query = "INSERT INTO dbo.results values (@Length,@Frequency,@text)";

            using (SqlConnection connectdb = new SqlConnection("Data Source=LAPTOP-21O7MB7R; " +
                                               "Trusted_Connection=yes; " +
                                               "Initial Catalog=case-test;"))
            {
                SqlCommand command = new SqlCommand(query, connectdb);

                command.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int));
                command.Parameters["@Length"].Value = result.length;
                command.Parameters.Add(new SqlParameter("@Frequency", SqlDbType.Int));
                command.Parameters["@Frequency"].Value = result.frequency;
                command.Parameters.Add(new SqlParameter("@text", SqlDbType.Text));
                command.Parameters["@text"].Value = result.text;

                if (connectdb.State != ConnectionState.Open)
                {
                    connectdb.Open();
                }

                command.ExecuteNonQuery();

                connectdb.Close();
                connectdb.Dispose();
            }
        }


        /*



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