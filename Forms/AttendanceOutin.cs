using MySql.Data.MySqlClient;
using SmartAttendance.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartAttendance.Forms
{
    public partial class AttendanceOutin : Form
    {
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        string employeename = "";
        MySqlDataReader reader;
        public AttendanceOutin()
        {
            InitializeComponent();
            
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
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

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


                listView1.Columns.Add("Out Range", 200);
                listView1.Columns.Add("In Range", 200);
                listView1.Columns.Add("Total Hours", 150);
              





                int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblLoginLogoutLog where employeeid='" + employeename + "' AND date='"+dateTimePicker1.Value.ToString("yyy-MM-dd")+"'";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["logouttime"]);
                    arr[1] = Convert.ToString(dt.Rows[i]["logintime"]);
                  if(arr[0]!="" && arr[1]!="")
                    {
                        TimeSpan duration = DateTime.Parse(arr[1]).Subtract(DateTime.Parse(arr[0]));
                        arr[2] = duration.ToString();
                    }
                  else
                    {
                        arr[2] = "";
                    }








                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ee)
            {

            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            getTest1();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            ListAttendance f = new ListAttendance();
            f.Show();
            this.Hide();
        }

        private void gunaTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                employeename = gunaTextBox2.Text;
                getTest1();
            }
        }
    }
}
