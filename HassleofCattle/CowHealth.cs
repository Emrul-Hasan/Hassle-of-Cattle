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
    public partial class CowHealth : Form
    {
        public CowHealth()
        {
            InitializeComponent();
            FillCowId();
            populate();
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

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            MilkProduction ob = new MilkProduction();
            ob.Show();
            this.Hide();


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
            String query = "Select * from HealthTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            HealthDGV.DataSource = ds.Tables[0];
            HealthDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            HealthDGV.RowTemplate.Height = 50;
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
            }
            Con.Close();

        }

        private void CowHealth_Load(object sender, EventArgs e)
        {

        }

        private void CowIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCowName();
        }
        private void Clear()
        {
            CowNameTb.Text = "";
            EventTb.Text = "";
            CostTb.Text = "";
            DiagnosisTb.Text = "";
            VetNameTb.Text = "";
            TreatmentTb.Text = "";
            key = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || EventTb.Text == "" || CostTb.Text == "" || VetNameTb.Text == "" || DiagnosisTb.Text == "" || TreatmentTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Insert into HealthTbl values(" + CowIdCb.SelectedValue.ToString() + ",'" + CowNameTb.Text + "','" + Date.Value.Date + "','" + EventTb.Text + "','" + DiagnosisTb.Text + "','" + TreatmentTb.Text + "','" + CostTb.Text+ "','" + VetNameTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Health isssue Successfuly");
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
        int key = 0;
        private void HealthDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            

        }

        private void button3_Click(object sender, EventArgs e)
        {


            if (key == 0)
            {
                MessageBox.Show("Select the report to be deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "delete from HealthTbl where RepId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Report delete Successfuly");
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
            if (CowIdCb.SelectedIndex == -1 || CowNameTb.Text == "" || EventTb.Text == "" || CostTb.Text == "" || VetNameTb.Text == "" || DiagnosisTb.Text == "" || TreatmentTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "Update HealthTbl set CowId=" + CowIdCb.SelectedValue.ToString() +",cowName='" + CowNameTb.Text + "',RepDate='" + Date.Value.Date + "',Event='" + EventTb.Text + "',Diagnosis='" + DiagnosisTb.Text + "',Treatment='" + TreatmentTb.Text +"',Cost='"+CostTb.Text +"',VetName='"+VetNameTb.Text+"' where RepId="+ key +";";
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

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HealthDGV_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            CowIdCb.Text = HealthDGV.SelectedRows[0].Cells[1].Value.ToString();
            CowNameTb.Text = HealthDGV.SelectedRows[0].Cells[2].Value.ToString();
            Date.Text = HealthDGV.SelectedRows[0].Cells[3].Value.ToString();
            EventTb.Text = HealthDGV.SelectedRows[0].Cells[4].Value.ToString();
            DiagnosisTb.Text = HealthDGV.SelectedRows[0].Cells[5].Value.ToString();
            TreatmentTb.Text = HealthDGV.SelectedRows[0].Cells[6].Value.ToString();
            CostTb.Text = HealthDGV.SelectedRows[0].Cells[7].Value.ToString();
            VetNameTb.Text = HealthDGV.SelectedRows[0].Cells[8].Value.ToString();
            if (CowNameTb.Text == "")
            {
                key = 0;


            }
            else
            {
                key = Convert.ToInt32(HealthDGV.SelectedRows[0].Cells[0].Value.ToString());



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
