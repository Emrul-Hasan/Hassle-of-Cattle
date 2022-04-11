using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HassleofCattle
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            Finance();
            Logistic();
            GetMax();
            SnameLbl.Text = Login.User;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Cows ob = new Cows();
            ob.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MilkProduction ob = new MilkProduction();
            ob.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            CowHealth ob = new CowHealth();
            ob.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Breedings ob = new Breedings();
            ob.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            MilkSales ob = new MilkSales();
            ob.Show();
            this.Hide();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Finances ob = new Finances();
            ob.Show();
            this.Hide();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ThinkPad\Documents\HassleofCattleDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Finance()
        {
            // calculate the final finanace relatedd
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(IncAmt) from IncomeTbl", Con);
            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(ExpAmount) from ExpenditureTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int inc, exp;
            double bal;
            inc = Convert.ToInt32(dt.Rows[0][0].ToString());
            IncLbl.Text = "Tk "+dt.Rows[0][0].ToString();

            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);

            exp = Convert.ToInt32(dt1.Rows[0][0].ToString());
            bal = inc - exp;
            ExpLbl.Text = "Tk "+dt1.Rows[0][0].ToString();
            BalLbl.Text = "Tk " + bal;
            Con.Close(); 
        }


        private void Logistic()
        {
            // calculate the final Logistic relatedd
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from CowTbl", Con);
            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(TotakMilk) from MilkTbl", Con);
            SqlDataAdapter sda2 = new SqlDataAdapter("select Count(*) from EmployeeTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CowLbl.Text = dt.Rows[0][0].ToString();
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            MilkLbl.Text =  dt1.Rows[0][0].ToString();

            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            EmpNumLbl.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }

        private void GetMax()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Max(IncAmt) from IncomeTbl", Con);
            SqlDataAdapter sda1 = new SqlDataAdapter("select Max(ExpAmount) from ExpenditureTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            HighestAmtLbl.Text = dt.Rows[0][0].ToString();

            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            HighestExpLbl.Text = dt1.Rows[0][0].ToString();


        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Login ob = new Login();
            ob.Show();
            this.Hide();
        }

        private void ExpLbl_Click(object sender, EventArgs e)
        {

        }
    }
}
