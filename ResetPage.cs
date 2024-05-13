using HotelManagementSystem;
using System;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class ResetPage : Form
    {
        private string username;
        private LoginEntity loginEntity = new LoginEntity();

        public ResetPage(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string newPassword = newPasswordTextBox.Text;
            string confirmPassword = confirmPasswordTextBox.Text;
            if (IsPasswordValid(newPassword))
            {
                if (newPassword == confirmPassword)
                {
                    bool isPasswordUpdated = loginEntity.UpdatePassword(username, newPassword);

                    if (isPasswordUpdated)
                    {
                        MessageBox.Show("Password updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Password not updated. No matching user found.");
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match!");
                }
            }
            else
            {
                MessageBox.Show("Invalid password! Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.");
            }
        }

        private bool IsPasswordValid(string password)
        {
            bool hasLower = false, hasUpper = false, hasSpecial = false, hasNumber = false;

            foreach (char c in password)
            {
                if (char.IsLower(c)) hasLower = true;
                else if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsDigit(c)) hasNumber = true;
                else if (!char.IsLetterOrDigit(c)) hasSpecial = true;
            }

            return hasLower && hasUpper && hasSpecial && hasNumber;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ForgetPass forgetpassword = new ForgetPass();
            forgetpassword.Show();
            this.Hide();
        }

        private void confirmPasswordTextBox_KeyDown_1(object sender, KeyEventArgs e)
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
    }
}
