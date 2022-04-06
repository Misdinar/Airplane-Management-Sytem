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
    public partial class AddPassenger : Form
    {
        public AddPassenger()
        {
            InitializeComponent();
        }

        //Jangan lupa ganti pathnya sesuai laptop masing-masing
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SMT6\PBKK\VS22\Airplane-Management-Sytem\Database\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PassAdd.Text == "" || PassName.Text == "" 
                || PassportTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query= "insert into PassengerTbl values('" + PassName.Text + "', '" + PassportTb.Text+ "', '" + PassAdd.Text + "', '" + NationalityCb.SelectedItem.ToString()+ "', '" + GenderCb.SelectedItem.ToString() + "', '"+PhoneTb.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passanger Recorded Successfully");

                    //setelah input, isi form di kosongkan
                    PassAdd.Text = "";
                    PassName.Text = "";
                    PassportTb.Text = "";
                    PhoneTb.Text = "";
                    NationalityCb.Text = "";
                    GenderCb.Text = "";
                    Con.Close();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            
        }

        //untuk mereset semua isi form
        private void button2_Click(object sender, EventArgs e)
        {
            PassAdd.Text = "";
            PassName.Text = "";
            PassportTb.Text = "";
            PhoneTb.Text = "";
            NationalityCb.Text = "";
            GenderCb.Text = "";
        }

        //untuk kembali ke halaman utama
        private void button3_Click(object sender, EventArgs e)
        {
            Splash splash = new Splash();
            splash.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewPassenger viewpass = new ViewPassenger();
            viewpass.Show();
            this.Hide();
        }
    }
}
