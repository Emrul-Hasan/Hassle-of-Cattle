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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            UsernameTb.Text = "";
            PasswordTb.Text = "";
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ThinkPad\Documents\HassleofCattleDb.mdf;Integrated Security=True;Connect Timeout=30");
        public static string User;
        private void button1_Click(object sender, EventArgs e)
        {
            if (UsernameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter the username and password");
            }
            else
            {
                if (RoleCb.SelectedIndex > -1)
                {
                    if (RoleCb.SelectedItem.ToString() == "Admin")
                    {
                        if (UsernameTb.Text == "Admin" && PasswordTb.Text == "Admin")

                        {
                            Employees prod = new Employees();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are Admin,Enter the correct id and password");
                        }
                    }
                    else
                    {
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from EmployeeTbl where EmpName='" + UsernameTb.Text + "' and EmpPass='" + PasswordTb.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            User = UsernameTb.Text;
                            Welcome cow = new Welcome();
                            cow.Show();
                            this.Hide();
                            Con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong UserName or password");
                        }
                        Con.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Select A Role");
                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AboutUs ob = new AboutUs();
            ob.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Splash ob = new Splash();
            ob.Show();
            this.Hide();
        }
    }
}

