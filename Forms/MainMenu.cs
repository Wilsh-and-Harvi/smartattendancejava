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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            AttendanceDisplay f = new AttendanceDisplay();
            f.Show();
            this.Hide();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            ListAttendance f = new ListAttendance();
            f.Show();
            this.Hide();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            f.Show();
            this.Hide();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            AddUser f = new AddUser();
            f.Show();
            this.Hide();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            EmployeeLoginLogout f = new EmployeeLoginLogout();
            f.Show();
            this.Hide();
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            viewoutofrange f = new viewoutofrange();
            f.Show();
            this.Hide();
        }
    }
}
