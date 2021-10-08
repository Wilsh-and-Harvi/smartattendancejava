using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
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
    public partial class LiveLocation : Form
    {
        int zoom = 18;
        CTRConnection c = new CTRConnection();
        MySqlConnection connection;
        public static string employeeid = "";
        public LiveLocation()
        {
            InitializeComponent();
            try
            {
                timer1.Start();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
         
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
           
        }
        private void LiveLocation1()
        {
            try
            {


                connection = c.openConnection();
                string query = "select latitude,longitude,time from SA_WH.tblLiveLocation where employeeid='" + employeeid + "' ";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader sdr = cmd.ExecuteReader();
                string latitude = "";
                string longitude = "";


                if (sdr.Read())
                {
                    latitude = Convert.ToString(sdr.GetValue(0));
                    longitude = Convert.ToString(sdr.GetValue(1));
                    label1.Text = "Time: " + Convert.ToString(sdr.GetValue(2));

                }


                sdr.Close();
                connection.Close();
                gMapControl1.Overlays.Clear();
                gMapControl1.DragButton = MouseButtons.Right;
                if (gunaSwitch1.Checked == true)
                {
                    gMapControl1.MapProvider = GMapProviders.GoogleSatelliteMap;
                }
                else
                {
                    gMapControl1.MapProvider = GMapProviders.GoogleMap;
                }
                //gMapControl1.MapProvider = GMapProviders.GoogleMap;

                double lat = Convert.ToDouble(latitude);
                double longi = Convert.ToDouble(longitude);
                gMapControl1.Position = new GMap.NET.PointLatLng(lat, longi);
                gMapControl1.MinZoom = 5;
                gMapControl1.MaxZoom = 100;
                gMapControl1.Zoom = zoom;
                PointLatLng point = new PointLatLng(lat, longi);
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
                GMapOverlay markers = new GMapOverlay("Markers");
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            //MessageBox.Show("Successfully Refresh");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            LiveLocation1();
        }

        private void LiveLocation_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            ListAttendance f = new ListAttendance();
            f.Show();
            this.Hide();
        }

        private void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Clicks.Equals(2))
            {
                PointLatLng pt = gMapControl1.FromLocalToLatLng(e.X, e.Y);

                gMapControl1.Position = pt;

                if(e.Button.Equals(MouseButtons.Left))
                 {
                    // Zoom in with left mouse button
                    gMapControl1.Zoom += 1;
                 }
                else if(e.Button.Equals(MouseButtons.Right))
                 {
                    // Zoom out with right mouse button
                    gMapControl1.Zoom -= 1;
                }
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {

        }

        private void gunaSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            LiveLocation1();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            zoom = Convert.ToInt32(comboBox1.Text);
            gMapControl1.Zoom = zoom;
        }
    }
}
