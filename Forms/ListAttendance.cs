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
    public partial class ListAttendance : Form
    {
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        MySqlDataReader reader;
        public static string employeename = "";
        public static string date = "";
        public static string intime = "";
        public static string outtime = "";
        public static string status = "";
        public ListAttendance()
        {
            InitializeComponent();
            getTest();
            getEmployeeName();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            MainMenu f = new MainMenu();
            f.Show();
            this.Hide();
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
        private void getTest()
        {
           


                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.Update(); // In case there is databinding
                listView1.Refresh(); // Redraw items

                listView1.View = View.Details;
                listView1.GridLines = true;
                listView1.FullRowSelect = true;


            listView1.Columns.Add("Employee Name", 200);
            listView1.Columns.Add("Date", 100);
            listView1.Columns.Add("In TIme", 120);
            listView1.Columns.Add("OutTime", 120);
            listView1.Columns.Add("Login Hours", 130);
            listView1.Columns.Add("Out of Office", 130);
            listView1.Columns.Add("Status", 120);




            int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblEmployeeattaindance where date='"+DateTime.Now.ToString("yyyy-MM-dd")+"' group by employeeid,date";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["employeename"]);
                    arr[1] = (Convert.ToDateTime(dt.Rows[i]["date"])).ToString("dd-MM-yyyy");
                    arr[2] = Convert.ToString(dt.Rows[i]["intime"]);
                   
                   

                    query = "select outtime from SA_WH.tblEmployeeattaindance where employeeid='" +arr[0] + "' AND date='" + (Convert.ToString(dt.Rows[i]["date"])) + "' ORDER BY id DESC LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader sdr = cmd.ExecuteReader();
                    string time = DateTime.Now.ToString("hh:mm:ss tt");


                    if (sdr.Read())
                    {
                       arr[3]= Convert.ToString(sdr.GetValue(0));


                    }
                    else
                    {
                        arr[3] = "";
                    }
                   
                    sdr.Close();

                    string currenttime = DateTime.Now.ToString("h: mm tt");

                    string starttime = arr[2];
                    string endtime = arr[3];
                    if(arr[1] == DateTime.Now.ToString("dd-MM-yyyy") && endtime=="")
                    {
                        endtime = currenttime;
                    }
                   if(endtime=="")
                    {
                        arr[4] = "";
                    }
                   else
                    {
                        TimeSpan duration = DateTime.Parse(endtime).Subtract(DateTime.Parse(starttime));

                        arr[4] = duration.ToString();
                    }

                int noofcount = 0;
                    query = "select logouttime,logintime from SA_WH.tblLoginLogoutLog where employeeid='" + arr[0] + "' AND date='" + Convert.ToString(dt.Rows[i]["date"]) + "' ";
                    cmd = new MySqlCommand(query, connection);
                    sdr = cmd.ExecuteReader();
                    time = DateTime.Now.ToString("hh:mm:ss tt");
                    string outrange = "";
                    string inrange = "";
                    TimeSpan totalinout = TimeSpan.Zero;
                int count = 0;
                    while (sdr.Read())
                    {
                    noofcount = noofcount + 1;
                        outrange = Convert.ToString(sdr.GetValue(0));
                        inrange = Convert.ToString(sdr.GetValue(1));
                        if (outrange != "" && inrange != "")
                        {
                            TimeSpan duration = DateTime.Parse(inrange).Subtract(DateTime.Parse(outrange));
                            totalinout = totalinout + duration;
                        count = count + 1;
                        }

                    }


                    sdr.Close();
                if (count > 0)
                {
                    string out1 = totalinout.ToString();
                    out1 = out1.Remove(out1.Length - 3);
                    arr[5] = out1 + " (" + noofcount + ")";
                }
                else
                {
                    arr[5] = "";
                }




                if (arr[4] == "")
                    {
                        arr[6] = "";
                    }
                    else
                    {
                        DateTime time1 = Convert.ToDateTime("05:00:00");
                        if (Convert.ToDateTime(arr[4]) > time1)
                        {
                            arr[6] = "Present";
                        }
                        else
                        {
                            arr[6] = "Half Day";
                        }
                    }
                    if (Convert.ToString(dt.Rows[i]["status"]) != "")
                    {
                        arr[6] = Convert.ToString(dt.Rows[i]["status"]);
                    }

                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
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
                listView1.Columns.Add("Date", 100);
                listView1.Columns.Add("In TIme", 120);
                listView1.Columns.Add("OutTime", 120);
                listView1.Columns.Add("Login Hours", 130);
                listView1.Columns.Add("Out of Office", 130);
                listView1.Columns.Add("Status", 120);





                int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblEmployeeattaindance where employeename='" + gunaTextBox2.Text+ "' group by employeeid,date";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["employeename"]);
                    arr[1] = (Convert.ToDateTime(dt.Rows[i]["date"])).ToString("dd-MM-yyyy");
                    arr[2] = Convert.ToString(dt.Rows[i]["intime"]);



                    query = "select outtime from SA_WH.tblEmployeeattaindance where employeeid='" + arr[0] + "' AND date='" + (Convert.ToString(dt.Rows[i]["date"])) + "' ORDER BY id DESC LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader sdr = cmd.ExecuteReader();
                    string time = DateTime.Now.ToString("hh:mm:ss tt");


                    if (sdr.Read())
                    {
                        arr[3] = Convert.ToString(sdr.GetValue(0));


                    }
                    else
                    {
                        arr[3] = "";
                    }

                    sdr.Close();

                    string currenttime = DateTime.Now.ToString("h: mm tt");

                    string starttime = arr[2];
                    string endtime = arr[3];
                    if (arr[1] == DateTime.Now.ToString("dd-MM-yyyy") && endtime == "")
                    {
                        endtime = currenttime;
                    }
                    if (endtime == "")
                    {
                        arr[4] = "";
                    }
                    else
                    {
                        TimeSpan duration = DateTime.Parse(endtime).Subtract(DateTime.Parse(starttime));

                        arr[4] = duration.ToString();
                    }
                    int noofcount = 0;
                    query = "select logouttime,logintime from SA_WH.tblLoginLogoutLog where employeeid='" + arr[0] + "' AND date='" + (Convert.ToString(dt.Rows[i]["date"])) + "' ";
                    cmd = new MySqlCommand(query, connection);
                    sdr = cmd.ExecuteReader();
                    time = DateTime.Now.ToString("hh:mm:ss tt");
                    string outrange = "";
                    string inrange = "";
                    TimeSpan totalinout = TimeSpan.Zero;
                    int count = 0;
                    while (sdr.Read())
                    {
                        noofcount = noofcount + 1;
                        outrange = Convert.ToString(sdr.GetValue(0));
                        inrange = Convert.ToString(sdr.GetValue(1));
                        if (outrange != "" && inrange != "")
                        {
                            TimeSpan duration = DateTime.Parse(inrange).Subtract(DateTime.Parse(outrange));
                            totalinout = totalinout + duration;
                            count = count + 1;
                        }

                    }


                    sdr.Close();
                    if (count > 0)
                    {
                        string out1 = totalinout.ToString();
                        out1 = out1.Remove(out1.Length - 3);
                        arr[5] = out1 + " (" + noofcount + ")";
                    }
                    else
                    {
                        arr[5] = "";
                    }




                    if (arr[4] == "")
                    {
                        arr[6] = "";
                    }
                    else
                    {
                        DateTime time1 = Convert.ToDateTime("05:00:00");
                        if (Convert.ToDateTime(arr[4]) > time1)
                        {
                            arr[6] = "Present";
                        }
                        else
                        {
                            arr[6] = "Half Day";
                        }
                    }
                    if (Convert.ToString(dt.Rows[i]["status"]) != "")
                    {
                        arr[6] = Convert.ToString(dt.Rows[i]["status"]);
                    }



                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
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
                listView1.Columns.Add("Date", 100);
                listView1.Columns.Add("In TIme", 120);
                listView1.Columns.Add("OutTime", 120);
                listView1.Columns.Add("Login Hours", 130);
                listView1.Columns.Add("Out of Office", 130);
                listView1.Columns.Add("Status", 120);





                int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblEmployeeattaindance where date='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' group by employeeid,date";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["employeename"]);
                    arr[1] = (Convert.ToDateTime(dt.Rows[i]["date"])).ToString("dd-MM-yyyy");
                    arr[2] = Convert.ToString(dt.Rows[i]["intime"]);



                    query = "select outtime from SA_WH.tblEmployeeattaindance where employeeid='" + arr[0] + "' AND date='" + (Convert.ToString(dt.Rows[i]["date"])) + "' ORDER BY id DESC LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader sdr = cmd.ExecuteReader();
                    string time = DateTime.Now.ToString("hh:mm:ss tt");


                    if (sdr.Read())
                    {
                        arr[3] = Convert.ToString(sdr.GetValue(0));


                    }
                    else
                    {
                        arr[3] = "";
                    }

                    sdr.Close();

                    string currenttime = DateTime.Now.ToString("h: mm tt");

                    string starttime = arr[2];
                    string endtime = arr[3];
                    if (arr[1] == DateTime.Now.ToString("dd-MM-yyyy") && endtime == "")
                    {
                        endtime = currenttime;
                    }
                    if (endtime == "")
                    {
                        arr[4] = "";
                    }
                    else
                    {
                        TimeSpan duration = DateTime.Parse(endtime).Subtract(DateTime.Parse(starttime));

                        arr[4] = duration.ToString();
                    }
                    int noofcount = 0;
                    query = "select logouttime,logintime from SA_WH.tblLoginLogoutLog where employeeid='" + arr[0] + "' AND date='" + (Convert.ToString(dt.Rows[i]["date"])) +"' ";
                    cmd = new MySqlCommand(query, connection);
                    sdr = cmd.ExecuteReader();
                    time = DateTime.Now.ToString("hh:mm:ss tt");
                    string outrange = "";
                    int count = 0;
                        string inrange = "";
                    TimeSpan totalinout = TimeSpan.Zero;
                    while (sdr.Read())
                    {
                        noofcount = noofcount + 1;
                        outrange = Convert.ToString(sdr.GetValue(0));
                        inrange = Convert.ToString(sdr.GetValue(1));
                        if(outrange!="" && inrange!="")
                        {
                            TimeSpan duration = DateTime.Parse(inrange).Subtract(DateTime.Parse(outrange));
                            totalinout = totalinout + duration;
                            count = count + 1;
                        }

                    }
                  if(count>0)
                    {
                        string out1 = totalinout.ToString();
                        out1 = out1.Remove(out1.Length - 3);
                        arr[5] = out1 + " (" + noofcount + ")";
                    }
                  else
                    {
                        arr[5] = "";
                    }

                    sdr.Close();
                   




                    if (arr[4] == "")
                    {
                        arr[6] = "";
                    }
                    else
                    {
                        DateTime time1 = Convert.ToDateTime("05:00:00");
                        if (Convert.ToDateTime(arr[4]) > time1)
                        {
                            arr[6] = "Present";
                        }
                        else
                        {
                            arr[6] = "Half Day";
                        }
                    }
                    if (Convert.ToString(dt.Rows[i]["status"]) != "")
                    {
                        arr[6] = Convert.ToString(dt.Rows[i]["status"]);
                    }




                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            getTest1();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            getTest();
        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getTest1();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                employeename = listView1.SelectedItems[0].SubItems[0].Text;
                date = listView1.SelectedItems[0].SubItems[1].Text;
                intime = listView1.SelectedItems[0].SubItems[2].Text;
                outtime = listView1.SelectedItems[0].SubItems[3].Text;
                status = listView1.SelectedItems[0].SubItems[5].Text;
                UpdateAttendance f = new UpdateAttendance();
                f.Show();
                this.Hide();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            getTest3();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            AttendanceOutin f = new AttendanceOutin();
            f.Show();
            this.Hide();
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                LiveLocation.employeeid = listView1.SelectedItems[0].SubItems[0].Text;
                LiveLocation f = new LiveLocation();
                f.Show();
                this.Hide();
             
            }
            else
            {
                MessageBox.Show("Please select atlist one Record...!!!");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
