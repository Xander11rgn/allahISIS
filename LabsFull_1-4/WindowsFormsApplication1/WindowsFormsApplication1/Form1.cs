using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    

    public partial class Form1 : Form
    {
        string buf = "";
        int formSizeX = 840, formSizeY = 676, maxSizeX = 1000, maxSizeY = 1000, minSizeX = 0, minSizeY = 0, locationX = 0, locationY = 0;
        double opacity = 100.0;
        string projectTitle = "MDI";
        private const string APP_PATH = "http://localhost:53705";
        static HttpClient client = new HttpClient();
        INI ini;
        
        //INIManager manager = new INIManager(Environment.CurrentDirectory+"\\my.ini");

        public Form1()
        {
            InitializeComponent();

            string query = "SELECT * FROM dbo.INI";
            try
            {
                /*formSizeX = Convert.ToInt32(manager.GetPrivateString("formSize", "xSize"));
                formSizeY = Convert.ToInt32(manager.GetPrivateString("formSize", "ySize"));
                projectTitle = manager.GetPrivateString("projectName", "title");
                maxSizeX = Convert.ToInt32(manager.GetPrivateString("MaximumSize", "xSize"));
                maxSizeY = Convert.ToInt32(manager.GetPrivateString("MaximumSize", "ySize"));
                minSizeX = Convert.ToInt32(manager.GetPrivateString("MinimumSize", "xSize"));
                minSizeY = Convert.ToInt32(manager.GetPrivateString("MinimumSize", "ySize"));
                locationX = Convert.ToInt32(manager.GetPrivateString("Location", "X"));
                locationY = Convert.ToInt32(manager.GetPrivateString("Location", "Y"));
                opacity = Convert.ToDouble(manager.GetPrivateString("Opacity", "opacity"));*/
                /*using (SqlConnection connectdb = new SqlConnection("Data Source=LAPTOP-21O7MB7R; " +
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
                        formSizeX = (int)reader["formSizeX"];
                        formSizeY = (int)reader["formSizeY"];
                        projectTitle = reader["projectTitle"].ToString();
                        maxSizeX = (int)reader["maxSizeX"];
                        maxSizeY = (int)reader["maxSizeY"];
                        minSizeX = (int)reader["minSizeX"];
                        minSizeY = (int)reader["minSizeY"];
                        locationX = (int)reader["locationX"];
                        locationY = (int)reader["locationY"];
                        opacity = (double)reader["opacity"];
                    }

                    reader.Close();

                    connectdb.Close();
                    connectdb.Dispose();
                }*/
                var response = client.GetAsync(APP_PATH + "/api/ini/").Result;
                var result = response.Content.ReadAsStringAsync().Result;
                ini = JsonConvert.DeserializeObject<INI>(result);
                formSizeX = ini.formSizeX;
                formSizeY = ini.formSizeY;
                projectTitle = ini.projectTitle;
                maxSizeX = ini.maxSizeX;
                maxSizeY = ini.maxSizeY;
                minSizeX = ini.minSizeX;
                minSizeY = ini.maxSizeY;
                locationX = ini.locationX;
                locationY = ini.locationY;
                opacity = ini.opacity;
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Width = formSizeX;
                Height = formSizeY;
                Text = projectTitle;
                Point loc = Location;
                loc.X = locationX;
                loc.Y = locationY;
                Location = loc;
                Size size = MaximumSize;
                size.Width = maxSizeX;
                size.Height = maxSizeY;
                MaximumSize = size;
                size = MinimumSize;
                size.Width = minSizeX;
                size.Height = minSizeY;
                Opacity = opacity / 100;
            }
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsMdiContainer = true;
            Form2 newForm = new Form2();
            newForm.MdiParent = this;
            newForm.Show();
            

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
                ActiveMdiChild.Close();

        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] form = MdiChildren;
            foreach (Form f in form)
                f.Close();
            this.IsMdiContainer = false;
            this.IsMdiContainer = true;

        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void verticalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void horizontalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Form2 newForm = new Form2();
                newForm.MdiParent = this;
                newForm.Size = new Size(800, 300);
                newForm.Left = 0;
                newForm.Location = new System.Drawing.Point(0, 0);
                newForm.Text = "Текст";
                newForm.Show();

                RichTextBox txtBox = new RichTextBox();
                newForm.Controls.Add(txtBox);
                txtBox.Location = new System.Drawing.Point(5, 10);
                txtBox.Size = new Size(650, 250);
                txtBox.Text = File.ReadAllText(openFileDialog1.FileName,Encoding.GetEncoding(1251));
                txtBox.Multiline = true;
                txtBox.ScrollBars = RichTextBoxScrollBars.Vertical;
                txtBox.Name = "txt";
                txtBox.SelectionChanged += txt_SelectionChanged;
                txtBox.TextChanged += TextChange;
                TrackBar trackBar = new TrackBar();
                newForm.Controls.Add(trackBar);
                trackBar.Location = new System.Drawing.Point(670, 10);
                trackBar.TickFrequency = 1;
                trackBar.Minimum = 1;
                trackBar.Maximum = 20;
                trackBar.Value = 1;
                trackBar.Scroll += DisplayText;
                trackBar.Name = "tb";
                Label lbl = new Label();
                newForm.Controls.Add(lbl);
                lbl.Location = new System.Drawing.Point(710, 60);
                lbl.Text = trackBar.Value.ToString();
                lbl.Name = "lbl";
                Button btn = new Button();
                newForm.Controls.Add(btn);
                btn.Location = new System.Drawing.Point(670, 82);
                btn.Size = new Size(100, 40);
                btn.Text = "Обработка";
                btn.Click += Process;
                btn.Name = "prcbtn";
                Button btn1 = new Button();
                newForm.Controls.Add(btn1);
                btn1.Location = new System.Drawing.Point(670, 127);
                btn1.Size = new Size(100, 40);
                btn1.Text = "Очистка";
                btn1.Click += Clear;
                Button btn2 = new Button();
                newForm.Controls.Add(btn2);
                btn2.Location = new System.Drawing.Point(670, 172);
                btn2.Size = new Size(100, 40);
                btn2.Text = "Выход";
                btn2.Click += Quit;
                Button btn3 = new Button();
                newForm.Controls.Add(btn3);
                btn3.Location = new System.Drawing.Point(670, 217);
                btn3.Size = new Size(100, 40);
                btn3.Text = "Сохранить в XML и БД";
                btn3.Enabled = false;
                btn3.Name = "xmlbtn";
                btn3.Click += SaveXML;
            }
        }

        private void Quit(object sender, EventArgs e)
        {
            this.Hide();
            auth a = new auth();
            a.Show();
    }

        private void SaveXML(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "Words";
            dt.Columns.Add("Length");
            dt.Columns.Add("Frequency");
            ds.Tables.Add(dt);

            string query = "INSERT INTO dbo.results values (@Length,@Frequency,@text)";

            var grid = GetControl(this, "grid") as DataGridView;
            var txt = GetControl(this, "txt") as RichTextBox;

            /*try
            {*/
                foreach (DataGridViewRow r in grid.Rows)
                {
                    if (r.Index == grid.RowCount - 1)
                    {
                        break;
                    }
                    DataRow row = ds.Tables["Words"].NewRow();
                    row["Length"] = r.Cells[0].Value;
                    row["Frequency"] = r.Cells[1].Value;
                    ds.Tables["Words"].Rows.Add(row);

                Result result = new Result();
                result.frequency= (int)r.Cells[1].Value;
                    result.length= (int)r.Cells[0].Value;
                    result.text = txt.Text;

                    client.PostAsJsonAsync(APP_PATH + "/api/result/", result);
                    /*using (SqlConnection connectdb = new SqlConnection("Data Source=LAPTOP-21O7MB7R; " +
                                               "Trusted_Connection=yes; " +
                                               "Initial Catalog=case-test;"))
                    {
                        SqlCommand command = new SqlCommand(query, connectdb);

                        command.Parameters.Add(new SqlParameter("@Length", SqlDbType.Int));
                        command.Parameters["@Length"].Value = r.Cells[0].Value;
                        command.Parameters.Add(new SqlParameter("@Frequency", SqlDbType.Int));
                        command.Parameters["@Frequency"].Value = r.Cells[1].Value;
                        command.Parameters.Add(new SqlParameter("@text", SqlDbType.Text));
                        command.Parameters["@text"].Value = txt.Text;

                        if (connectdb.State != ConnectionState.Open)
                        {
                            connectdb.Open();
                        }

                        command.ExecuteNonQuery();

                        connectdb.Close();
                        connectdb.Dispose();
                    }*/
                }
                ds.WriteXml(Environment.CurrentDirectory + "\\Data.xml");

                MessageBox.Show("Данные в XML файл и БД успешно сохранены.", "Выполнено.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            /*}
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так...","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }*/
        }

        private async void Form1_SizeChanged(object sender, EventArgs e)
        {
            //manager.WritePrivateString("formSize", "xSize", Size.Width.ToString());
            //manager.WritePrivateString("formSize", "ySize", Size.Height.ToString());
            /*string query = "UPDATE dbo.INI SET formSizeX=" + Size.Width;
            string query1 = "UPDATE dbo.INI SET formSizeY=" + Size.Height;
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

                    command = new SqlCommand(query1, connectdb);

                    command.ExecuteNonQuery();

                    connectdb.Close();
                    connectdb.Dispose();
                }
            }
            catch
            {

            }*/
            ini.formSizeX = Size.Width;
            ini.formSizeY = Size.Height;
            await ChangeINI(ini);
        }

        private async Task ChangeINI(INI ini)
        {
            var res = client.PutAsJsonAsync(APP_PATH + "/api/ini/1", ini).Result;
        }

        private void TextChange(object sender, EventArgs e)
        {
            var txt = GetControl(this, "txt") as RichTextBox;
            var btn = GetControl(this, "prcbtn") as Button;
            if (txt.Text == "")
            {
                btn.Enabled = false;
            }
            else
            {
                btn.Enabled = true;
            }
        }

        private async void Form1_LocationChanged(object sender, EventArgs e)
        {
            //manager.WritePrivateString("Location", "X", Location.X.ToString());
            //manager.WritePrivateString("Location", "Y", Location.Y.ToString());
            /*string query = "UPDATE dbo.INI SET locationX=" + Location.X;
            string query1 = "UPDATE dbo.INI SET locationY=" + Location.Y;
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

                    command = new SqlCommand(query1, connectdb);

                    command.ExecuteNonQuery();

                    connectdb.Close();
                    connectdb.Dispose();
                }
            }
            catch
            {

            }*/

            ini.locationX = Location.X;
            ini.locationY = Location.Y;
            await ChangeINI(ini);
        }

        private void Clear(object sender, EventArgs e)
        {
            closeAllToolStripMenuItem_Click(sender, e);
        }

        private Control GetControl(Control control, string name)
        {
            var fetchedControl = control.Controls[name];
            if (fetchedControl == null)
                foreach (Control c in control.Controls)
                {
                    fetchedControl = GetControl(c, name);
                    if (fetchedControl != null)
                        break;
                }
            return fetchedControl;
        }

        

        private void DisplayText(object sender, EventArgs e)
        {
            var lbl=GetControl(this,"lbl");
            var tb= GetControl(this, "tb") as TrackBar;
            lbl.Text = tb.Value.ToString();
        }

        private void Process(object sender, EventArgs e)
        {
            var btn = GetControl(this, "xmlbtn") as Button;
            btn.Enabled = true;
            var form1 = GetControl(this, "tableForm") as Form;
            if (form1!=null)
            {
                form1.Close();
            }
            var form2 = GetControl(this, "diagramForm") as Form;
            if (form2 != null)
            {
                form2.Close();
            }
            Form2 newForm = new Form2();
            newForm.MdiParent = this;
            newForm.Size = new Size(400, 300);
            newForm.Text = "Таблица";
            newForm.Name = "tableForm";
            newForm.Show();
            newForm.Left = 0;
            newForm.Location = new System.Drawing.Point(0, 300);
            Form2 newForm1 = new Form2();
            newForm1.MdiParent = this;
            newForm1.Size = new Size(400, 300);
            newForm1.Text = "Диаграмма";
            newForm1.Name = "diagramForm";
            newForm1.Show();
            newForm1.Left = 0;
            newForm1.Location = new System.Drawing.Point(400, 300);

            DataGridView grid = new DataGridView();
            newForm.Controls.Add(grid);
            grid.Location = new System.Drawing.Point(15, 25);
            grid.Size = new Size(350, 200);
            grid.RowCount = 2;
            grid.ColumnCount = 2;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.Columns[0].HeaderText = "Длина слова";
            grid.Columns[1].HeaderText = "Частота";
            grid.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.Name = "grid";

            var tb = GetControl(this, "tb") as TrackBar;
            int maxWordLength = tb.Value;
            int minWordLength = 1;

            string[] words = (GetControl(this, "txt") as RichTextBox).Text.Split(new Char[] {'\n','\t', '\r', '\\', ',',' ','.',':',';','?','!','"', '«', '»','-' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> Words = new List<string>(words);
            
            for (int i=0;i<Words.Count;i++)
            {
                
                if ((Words[i]=="") || (Words[i]== null) || (int.TryParse(Words[i],out int n)))
                {
                    Words.RemoveAt(i);
                    i--;
                }
            }
            words = Words.ToArray();
            grid.RowCount = maxWordLength+1;

            Chart chart1 = new Chart();
            chart1.Location = new System.Drawing.Point(15, 25);
            chart1.Size = new Size(350, 200);
            newForm1.Controls.Add(chart1);
            chart1.ChartAreas.Add(new ChartArea());
            chart1.Series.Add(new Series("ColumnSeries"));
            chart1.Series["ColumnSeries"].ChartType = SeriesChartType.Pie;
            chart1.Series["ColumnSeries"].Palette = ChartColorPalette.Excel;
            chart1.Titles.Add("Диаграмма");
            chart1.Legends.Add(new Legend("Legend"));


            int[] result = new int[maxWordLength];
            int[] ii = new int[maxWordLength];
            for (int i=minWordLength;i<=maxWordLength;i++)
            {
                result[i-1] = 0;
                for (int k=0;k<words.Length;k++)
                {
                    if (words[k].Length==i)
                    {
                        result[i-1]++;
                    }
                }
                ii[i-1] = i;
                grid.Rows[i-1].Cells[0].Value = ii[i-1];
                grid.Rows[i-1].Cells[1].Value = result[i-1];
                chart1.Series["ColumnSeries"].Points.AddXY(ii[i - 1], result[i - 1]);
            }

            
        }

        public int[] wordsCount(string[] words, int maxWordLength)
        {
            int minWordLength = 1;

            int[] result = new int[maxWordLength];
            for (int i = minWordLength; i <= maxWordLength; i++)
            {
                result[i - 1] = 0;
                for (int k = 0; k < words.Length; k++)
                {
                    if (words[k].Length == i)
                    {
                        result[i - 1]++;
                    }
                }
            }
            return result;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var txt = GetControl(this, "txt") as RichTextBox;
            buf = txt.SelectedText;
            Clipboard.SetText(buf);
            pasteToolStripMenuItem.Enabled = true;
            txt.Select();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var txt = GetControl(this, "txt") as RichTextBox;
                buf = txt.SelectedText;
                Clipboard.SetText(buf);
                pasteToolStripMenuItem.Enabled = true;
                txt.SelectedText = "";
                txt.Select();
            }
            finally
            { }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var txt = GetControl(this, "txt") as RichTextBox;
                int a = txt.SelectionStart;
                buf = Clipboard.GetText();
                txt.SelectedText = buf;
                txt.SelectionStart = a + buf.Length;
                txt.Select();
            }
            catch
            {

            }
        }

        private void txt_SelectionChanged(object sender, EventArgs e)
        {
            var txt = GetControl(this, "txt") as RichTextBox;
            if ((txt.SelectedText != "") && (txt.SelectedText != " "))
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }
            else
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
            }
        }

        public int test()
        {
            int a = 300;
            int b = 250;
            return a - b;
        }


        public string fileLoad(string path)
        {
            string text = File.ReadAllText(path, Encoding.GetEncoding(1251));
            return text;
        }

        public bool GetControlTest(Control control, string name)
        {
            
            var fetchedControl = control.Controls[name];
            if (fetchedControl == null)
                foreach (Control c in control.Controls)
                {
                    fetchedControl = GetControl(c, name);
                    if (fetchedControl != null)
                    {
                        return true;
                    }
                }
            return false;
        }

        public string[] words(string text)
        {
            string[] words = text.Split(new Char[] { '\n', '\t', '\r', '\\', ',', ' ', '.', ':', ';', '?', '!', '"', '«', '»', '-' }, StringSplitOptions.RemoveEmptyEntries);
            return words;
        }

        public string[] wordsFind(string[] words)
        {
            
            List<string> Words = new List<string>(words);

            for (int i = 0; i < Words.Count; i++)
            {

                if ((Words[i] == "") || (Words[i] == null) || (int.TryParse(Words[i], out int n)))
                {
                    Words.RemoveAt(i);
                    i--;
                }
            }
            words = Words.ToArray();

            return words;
        }

    }

    class Result
    {
        public int id { get; set; }
        public int length { get; set; }
        public int frequency { get; set; }
        public string text { get; set; }
    }
}
