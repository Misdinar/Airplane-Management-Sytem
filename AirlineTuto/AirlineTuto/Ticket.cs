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
    public partial class Ticket : Form
    {
        public Ticket()
        {
            InitializeComponent();
        }

        //Jangan lupa ganti pathnya sesuai laptop masing-masing
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\SMT6\PBKK\VS22\Airplane-Management-Sytem\Database\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        string pname, ppass, pnat;

        private void populate()
        {
            Con.Open();
            string query = "select TId as [Ticket Id], Fcode as [Flight Code], PId as [Passenger Id], PName as [Name], PPass as [Passport], PNation as [Nationality], Amt as [Amount]  from TicketTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            BookingDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void fillPassenger()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PassId from PassengerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PassId", typeof(string));
            dt.Load(rdr);
            PIdCb.ValueMember = "PassId";
            PIdCb.DataSource = dt;
            Con.Close();
        }

        private void fillFlightCode()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select Fcode from FlightTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Fcode", typeof(string));
            dt.Load(rdr);
            Fcode.ValueMember = "Fcode";
            Fcode.DataSource = dt;
            Con.Close();
        }

        private void fetchpassenger()
        {
            Con.Open();
            string query = "select * from PassengerTbl where PassId="+PIdCb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                //ambil data dari hasil query
                pname = dr["PassName"].ToString();
                ppass = dr["Passport"].ToString();
                pnat = dr["PassNat"].ToString();

                //data dimasukkan ke setiap bagian form
                PNameTb.Text = pname;
                PPassTb.Text = ppass;
                PNatTb.Text = pnat;
            }
            Con.Close();
        }


        private void Ticket_Load(object sender, EventArgs e)
        {
            fillPassenger();
            fillFlightCode();
            populate();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PNameTb.Text == "" || PPassTb.Text == "" || PNatTb.Text == "" || PAmtTb.Text == ""
                || Fcode.SelectedValue.ToString() == "" || PIdCb.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into TicketTbl values('" + Fcode.SelectedValue.ToString() + "', '" + PIdCb.SelectedValue.ToString() + "', '" + PNameTb.Text + "', '" + PPassTb.Text + "', '" + PNatTb.Text + "', '" + PAmtTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket Booked Successfully");

                    //setelah input, isi form di kosongkan
                    PNameTb.Text = "";
                    PPassTb.Text = "";
                    PNatTb.Text = "";
                    Fcode.Text = "";
                    PIdCb.Text = "";
                    PAmtTb.Text = "";
                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Splash splash = new Splash();
            splash.Show();
            this.Hide();
        }

        private void PIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchpassenger();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PNameTb.Text = "";
            PPassTb.Text = "";
            PNatTb.Text = "";
            PIdCb.Text = "";
            Fcode.Text = "";
            PAmtTb.Text = "";
        }
    }
}
