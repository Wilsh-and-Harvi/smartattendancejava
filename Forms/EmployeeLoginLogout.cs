using GMap.NET.MapProviders;
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
    public partial class EmployeeLoginLogout : Form
    {
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        string username = "";
        MySqlDataReader reader;

        public EmployeeLoginLogout()
        {
            InitializeComponent();
            getEmployeeName();
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
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

                gunaTextBox4.AutoCompleteCustomSource = MyCollection;


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
        private void gunaButton2_Click(object sender, EventArgs e)
        {
            int i = 0;
            i=markin(username, username, "", "", "", "", "");
            if(i>0)
            {
                MessageBox.Show("Login Successfully");
                username = "";
                EmployeeLoginLogout f = new EmployeeLoginLogout();
                f.Show();
                this.Hide();
            }


        }
      
        private void buttonenable()
        {
            connection = c.openConnection();
            string markin = "";
            string markout = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            String query = "select markinstatus,markoutstatus,intime,outtime from SA_WH.tblEmployeeattaindance where employeeid='" + username + "' AND date='" + date + "' ORDER BY id DESC LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader sdr = cmd.ExecuteReader();
            string time = DateTime.Now.ToString("hh:mm:ss tt");
            string markinButton = "";
            string markoutButton = "";

            if (sdr.Read())
            {
                markin = Convert.ToString(sdr.GetValue(0));
                markout = Convert.ToString(sdr.GetValue(1));
                markinButton = Convert.ToString(sdr.GetValue(0));
                markoutButton = Convert.ToString(sdr.GetValue(1));
                if (markin == "Success")
                {

                  
                    gunaButton2.Enabled = false;
                    //  button1.Text = "Login Time " + Convert.ToString(sdr.GetValue(2)); ;
                    gunaButton3.Enabled = true;
                }
                if (markout == "Success")
                {

                    gunaButton3.Enabled = false;
                    //  button2.Text = "Logout Time " + Convert.ToString(sdr.GetValue(3)); ;
                    gunaButton2.Enabled = false;
                }
                if (markin == "Success" && markout == "Success")
                {
                   
                    gunaButton2.Enabled = true;
                    gunaButton3.Enabled = false;
                }

            }
            else
            {
               
                gunaButton2.Enabled = true;
                gunaButton3.Enabled = false;
            }

            sdr.Close();
        }
        public int markin(string empid, string empname, string address, string postalcode, string locality, string latitude, string logitude)
        {
            connection = c.openConnection();
            string query = "insert into SA_WH.tblEmployeeattaindance ( intime,employeeid, employeename, markinaddress, markinpostalcode, markinlocalty, markinlatitude, markinlongitude, markinstatus,date) values (@intime,@employeeid,@employeename,@markinaddress,@markinpostalcode,@markinlocalty,@markinlatitude,@markinlongitude,@markinstatus,@date)";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@employeeid", empid);
            cmd.Parameters.AddWithValue("@employeename", empname);
            cmd.Parameters.AddWithValue("@markinaddress", address);
            cmd.Parameters.AddWithValue("@markinpostalcode", postalcode);
            cmd.Parameters.AddWithValue("@markinlocalty", locality);
            cmd.Parameters.AddWithValue("@markinlatitude", latitude);
            DateTime dt = DateTime.Now ;
            string time = comboBox4.Text + ":" + comboBox2.Text + "" + comboBox3.Text;
            cmd.Parameters.AddWithValue("@intime", time);
            cmd.Parameters.AddWithValue("@markinlongitude", logitude);
            cmd.Parameters.AddWithValue("@markinstatus", "Success");
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            cmd.Parameters.AddWithValue("@date", date);

            int i = 0;
            i = cmd.ExecuteNonQuery();
            return i;
        }
        public int markout(string empid, string empname, string address, string postalcode, string locality, string latitude, string logitude)
        {
            connection = c.openConnection();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string query = "update SA_WH.tblEmployeeattaindance set outtime=@outtime,markoutaddress=@markoutaddress, markoutpostalcode=@markoutpostalcode, markoutlocality=@markoutlocality, markoutlongitude=@markoutlongitude, markoutlatitude=@markoutlatitude, markoutstatus=@markoutstatus where employeeid='" + empid + "' AND date='" + date + "'ORDER BY id DESC LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@markoutaddress", address);
            cmd.Parameters.AddWithValue("@markoutpostalcode", postalcode);
            cmd.Parameters.AddWithValue("@markoutlocality", locality);
            cmd.Parameters.AddWithValue("@markoutlatitude", latitude);
            DateTime dt =DateTime.Now;
            string time = comboBox4.Text + ":" + comboBox2.Text + "" + comboBox3.Text;
            cmd.Parameters.AddWithValue("@outtime", time);
            cmd.Parameters.AddWithValue("@markoutlongitude", logitude);
            cmd.Parameters.AddWithValue("@markoutstatus", "Success");


            int i = 0;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            connection = c.openConnection();
            username = "";

            string date = DateTime.Now.ToString("yyyy-MM-dd");
            String query = "select username from SA_WH.tblLogin where username='" + gunaTextBox4.Text + "' ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader sdr = cmd.ExecuteReader();



            if (sdr.Read())
            {
                username = Convert.ToString(sdr.GetValue(0));



            }
            sdr.Close();
          
            if (username != "")
            {
                query = "select intime,outtime from SA_WH.tblEmployeeattaindance where employeename='" + gunaTextBox4.Text + "' AND date='"+DateTime.Now.ToString("yyyy-MM-dd")+"'";
                cmd = new MySqlCommand(query, connection);
                sdr = cmd.ExecuteReader();

                string intime = "";
                string outtime = "";
                while (sdr.Read())
                {
                  if(intime=="")
                    {
                        intime = Convert.ToString(sdr.GetValue(0));
                    }

                    outtime = Convert.ToString(sdr.GetValue(1));

                }
                sdr.Close();
                if(intime!="")
                {
                    string currenttime = DateTime.Now.ToString("h:mmtt");
                    if (outtime!="")
                    {
                        currenttime = outtime;
                    }
                    
                    TimeSpan duration = DateTime.Parse(currenttime).Subtract(DateTime.Parse(intime));
                    label5.Text = duration.ToString();
                }
                else
                {
                    label5.Text ="";
                }
                label3.Text = intime;
                label4.Text = outtime;
                username = gunaTextBox4.Text;
                buttonenable();
            }
            else
            {
                label3.Text = "";
                label4.Text = "";
                label5.Text = "";
                gunaButton2.Enabled = false;
                gunaButton3.Enabled = false;
                MessageBox.Show("Please Select Correct Employee");
            }

        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            int i = 0;
            i = markout(username, username, "", "", "", "", "");
            if (i > 0)
            {
                MessageBox.Show("Logout Successfully");
                username = "";
                EmployeeLoginLogout f = new EmployeeLoginLogout();
                f.Show();
                this.Hide();
            }
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            MainMenu f = new MainMenu();
            f.Show();
            this.Hide();
        }
    }
}
