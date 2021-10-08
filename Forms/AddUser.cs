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
    public partial class AddUser : Form
    {
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        MySqlDataReader reader;
        public AddUser()
        {
            InitializeComponent();
            
            getEmployeeName();
            getAdmin();
            getTest();
          //  getreceiptno();
        }
        private void getAdmin()
        {
            try
            {
                connection = c.openConnection();
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox1.DataBindings.Clear();
                String query = "select * from SA_WH.tblLogin where status='superviser'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet("Dealer");
                da.Fill(ds, "Dealer");
                comboBox1.DataSource = ds;
                comboBox1.ValueMember = "Dealer.id";
                comboBox1.DisplayMember = "Dealer.username";
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }

        }
        private void getreceiptno()
        {
            try
            {
                int cc = 0, billno = 1001;

                connection = c.openConnection();
                string query = "select count(employeeid) from SA_WH.tblLogin";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cc = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (cc == 0)
                {
                    gunaTextBox5.Text = Convert.ToString(billno);
                }
                else
                {
                    query = "select max(employeeid) from SA_WH.tblLogin";
                    MySqlCommand cmd1 = new MySqlCommand(query, connection);
                    MySqlDataReader sdr1 = cmd1.ExecuteReader();
                    if (sdr1.Read())
                    {
                        billno = 1 + Convert.ToInt32(sdr1.GetValue(0));
                        gunaTextBox5.Text = Convert.ToString(billno);
                    }
                    sdr1.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private bool checkUsername()
        {
            try
            {
                int count = 0;
                connection = c.openConnection();
                string query = "select count(id) from SA_WH.tblLogin where username='" + gunaTextBox2.Text + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    count = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (count > 0)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }
        private bool checkEmpid()
        {
            try
            {
                int count = 0;
                connection = c.openConnection();
                string query = "select count(id) from SA_WH.tblLogin where employeeid='" + gunaTextBox5.Text + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    count = Convert.ToInt32(sdr.GetValue(0));
                }
                sdr.Close();
                if (count > 0)
                {
                    return false;
                }
                else
                    return true;
            }
            catch (Exception ee)
            {
                return false;
            }
        }
        private void gunaButton3_Click(object sender, EventArgs e)
        {
            if (checkUsername() == true)
            {

                if (checkEmpid() == true)
                {


                    connection = c.openConnection();
                    String query = "insert into SA_WH.tblLogin (username, password, employeeid, employeename,status,superviser) values (@username, @password, @employeeid, @employeename,@status,@superviser)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@username", gunaTextBox2.Text);
                    cmd.Parameters.AddWithValue("@password", "Remsys");
                    cmd.Parameters.AddWithValue("@employeeid", gunaTextBox5.Text);

                    cmd.Parameters.AddWithValue("@employeename", gunaTextBox4.Text);
                    string status = "";
                    if(gunaCheckBox1.Checked==true)
                    {
                        status = "admin";
                    }
                    if(gunaCheckBox2.Checked==true)
                    {
                        status = "superviser";
                    }
                    if (gunaCheckBox2.Checked == true && gunaCheckBox1.Checked == true)
                    {
                        status = "adminsuperviser";
                    }
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@superviser", comboBox1.Text);

                    cmd.ExecuteNonQuery();
                    AddUser f = new AddUser();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Employee id already Exists...!!!");
                }
            }
            else
            {
                MessageBox.Show("Username already Exists...!!!");
            }
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

                gunaTextBox6.AutoCompleteCustomSource = MyCollection;


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
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            connection = c.openConnection();
            String query = "update SA_WH.tblLogin set employeeid=@employeeid, employeename=@employeename,status=@status,superviser=@superviser where employeeid='" + gunaTextBox5.Text+"'";
            MySqlCommand cmd = new MySqlCommand(query, connection);

          
          
            cmd.Parameters.AddWithValue("@employeeid", gunaTextBox5.Text);

            cmd.Parameters.AddWithValue("@employeename", gunaTextBox4.Text);
            cmd.Parameters.AddWithValue("@superviser", comboBox1.Text);
            string status = "";
            if (gunaCheckBox1.Checked == true)
            {
                status = "admin";
            }
            if (gunaCheckBox2.Checked == true)
            {
                status = "superviser";
            }
            if (gunaCheckBox2.Checked == true && gunaCheckBox1.Checked == true)
            {
                status = "adminsuperviser";
            }
            cmd.Parameters.AddWithValue("@status", status);


            cmd.ExecuteNonQuery();
            AddUser f = new AddUser();
            f.Show();
            this.Hide();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            connection = c.openConnection();
            string query = "select username, password, employeeid, employeename,status,superviser from SA_WH.tblLogin where username='" + gunaTextBox6.Text+"'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader sdr = cmd.ExecuteReader();
         


            if (sdr.Read())
            {
                gunaTextBox5.Text = Convert.ToString(sdr.GetValue(2));
                gunaTextBox4.Text = Convert.ToString(sdr.GetValue(3));
                gunaTextBox2.Text = Convert.ToString(sdr.GetValue(0));
                gunaTextBox1.Text = Convert.ToString(sdr.GetValue(1));
                comboBox1.Text = Convert.ToString(sdr.GetValue(5));
                if (Convert.ToString(sdr.GetValue(4)) == "admin")
                {
                    gunaCheckBox1.Checked = true;
                }
                else
                {
                    gunaCheckBox1.Checked = false;
                }
                if (Convert.ToString(sdr.GetValue(4)) == "superviser")
                {
                    gunaCheckBox2.Checked = true;
                }
                else
                {
                    gunaCheckBox2.Checked = false;
                }

                gunaTextBox2.ReadOnly = true;


            }

            sdr.Close();
        }
        private void search(string username)
        {
            connection = c.openConnection();
            string query = "select username, password, employeeid, employeename,status,superviser from SA_WH.tblLogin where username='" + username + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader sdr = cmd.ExecuteReader();



            if (sdr.Read())
            {
                gunaTextBox5.Text = Convert.ToString(sdr.GetValue(2));
                gunaTextBox4.Text = Convert.ToString(sdr.GetValue(3));
                gunaTextBox2.Text = Convert.ToString(sdr.GetValue(0));
                gunaTextBox1.Text = Convert.ToString(sdr.GetValue(1));
                comboBox1.Text = Convert.ToString(sdr.GetValue(5));
                if (Convert.ToString(sdr.GetValue(4))=="admin")
                {
                    gunaCheckBox1.Checked = true;
                }
                else
                {
                    gunaCheckBox1.Checked = false;
                }
                if (Convert.ToString(sdr.GetValue(4)) == "superviser")
                {
                    gunaCheckBox2.Checked = true;
                }
                else
                {
                    gunaCheckBox2.Checked = false;
                }

                gunaTextBox2.ReadOnly = true;


            }

            sdr.Close();

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            MainMenu f = new MainMenu();
            f.Show();
            this.Hide();
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
                listView1.Columns.Add("Username", 150);
                listView1.Columns.Add("Status", 0);






                int i;

                connection = c.openConnection();

                DataTable dt = new DataTable();
                string[] arr = new string[14];
                ListViewItem itm;
                String query = "select * from SA_WH.tblLogin ORDER BY employeename";
                MySqlCommand cmd1 = new MySqlCommand(query, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd1);
                da.Fill(dt);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    arr[0] = Convert.ToString(dt.Rows[i]["employeename"]);
                    arr[1] = (Convert.ToString(dt.Rows[i]["username"]));
                    arr[3] = (Convert.ToString(dt.Rows[i]["status"]));
                    if (arr[0]=="")
                    {
                        arr[0] = arr[1];
                    }

                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string status = (listView1.Items[i].SubItems[3].Text);


                    if (status == "admin")
                    {
                        listView1.Items[i].BackColor = Color.Green;
                        listView1.Items[i].ForeColor = Color.White;
                    }
                    if (status == "superviser")
                    {
                        listView1.Items[i].BackColor = Color.Orange;
                        listView1.Items[i].ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ee)
            {

            }

        }

        private void gunaTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            AddUser f = new AddUser();
            f.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaLabel4_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel5_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            connection = c.openConnection();
            String query = "update SA_WH.tblLogin set password=@password where employeeid='" + gunaTextBox5.Text + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@password", "Remsys");
          


            cmd.ExecuteNonQuery();
            AddUser f = new AddUser();
            f.Show();
            this.Hide();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string username = listView1.SelectedItems[0].SubItems[1].Text;
            search(username);
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            if (gunaTextBox2.Text == "")
            {
                MessageBox.Show("Please select the User...!!!");
            }
            else
            {


                string message = "Are you sure want to delete this User...!!!";
                string title = "Delete";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.OK)
                {
                    string username = gunaTextBox2.Text;
                    connection = c.openConnection();
                    String query = "delete from SA_WH.tblLogin where username='" + username + "'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);


                    cmd.ExecuteNonQuery();
                    AddUser f = new AddUser();
                    f.Show();
                    this.Hide();
                }
            }
               
        }

        private void gunaCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void gunaCheckBox1_Click(object sender, EventArgs e)
        {
            if (gunaCheckBox1.Checked == true)
            {
                //gunaCheckBox2.Checked = false;
                string message = "Are you sure you want to provide Admin Rights...";
                string title = "Access Control";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.OK)
                {

                }
                else
                {
                    gunaCheckBox1.Checked = false;

                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaCheckBox2_Click(object sender, EventArgs e)
        {
            if (gunaCheckBox2.Checked == true)
            {
               // gunaCheckBox1.Checked = false;
                string message = "Are you sure you want to provide Superviser Rights...";
                string title = "Access Control";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.OK)
                {

                }
                else
                {
                    gunaCheckBox2.Checked = false;

                }
            }
        }

        private void gunaCheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
