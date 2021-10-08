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
    public partial class Login : Form
    {
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        public Login()
        {
            InitializeComponent();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (gunaTextBox1.Text == "")
            {
                MessageBox.Show("Please Enter the Username");
            }
            else if (gunaTextBox1.Text == "")
            {
                MessageBox.Show("Please Enter the Password");
            }
            else
            {


                connection = c.openConnection();

                String query = "select count(password) from SA_WH.tblLogin where username='" + gunaTextBox1.Text + "' and password='" + gunaTextBox2.Text + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader sdr = cmd.ExecuteReader();
                string time = DateTime.Now.ToString("hh:mm:ss tt");
                int count = 0;

                if (sdr.Read())
                {
                    count = Convert.ToInt32(sdr.GetValue(0));


                }
                sdr.Close();
                if(count>0)
                {
                    MainMenu f = new MainMenu();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Login Details");
                }    
            }
        }

        private void gunaTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gunaButton1_Click(sender, e);
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gunaTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                gunaTextBox2.Focus();
            }
        }
    }
}
