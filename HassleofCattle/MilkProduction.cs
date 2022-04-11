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
    public partial class MilkProduction : Form
    {
        public MilkProduction()
        {
            InitializeComponent();
            FillCowId();
            populate();
            SnameLbl.Text = Login.User;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void label7_Click(object sender, EventArgs e)
        {
            CowHealth ob = new CowHealth();
            ob.Show();
            this.Hide();
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

        private void label5_Click(object sender, EventArgs e)
        {
            Cows ob = new Cows();
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

        private void CowIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ThinkPad\Documents\HassleofCattleDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void FillCowId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CowId from CowTbl",Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CowId", typeof(int));
            dt.Load(Rdr);
            CowIdCb.ValueMember = "CowId";
            CowIdCb.DataSource = dt;
            Con.Close();
        }


        private void populate()
        {
            Con.Open();
            String query = "Select * from MilkTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MilkDGV.DataSource = ds.Tables[0];
            MilkDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            MilkDGV.RowTemplate.Height = 50;
            Con.Close();
        }
        private void Clear()
        {
            Cownametb.Text = "";
            Amtb.Text = "";
            Noontb.Text = "";
            Pmtb.Text = "";
            TotalTb.Text = "";
            key = 0;

        }

        private void GetCowName()
        {
            Con.Open();
          
            string query = "select * from CowTbl where CowId=" + CowIdCb.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                Cownametb.Text = dr["CowName"].ToString();
            }
            Con.Close();

        }
        private void button1_Click(object sender, EventArgs e)

        {
            if (CowIdCb.SelectedIndex == -1 || Cownametb.Text == "" || Amtb.Text == "" || Noontb.Text == "" || Pmtb.Text == "" || TotalTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Insert into MilkTbl values(" + CowIdCb.SelectedValue.ToString()+ ",'" + Cownametb.Text + "'," + Amtb.Text + "," + Noontb.Text + "," + Pmtb.Text + "," + TotalTb.Text+ ",'" + Date.Value.Date + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Milk saved Successfuly");
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

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }

        private void Pmtb_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Pmtb_MouseLeave(object sender, EventArgs e)
        {
            
        }
     
        private void MilkDGV_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MilkDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int key = 0;
        private void MilkDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (key == 0)
            {
                MessageBox.Show("Select Milk Product to be deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from MilkTbl where Mid=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product delete Successfuly");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (CowIdCb.SelectedIndex == -1 || Cownametb.Text == "" || Amtb.Text == "" || Noontb.Text == "" || Pmtb.Text == "" || TotalTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Update MilkTbl set CowName='" + Cownametb.Text + "',AmMilk='" + Amtb.Text + "',NoonMilk='" + Noontb.Text + "',PmMilk='" + Pmtb.Text + "',TotakMilk=" + TotalTb.Text + ",DateProd='" + Date.Value.Date + "' where Mid=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product  Update Successfuly");
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

        private void TotalTb_Leave(object sender, EventArgs e)
        {
            int total = Convert.ToInt32(Amtb.Text) + Convert.ToInt32(Noontb.Text) + Convert.ToInt32(Pmtb.Text);
            TotalTb.Text = "" + total;

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MilkDGV_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {

            CowIdCb.Text = MilkDGV.SelectedRows[0].Cells[1].Value.ToString();
            Cownametb.Text = MilkDGV.SelectedRows[0].Cells[2].Value.ToString();
            Amtb.Text = MilkDGV.SelectedRows[0].Cells[3].Value.ToString();
            Noontb.Text = MilkDGV.SelectedRows[0].Cells[4].Value.ToString();
            Pmtb.Text = MilkDGV.SelectedRows[0].Cells[5].Value.ToString();
            TotalTb.Text = MilkDGV.SelectedRows[0].Cells[6].Value.ToString();
            Date.Text = MilkDGV.SelectedRows[0].Cells[7].Value.ToString();
            if (Cownametb.Text == "")
            {
                key = 0;


            }
            else
            {
                key = Convert.ToInt32(MilkDGV.SelectedRows[0].Cells[0].Value.ToString());



            }

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Login ob = new Login();
            ob.Show();
            this.Hide();
        }
    }
}
