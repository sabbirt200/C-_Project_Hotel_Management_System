using System;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2ProgressBar1.Value < 100)
            {
                guna2ProgressBar1.Value += 1;
                labelProgress.Text = guna2ProgressBar1.Value + " %";
            }
            else
            {
                timer1.Stop();
                Login loginForm = new Login();
                loginForm.Show();
                this.Hide();
            }
        }
    }
}
