using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace HassleofCattle
{
    public partial class Employees : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Employees()
        {
            InitializeComponent();
            BindGridView();
            ResetControl();
          
        }

        private void Employees_Load(object sender, EventArgs e)
        {

        }

     //SqlConnection Con= new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ThinkPad\Documents\HassleofCattleDb.mdf;Integrated Security=True;Connect Timeout=30");



       
        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            //throw new NotImplementedException();
            pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
            return ms.GetBuffer();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection Con = new SqlConnection(cs);
            string query = "Insert EmployeeTbl values(@EmpName,@EmpDob,@Gender,@Phone,@Address,@EmpPass,@Picture)";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@EmpId",EmpId.Text);
            cmd.Parameters.AddWithValue("@EmpName", EmpNameTb.Text);
            cmd.Parameters.AddWithValue("@EmpDob", DOB.Value.Date);
            cmd.Parameters.AddWithValue("@Gender", GenderCb.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Phone", PhoneTb.Text);
            cmd.Parameters.AddWithValue("@Address", AddressTb.Text);
            cmd.Parameters.AddWithValue("@EmpPass", EmpPassTb.Text);
            cmd.Parameters.AddWithValue("@Picture", SavePhoto());




            Con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a>0)
            {
                MessageBox.Show("Data Insert");
                BindGridView();
                ResetControl();

            }
            else
            {
                MessageBox.Show("Data not Insert");
            }
           

        }
        void BindGridView()
        {
            SqlConnection Con = new SqlConnection(cs);
            string query = "Select * from EmployeeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            DataTable data = new DataTable();
            sda.Fill(data);
            EmpDGV.DataSource = data;
            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)EmpDGV.Columns[7];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;
            EmpDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EmpDGV.RowTemplate.Height = 70;

        }
    
        private void EmpDGV_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

         
     
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        void ResetControl()
        {
            EmpNameTb.Clear();
            GenderCb.SelectedIndex = -1;
            PhoneTb.Clear();
            AddressTb.Clear();
            EmpPassTb.Clear();
            pictureBox2.Image = Properties.Resources.Upload_Photo;


        }
        private void button4_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection(cs);
            string query = "Delete from EmployeeTbl where EmpId=@EmpId";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@EmpId", EmpId.Text);
   



            Con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Delete");
                BindGridView();
                ResetControl();

            }
            else
            {
                MessageBox.Show("Data not Delete");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection Con = new SqlConnection(cs);
            string query = "Update EmployeeTbl set EmpName=@EmpName,EmpDob=@EmpDob,Gender=@Gender,Phone=@Phone,Address=@Address,EmpPass=@EmpPass,Picture=@Picture where EmpId=@EmpId";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@EmpId", EmpId.Text);
            cmd.Parameters.AddWithValue("@EmpName", EmpNameTb.Text);
            cmd.Parameters.AddWithValue("@EmpDob", DOB.Value.Date);
            cmd.Parameters.AddWithValue("@Gender", GenderCb.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Phone", PhoneTb.Text);
            cmd.Parameters.AddWithValue("@Address", AddressTb.Text);
            cmd.Parameters.AddWithValue("@EmpPass", EmpPassTb.Text);
            cmd.Parameters.AddWithValue("@Picture", SavePhoto());




            Con.Open();
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data update");
                BindGridView();
                ResetControl();

            }
            else
            {
                MessageBox.Show("Data not updated");
            }

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login ob = new Login();
            ob.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select your photo";
            ofd.Filter = "ALL IMAGE FILE (*.*)|*.*";
            //ofd.ShowDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(ofd.FileName);
            }
        }

        private void EmpDGV_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            EmpId.Text = EmpDGV.SelectedRows[0].Cells[0].Value.ToString();

            EmpNameTb.Text = EmpDGV.SelectedRows[0].Cells[1].Value.ToString();
            DOB.Text = EmpDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenderCb.SelectedItem = EmpDGV.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = EmpDGV.SelectedRows[0].Cells[4].Value.ToString();
             AddressTb.Text = EmpDGV.SelectedRows[0].Cells[5].Value.ToString();
            EmpPassTb.Text = EmpDGV.SelectedRows[0].Cells[6].Value.ToString();
            pictureBox2.Image = GetPhoto((byte[])EmpDGV.SelectedRows[0].Cells[7].Value);


        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
           Login ob = new Login();
           ob.Show();
           this.Hide();
        }
    }
}
