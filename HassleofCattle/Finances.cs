using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HassleofCattle
{
    public partial class Finances : Form
    {
        public Finances()
        {
            InitializeComponent();
            populateExp();
            populateInc();
            FillEmpId();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            Dashboard ob = new Dashboard();
            ob.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            MilkSales ob = new MilkSales();
            ob.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Breedings ob = new Breedings();
            ob.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            CowHealth ob = new CowHealth();
            ob.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            MilkProduction ob = new MilkProduction();
            ob.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Cows ob = new Cows();
            ob.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ThinkPad\Documents\HassleofCattleDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populateExp()
        {
            Con.Open();
            String query = "Select * from ExpenditureTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populateInc()
        {
            Con.Open();
            String query = "Select * from IncomeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            IncDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FilterIncome()
        {
            Con.Open();
            String query = "Select * from IncomeTbl where IncDate='"+IncDateFilter.Value.Date+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            IncDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FilterExp()
        {
            Con.Open();
            String query = "Select * from ExpenditureTbl where ExpDate='"+ExpDateFilter.Value.Date+"'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ExpDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ClearExp()
        {
            AmountTb.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (PurposeCb.SelectedIndex == -1 || AmountTb.Text == "" || EmpIdCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Insert into ExpenditureTbl values('" + ExpDate.Value.Date + "','" + PurposeCb.SelectedItem.ToString() + "','" + AmountTb.Text + "'," + EmpIdCb.SelectedValue.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Expenditure saved Successfuly");
                    Con.Close();
                    populateExp();
                    ClearExp();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void FillEmpId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select EmpId from EmployeeTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpId", typeof(int));
            dt.Load(Rdr);
            EmpIdCb.ValueMember = "EmpId";
            EmpIdCb.DataSource = dt;
            Con.Close();
        }
        private void ClearInc()
        {
            IncPurCb.SelectedIndex = -1;
            IncAmount.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (IncPurCb.SelectedIndex == -1 || IncAmount.Text == "" || EmpIdCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Insert into IncomeTbl values('" + IncDate.Value.Date + "','" + IncPurCb.SelectedItem.ToString() + "','" + IncAmount.Text + "'," + EmpIdCb.SelectedValue.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Income saved Successfuly");
                    Con.Close();
                    populateInc();
                    ClearInc();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void IncDateFilter_ValueChanged(object sender, EventArgs e)
        {
            FilterIncome();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            populateInc();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            populateExp();
        }

        private void ExpDateFilter_ValueChanged_1(object sender, EventArgs e)
        {
            FilterExp();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Login ob = new Login();
            ob.Show();
            this.Hide();
        }
    }
}
