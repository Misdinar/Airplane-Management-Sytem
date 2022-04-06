using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AirlineTuto
{
    public partial class FlightTbl : Form
    {
        public FlightTbl()
        {
            InitializeComponent();
        }

        //Jangan lupa ganti pathnya sesuai laptop masing-masing
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SMT6\PBKK\VS22\Airplane-Management-Sytem\Database\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void button2_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            Fdate.Text = "";
            Fdest.Text = "";
            SeatNum.Text = "";
            Fsrc.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "" || Fdate.Text == "" || SeatNum.Text == ""
                || Fdest.Text == "" || Fsrc.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into FlightTbl values('" + FcodeTb.Text + "', '" + Fsrc.SelectedItem.ToString() + "', '" + Fdest.SelectedItem.ToString() + "', '" + Fdate.Value.ToString() + "', '" + SeatNum.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Recorded Successfully");

                    //setelah input, isi form di kosongkan
                    FcodeTb.Text = "";
                    Fdate.Text = "";
                    Fdest.Text = "";
                    SeatNum.Text = "";
                    Fsrc.Text = "";
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewFlight viewFlight = new ViewFlight();
            viewFlight.Show();
            this.Hide();
        }

        private void FlightTbl_Load(object sender, EventArgs e)
        {

        }
    }
}
