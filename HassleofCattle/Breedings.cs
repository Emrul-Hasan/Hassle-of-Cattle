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
    public partial class Breedings : Form
    {
        public Breedings()
        {
            InitializeComponent();
            populate();
            FillCowId();
            SnameLbl.Text = Login.User;
        }

        private void label12_Click(object sender, EventArgs e)
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

        private void label7_Click(object sender, EventArgs e)
        {
            CowHealth ob = new CowHealth();
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

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }


        private void Clear()
        {
            CowNameTb.Text = "";
            RemarksTb.Text = "";
            CowAgeTb.Text = "";
            key = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || RemarksTb.Text == "" || CowAgeTb.Text =="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Insert into BreedTbl values('"+HeatDate.Value.Date+"','" + BreedDate.Value.Date+ "','" + CowIdCb.SelectedValue.ToString() + "','" + CowNameTb.Text + "','" + PregDate.Value.Date + "','" + ExpDate.Value.Date + "','" + DateCalved.Value.Date + "','" + CowAgeTb.Text + "','" + RemarksTb.Text+ "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Breeding Report Saved");
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

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ThinkPad\Documents\HassleofCattleDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void FillCowId()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CowId from CowTbl", Con);
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
            String query = "Select * from BreedTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BreedDGV.DataSource = ds.Tables[0];
            BreedDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BreedDGV.RowTemplate.Height = 50;
            Con.Close();
        }

       

        private void GetCowName()
        {
            Con.Open();

            string query = "select * from CowTbl where CowId=" + CowIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CowNameTb.Text = dr["CowName"].ToString();
                CowAgeTb.Text = dr["Age"].ToString();
            }
            Con.Close();

        }

        private void Breedings_Load(object sender, EventArgs e)
        {

        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        int key = 0;
        private void BreedDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (key == 0)
            {
                MessageBox.Show("Select the Breed report to be deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from BreedTbl where BrId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Breed Report delete Successfuly");
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

            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || RemarksTb.Text == "" || CowAgeTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Update BreedTbl set HeatDate= '" + HeatDate.Value.Date + "',BreedDate='" + BreedDate.Value.Date + "',CowId=" + CowIdCb.SelectedValue.ToString() + ",CowName='" + CowNameTb.Text + "',PregDate='" + PregDate.Value.Date + "',ExpDateCalve='" + ExpDate.Value.Date + "',DateCalved='" + DateCalved.Value.Date + "',CowAge='" + CowAgeTb.Text + "',Remarks='" + RemarksTb.Text + "' where BrId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Breeding  Update Successfuly");
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

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BreedDGV_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            HeatDate.Text = BreedDGV.SelectedRows[0].Cells[1].Value.ToString();
            BreedDate.Text = BreedDGV.SelectedRows[0].Cells[2].Value.ToString();
            CowIdCb.SelectedValue = BreedDGV.SelectedRows[0].Cells[3].Value.ToString();
            CowNameTb.Text = BreedDGV.SelectedRows[0].Cells[4].Value.ToString();
            PregDate.Text = BreedDGV.SelectedRows[0].Cells[5].Value.ToString();
            ExpDate.Text = BreedDGV.SelectedRows[0].Cells[6].Value.ToString();
            DateCalved.Text = BreedDGV.SelectedRows[0].Cells[7].Value.ToString();
            CowAgeTb.Text = BreedDGV.SelectedRows[0].Cells[8].Value.ToString();
            RemarksTb.Text = BreedDGV.SelectedRows[0].Cells[9].Value.ToString();
            if (CowNameTb.Text == "")
            {
                key = 0;


            }
            else
            {
                key = Convert.ToInt32(BreedDGV.SelectedRows[0].Cells[0].Value.ToString());



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
