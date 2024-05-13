using HotelManagementSystem;
using System;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class ForgetPass : Form
    {
        private readonly LoginEntity loginEntity = new LoginEntity();

        public ForgetPass()
        {
            InitializeComponent();
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            string enteredUsername = UserNameTB.Text;
            string enteredPassword = PassTB.Text;

            if (enteredPassword.Length >= 2)
            {
                string storedPassword = loginEntity.FetchPasswordByUsername(enteredUsername);

                if (storedPassword != null && enteredPassword.Substring(0, 2) == storedPassword.Substring(0, 2))
                {
                    ResetPage resetPage = new ResetPage(enteredUsername);
                    resetPage.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Please contact the manager to reset your password.");
                }
            }
            else
            {
                MessageBox.Show("Password should be at least 2 characters long.");
            }
        }
        private void PassTB_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton.PerformClick();
            }
        }

        private void closeButtonLogin_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MinimizeBT_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Hide();
        }


    }
}
