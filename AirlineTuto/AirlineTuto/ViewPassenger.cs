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
    public partial class ViewPassenger : Form
    {
        public ViewPassenger()
        {
            InitializeComponent();
        }

        //Jangan lupa ganti pathnya sesuai laptop masing-masing
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SMT6\PBKK\VS22\Airplane-Management-Sytem\Database\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        int key = 0;

        private void populate()
        {
            Con.Open();
            string query = "select PassId as [Id], PassName as [Name], Passport as [Passport], PassAd as [Address], PassNat as [Nationality], PassGend as [Gender], PassPhone as [Phone Number] from PassengerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PassengerGDV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void PassengerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(PassengerGDV.SelectedRows[0].Cells[0].Value.ToString());
            PassName.Text = PassengerGDV.SelectedRows[0].Cells[1].Value.ToString();
            PassportTb.Text = PassengerGDV.SelectedRows[0].Cells[2].Value.ToString();
            PassAdd.Text = PassengerGDV.SelectedRows[0].Cells[3].Value.ToString();
            NationalityCb.Text = PassengerGDV.SelectedRows[0].Cells[4].Value.ToString();
            GenderCb.Text = PassengerGDV.SelectedRows[0].Cells[5].Value.ToString();
            PhoneTb.Text = PassengerGDV.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Splash spalsh = new Splash();
            spalsh.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PassAdd.Text = "";
            PassName.Text = "";
            PassportTb.Text = "";
            PhoneTb.Text = "";
            NationalityCb.Text = "";
            GenderCb.Text = "";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ViewPassenger_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Passenger To Be Deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from PassengerTbl where PassId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger Deleted Successfully");
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
            if (key == 0 || PassAdd.Text == "" || PassName.Text == "" || NationalityCb.Text==""
                || PassportTb.Text == "" || PhoneTb.Text == "" || GenderCb.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update PassengerTbl set PassName='" + PassName.Text + "', PassPhone='" + PhoneTb.Text + "',Passport='" + PassportTb.Text + "',PassAd='" + PassAdd.Text + "',PassGend='" + GenderCb.Text + "',PassNat='" + NationalityCb.Text + "' where PassId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger Updated Successfully");
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
