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
    public partial class UpdateAttendance : Form
    {
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        public UpdateAttendance()
        {
            InitializeComponent();
            gunaTextBox4.Text = ListAttendance.employeename;
            gunaTextBox2.Text = ListAttendance.intime;
            gunaTextBox1.Text = ListAttendance.outtime;
            gunaTextBox3.Text = ListAttendance.date;
            comboBox1.Text = ListAttendance.status;
            if(gunaTextBox1.Text=="")
            {
                gunaTextBox1.Text = DateTime.Now.ToString("h: mm tt");
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            ListAttendance f = new ListAttendance();
            f.Show();
            this.Hide();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.ParseExact(gunaTextBox3.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            connection = c.openConnection();
            String query = "update SA_WH.tblEmployeeattaindance set intime=@intime,status=@status where employeename='" + gunaTextBox4.Text + "' AND date='"+dt.ToString("yyyy-MM-dd")+ "' order by id asc limit 1";
            MySqlCommand cmd = new MySqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@intime", gunaTextBox2.Text);
            cmd.Parameters.AddWithValue("@status", comboBox1.Text);



            cmd.ExecuteNonQuery();

            query = "update SA_WH.tblEmployeeattaindance set outtime=@outtime,markoutstatus=@markoutstatus where employeename='" + gunaTextBox4.Text + "' AND date='" + dt.ToString("yyyy-MM-dd") + "' order by id desc limit 1";
            cmd = new MySqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@outtime", gunaTextBox1.Text);

            cmd.Parameters.AddWithValue("@markoutstatus", "Success");


            cmd.ExecuteNonQuery();
            ListAttendance f = new ListAttendance();
            f.Show();
            this.Hide();
        }
    }
}
