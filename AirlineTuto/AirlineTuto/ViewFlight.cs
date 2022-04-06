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
    public partial class ViewFlight : Form
    {
        public ViewFlight()
        {
            InitializeComponent();
        }

        //Jangan lupa ganti pathnya sesuai laptop masing-masing
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SMT6\PBKK\VS22\Airplane-Management-Sytem\Database\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select Fcode as [Flight Code], Fsrc as [From], Fdest as [To], Fdate as [Date], Fcap as [Total Seats] from FlightTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FlightGDV.DataSource = ds.Tables[0];
            Con.Close();
        }


        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Splash splash = new Splash();
            splash.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            Fdate.Text = "";
            Fdest.Text = "";
            SeatNum.Text = "";
            Fsrc.Text = "";
        }

        private void ViewFlight_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void FlightGDV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FcodeTb.Text = FlightGDV.SelectedRows[0].Cells[0].Value.ToString();
            Fsrc.SelectedItem = FlightGDV.SelectedRows[0].Cells[1].Value.ToString();
            Fdest.SelectedItem = FlightGDV.SelectedRows[0].Cells[2].Value.ToString();
            Fdate.Text = FlightGDV.SelectedRows[0].Cells[3].Value.ToString();
            SeatNum.Text = FlightGDV.SelectedRows[0].Cells[4].Value.ToString();

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "")
            {
                MessageBox.Show("Select The Flight To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from FlightTbl where Fcode='" + FcodeTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Deleted Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "" || SeatNum.Text == "" || Fsrc.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update FlightTbl set Fsrc='" + Fsrc.SelectedItem.ToString() + "',Fdest='" + Fdest.SelectedItem.ToString() + "', Fdate='" + Fdate.Value.Date.ToString() + "', Fcap='" + SeatNum.Text + "' where Fcode='" + FcodeTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Updated Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
