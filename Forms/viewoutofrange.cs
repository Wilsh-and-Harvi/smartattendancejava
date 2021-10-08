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
    public partial class viewoutofrange : Form
    {
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        MySqlDataReader reader;
        public viewoutofrange()
        {
            InitializeComponent();
            getEmployeeName();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            getTest1();
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


                listView1.Columns.Add("Employee Name", 150);
                listView1.Columns.Add("Outtime", 100);
                listView1.Columns.Add("In TIme", 100);
                listView1.Columns.Add("Total Out Time", 150);





                int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblLoginLogoutLog where employeeid='" + gunaTextBox2.Text + "' AND date='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"'";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);
                TimeSpan totalinout = TimeSpan.Zero;
                TimeSpan total = TimeSpan.Zero;
                int count = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    totalinout = TimeSpan.Zero;
                    count = 0;
                    arr[0] = Convert.ToString(dt.Rows[i]["employeeid"]);
                    arr[1] = Convert.ToString(dt.Rows[i]["logouttime"]);
                    arr[2] = Convert.ToString(dt.Rows[i]["logintime"]);

                    if (arr[1] != "" && arr[2] != "")
                    {
                        TimeSpan duration = DateTime.Parse(arr[2]).Subtract(DateTime.Parse(arr[1]));
                        totalinout = totalinout + duration;
                        total = total + totalinout;
                        count = count + 1;
                    }
                    if (count > 0)
                    {
                        string out1 = totalinout.ToString();
                        out1 = out1.Remove(out1.Length - 3);
                        arr[3] = out1;
                    }
                    else
                    {
                        arr[3] = "";
                    }
                    if(arr[1]==arr[2] || arr[2]=="")
                    {

                    }
                    else
                    {
                        itm = new ListViewItem(arr);
                        listView1.Items.Add(itm);
                    }
                 
                }
                string out2 = total.ToString();
                out2 = out2.Remove(out2.Length - 3);
                
                gunaTextBox1.Text = out2;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            MainMenu f = new MainMenu();
            f.Show();
            this.Hide();
        }
    }
}
