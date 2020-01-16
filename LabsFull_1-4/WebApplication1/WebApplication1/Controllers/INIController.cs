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
    public class INIController : ApiController
    {
        Context db = new Context();



        /*public IEnumerable<INI> GetINIs()
        {
            return db.INI;
        }*/

        [HttpPut]
        public void EditINI([FromBody]INI ini)
        {
            
            string query = "UPDATE dbo.INI SET formSizeX=" + ini.formSizeX+
                ", formSizeY="+ini.formSizeY+", locationX="+ini.locationX+", locationY="+ini.locationY;
            /*string query1 = "UPDATE dbo.INI SET formSizeY=" + ini.formSizeY;
            string query2 = "UPDATE dbo.INI SET locationX=" + ini.locationX;
            string query3 = "UPDATE dbo.INI SET locationY=" + ini.locationY;*/
            try
            {
                using (SqlConnection connectdb = new SqlConnection("Data Source=LAPTOP-21O7MB7R; " +
                                           "Trusted_Connection=yes; " +
                                           "Initial Catalog=case-test;"))
                {
                    SqlCommand command = new SqlCommand(query, connectdb);

                    if (connectdb.State != ConnectionState.Open)
                    {
                        connectdb.Open();
                    }

                    command.ExecuteNonQuery();

                    /*command = new SqlCommand(query1, connectdb);

                    command.ExecuteNonQuery();
                    command = new SqlCommand(query1, connectdb);

                    command.ExecuteNonQuery();

                    command = new SqlCommand(query1, connectdb);

                    command.ExecuteNonQuery();*/

                    connectdb.Close();
                    connectdb.Dispose();
                }
            }
            catch
            {

            }
        }

        public INI GetINI()
        {
            string query = "SELECT * FROM dbo.INI";
            INI ini = new INI();
            try
            {

                using (SqlConnection connectdb = new SqlConnection("Data Source=LAPTOP-21O7MB7R; " +
                                           "Trusted_Connection=yes; " +
                                           "Initial Catalog=case-test;"))
                {
                    SqlCommand command = new SqlCommand(query, connectdb);


                    if (connectdb.State != ConnectionState.Open)
                    {
                        connectdb.Open();
                    }

                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        ini.formSizeX = (int)reader["formSizeX"];
                        ini.formSizeY = (int)reader["formSizeY"];
                        ini.projectTitle = reader["projectTitle"].ToString();
                        ini.maxSizeX = (int)reader["maxSizeX"];
                        ini.maxSizeY = (int)reader["maxSizeY"];
                        ini.minSizeX = (int)reader["minSizeX"];
                        ini.minSizeY = (int)reader["minSizeY"];
                        ini.locationX = (int)reader["locationX"];
                        ini.locationY = (int)reader["locationY"];
                        ini.opacity = (double)reader["opacity"];
                    }

                    reader.Close();

                    connectdb.Close();
                    connectdb.Dispose();
                }
                return ini;
            }
            catch
            {
                return null;
            }
        }



        /*[HttpPost]
        public void CreateBook([FromBody]User book)
        {
            db.Users.Add(book);
            db.SaveChanges();
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