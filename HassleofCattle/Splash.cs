using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HassleofCattle
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            MyProgress.Value = startpoint;
            if(MyProgress.Value == 100)
            {
                MyProgress.Value = 0;
                timer1.Stop();
                Login log = new Login();
                this.Hide();
                log.Show();
            }

        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void MyProgress_Click(object sender, EventArgs e)
        {

        }
    }
}
