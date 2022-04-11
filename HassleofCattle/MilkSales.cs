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
    public partial class MilkSales : Form
    {
        public MilkSales()
        {
            InitializeComponent();
            FillEmpId();
            populate();
            SnameLbl.Text = Login.User;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
           
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

        private void label15_Click(object sender, EventArgs e)
        {
            Finances ob = new Finances();
            ob.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
           
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Dashboard ob = new Dashboard();
            ob.Show();
            this.Hide();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Breedings ob = new Breedings();
            ob.Show();
            this.Hide();

        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ThinkPad\Documents\HassleofCattleDb.mdf;Integrated Security=True;Connect Timeout=30");
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

        private void MilkSales_Load(object sender, EventArgs e)
        {

        }

        private void QuantityTb_Leave(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(PriceTb.Text) * Convert.ToInt32(QuantityTb.Text);
            TotalTb.Text = "" + total;
        }

        private void populate()
        {
            Con.Open();
            String query = "Select * from MilkSalesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SalesDGV.DataSource = ds.Tables[0];
            SalesDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SalesDGV.RowTemplate.Height = 50;
            Con.Close();
        }
        private void Clear()
        {
            PriceTb.Text = "";
            QuantityTb.Text = "";
            ClientNameTb.Text = "";
            ClientPhoneTb.Text = "";
            TotalTb.Text = "";

        }
        private void SaveTransaction()
        {

           
            {
                try
                {
                    string Sales = "Sales";
                    Con.Open();
                    string Query = "Insert into IncomeTbl values('" + Date.Value.Date + "','" + Sales+ "','" + TotalTb.Text + "'," + EmpIdCb.SelectedValue.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Income saved Successfuly");
                    Con.Close();
                   
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpIdCb.SelectedIndex == -1 || PriceTb.Text == "" || ClientNameTb.Text == "" || ClientPhoneTb.Text == "" || QuantityTb.Text == "" || TotalTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Insert into MilkSalesTbl values('" + Date.Value.Date + "'," + PriceTb.Text + ",'" + ClientNameTb.Text + "','" + ClientPhoneTb.Text + "'," + EmpIdCb.SelectedValue.ToString() + ",'" + QuantityTb.Text + "','" +TotalTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Milk Sold Successfuly");
                    Con.Close();
                    populate();
                    SaveTransaction();
                    Clear();  
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login ob = new Login();
            ob.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            CowHealth ob = new CowHealth();
            ob.Show();
            this.Hide();

        }
    }
}
