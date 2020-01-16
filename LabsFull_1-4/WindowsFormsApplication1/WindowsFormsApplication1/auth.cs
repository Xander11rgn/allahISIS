using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class auth : Form
    {
        public auth()
        {
            InitializeComponent();
        }
        public Form1 f = new Form1();
        private const string APP_PATH = "http://localhost:53705";
        static HttpClient client = new HttpClient();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var response = client.GetAsync(APP_PATH + "/api/users/GetUser?name=" + textBox1.Text + "&pass=" + textBox2.Text).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                if (result != "true")
                {
                    MessageBox.Show("Пользователя с таким логином и паролем не существует! Повторите ввод.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    this.Hide();

                    f.Show();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
