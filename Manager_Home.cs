using System;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class ManagerHome : Form
    {
        public ManagerHome()
        {
            InitializeComponent();
        }

        private void ManageRoomBT_Click(object sender, EventArgs e)
        {
            OpenForm(new mRoomManager(homepanel));
        }

        private void closeButtonLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MinimizeBT_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void UserBt_Click(object sender, EventArgs e)
        {
            OpenForm(new UserTable(homepanel));
        }

        private void HouseKeeper_Click(object sender, EventArgs e)
        {
            OpenForm(new HouseKeeper(homepanel));
        }

        private void Receptionist_Click(object sender, EventArgs e)
        {
            OpenForm(new Receptionist(homepanel));
        }

        private void HomeBT_Click(object sender, EventArgs e)
        {
            new ManagerHome().Show();
            this.Hide();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            Hide();
            new Login().Show();
        }

        private void paymentBT_Click(object sender, EventArgs e)
        {
            OpenForm(new Manager_Payment(homepanel));
        }

        private void OpenForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            homepanel.Controls.Clear();
            homepanel.Controls.Add(form);

            form.Show();
        }
    }
}
