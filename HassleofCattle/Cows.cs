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

namespace HassleofCattle
{
    public partial class Cows : Form
    {
        public Cows()
        {
            InitializeComponent();
            populate();
            SnameLbl.Text = Login.User;
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ThinkPad\Documents\HassleofCattleDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

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

        private void label15_Click(object sender, EventArgs e)
        {
            Finances ob = new Finances();
            ob.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Dashboard ob = new Dashboard();
            ob.Show();
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void label16_Click_1(object sender, EventArgs e)
        {
            Dashboard ob = new Dashboard();
            ob.Show();
            this.Hide();


        }
        private void populate()
        {
            Con.Open();
            String query = "Select * from CowTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CowsDGV.DataSource = ds.Tables[0];
            CowsDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CowsDGV.RowTemplate.Height = 50;
            Con.Close();
        }

        private void Clear()
        {
            CowNameTb.Text = "";
            EarTagTb.Text = "";
            ColorTb.Text = "";
            BreedTb.Text = "";
            WeightTb.Text = "";
            AgeTb.Text = "";
            PastureTb.Text = "";

        }
        int age = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (CowNameTb.Text == "" || EarTagTb.Text == "" || ColorTb.Text == "" || BreedTb.Text == "" || WeightTb.Text == "" || AgeTb.Text == "" || PastureTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Insert into CowTbl values('"+CowNameTb.Text+ "','" + EarTagTb.Text + "','" + ColorTb.Text + "','" + BreedTb.Text + "','" + WeightTb.Text + "','" + age + "','" + PastureTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cow saved Successfuly");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DOBDate_ValueChanged(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date-DOBDate.Value.Date).Days)/365;
       
        }

        private void DOBDate_MouseLeave(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date - DOBDate.Value.Date).Days) / 365;
            AgeTb.Text = "" + age;
        }

        private void WeightTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }
       // int key = 0;
       

        private void button3_Click(object sender, EventArgs e)
        {


            if (key == 0)
            {
                MessageBox.Show("Select the cow to be deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from CowTbl where CowId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cow delete Successfuly");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }


        int key = 0;

        private void CowsDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            //CowNameTb.Text = CowsDGV.SelectedRows[0].Cells[1].Value.ToString();
            //EarTagTb.Text = CowsDGV.SelectedRows[0].Cells[2].Value.ToString();
            //ColorTb.Text = CowsDGV.SelectedRows[0].Cells[3].Value.ToString();
            //BreedTb.Text = CowsDGV.SelectedRows[0].Cells[4].Value.ToString();
            //WeightTb.Text = CowsDGV.SelectedRows[0].Cells[6].Value.ToString();
            //PastureTb.Text = CowsDGV.SelectedRows[0].Cells[7].Value.ToString();
            //////if (CowNameTb.Text == "")
            ////{
            ////    key = 0;
            ////    age = 0;

            ////}
            ////else
            ////{
            ////    key = Convert.ToInt32(CowsDGV.SelectedRows[0].Cells[1].Value.ToString());
            ////    age = Convert.ToInt32(CowsDGV.SelectedRows[0].Cells[5].Value.ToString());


            ////}

        }

        private void CowsDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CowNameTb.Text = CowsDGV.SelectedRows[0].Cells[1].Value.ToString();
            EarTagTb.Text = CowsDGV.SelectedRows[0].Cells[2].Value.ToString();
            ColorTb.Text = CowsDGV.SelectedRows[0].Cells[3].Value.ToString();
            BreedTb.Text = CowsDGV.SelectedRows[0].Cells[4].Value.ToString();
            WeightTb.Text = CowsDGV.SelectedRows[0].Cells[6].Value.ToString();
            PastureTb.Text = CowsDGV.SelectedRows[0].Cells[7].Value.ToString();
             if (CowNameTb.Text == "")
            {
                key = 0;
                age = 0;

            }
            else
            {
                key = Convert.ToInt32(CowsDGV.SelectedRows[0].Cells[0].Value.ToString());
                age = Convert.ToInt32(CowsDGV.SelectedRows[0].Cells[5].Value.ToString());


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CowNameTb.Text == "" || EarTagTb.Text == "" || ColorTb.Text == "" || BreedTb.Text == "" || WeightTb.Text == "" || AgeTb.Text == "" || PastureTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Update CowTbl set CowName='" + CowNameTb.Text + "',EarTag='" + EarTagTb.Text + "',Color='" + ColorTb.Text + "',Breed='" + BreedTb.Text + "',Age=" + age + ",weightatbirth=" + WeightTb.Text + ",Pasture='" + PastureTb.Text + "' where Cowid=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cow Update Successfuly");
                    Con.Close();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void SearchCow()
        {
            Con.Open();
            string query = "select * from CowTbl where CowName like '%" + CowSearchTb.Text + "%'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CowsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void CowSearchTb_MouseLeave(object sender, EventArgs e)
        {
            SearchCow();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Login ob = new Login();
            ob.Show();
            this.Hide();
        }
    }
}
