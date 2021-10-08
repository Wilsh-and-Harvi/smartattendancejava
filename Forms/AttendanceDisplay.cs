using MySql.Data.MySqlClient;
using SmartAttendance.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartAttendance.Forms
{
    public partial class AttendanceDisplay : Form
    {
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        MySqlDataReader reader;
        public AttendanceDisplay()
        {
            InitializeComponent();
            getTest();
            getEmployeeName();
        }
        public void getEmployeeName()
        {

            try
            {

                connection = c.openConnection();


                DateTime dp = DateTime.Now;
                DateTime cdate = dp.Date;
                string date = cdate.ToString("yyyy-MM-dd");
                string query = "select username from SA_WH.tblLogin";// where DATE(validdate)>'" + date + "'
                MySqlCommand cmd = new MySqlCommand(query, connection);
                reader = cmd.ExecuteReader();

                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));




                }

                gunaTextBox2.AutoCompleteCustomSource = MyCollection;


                reader.Close();
                connection.Close();


            }
            catch (Exception ex)
            {
                reader.Close();
                connection.Close();
                MessageBox.Show(ex.Message);
            }


        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void getTest()
        {
            try
            {


                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Update(); // In case there is databinding
                listView1.Refresh(); // Redraw items

                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;


                listView1.Columns.Add("Employee Name", 200);
                listView1.Columns.Add("Date", 150);
                listView1.Columns.Add("In TIme", 150);
                listView1.Columns.Add("OutTime", 150);





                int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblEmployeeattaindance";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["employeename"]);
                    arr[1] = (Convert.ToDateTime(dt.Rows[i]["date"])).ToString("dd-MM-yyyy");
                    if (Convert.ToString(dt.Rows[i]["intime"]) == "")
                    {
                        arr[2] = "";
                    }
                    else
                    {



                        arr[2] = (Convert.ToDateTime(dt.Rows[i]["intime"])).ToString("h:mm tt");

                    }
                    if (Convert.ToString(dt.Rows[i]["outtime"]) == "")
                    {
                        arr[3] = "";
                    }
                    else
                    {



                        arr[3] = (Convert.ToDateTime(dt.Rows[i]["outtime"])).ToString("h:mm tt");

                    }








                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch(Exception ee)
            {

            }
   
        }
        private void getTest1()
        {
            try
            {


                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Update(); // In case there is databinding
                listView1.Refresh(); // Redraw items

                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;


                listView1.Columns.Add("Employee Name", 200);
                listView1.Columns.Add("Date", 150);
                listView1.Columns.Add("In TIme", 150);
                listView1.Columns.Add("OutTime", 150);





                int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblEmployeeattaindance where employeename='" + gunaTextBox2.Text+"'";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["employeename"]);
                    arr[1] = (Convert.ToDateTime(dt.Rows[i]["date"])).ToString("dd-MM-yyyy");
                    if (Convert.ToString(dt.Rows[i]["intime"]) == "")
                    {
                        arr[2] = "";
                    }
                    else
                    {



                        arr[2] = (Convert.ToDateTime(dt.Rows[i]["intime"])).ToString("h:mm tt");

                    }
                    if (Convert.ToString(dt.Rows[i]["outtime"]) == "")
                    {
                        arr[3] = "";
                    }
                    else
                    {



                        arr[3] = (Convert.ToDateTime(dt.Rows[i]["outtime"])).ToString("h:mm tt");

                    }








                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ee)
            {

            }

        }
        private void getTest3()
        {
            try
            {


                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Update(); // In case there is databinding
                listView1.Refresh(); // Redraw items

                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;


                listView1.Columns.Add("Employee Name", 200);
                listView1.Columns.Add("Date", 150);
                listView1.Columns.Add("In TIme", 150);
                listView1.Columns.Add("OutTime", 150);





                int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblEmployeeattaindance where date='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["employeename"]);
                    arr[1] = (Convert.ToDateTime(dt.Rows[i]["date"])).ToString("dd-MM-yyyy");
                    if (Convert.ToString(dt.Rows[i]["intime"]) == "")
                    {
                        arr[2] = "";
                    }
                    else
                    {



                        arr[2] = (Convert.ToDateTime(dt.Rows[i]["intime"])).ToString("h:mm tt");

                    }
                    if (Convert.ToString(dt.Rows[i]["outtime"]) == "")
                    {
                        arr[3] = "";
                    }
                    else
                    {



                        arr[3] = (Convert.ToDateTime(dt.Rows[i]["outtime"])).ToString("h:mm tt");

                    }








                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ee)
            {

            }

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            MainMenu f = new MainMenu();
            f.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            getTest1();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            getTest();
        }

        private void gunaTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getTest1();
            }
            }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            getTest3();
        }
    }
}
