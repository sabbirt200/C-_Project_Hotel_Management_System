using HotelManagementSystem;
using System;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class CustomerSignUp : Form
    {
        private readonly LoginEntity loginEntity = new LoginEntity();

        public CustomerSignUp()
        {
            InitializeComponent();
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

        private void BackBT_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private int GetNextAvailableCustomerID()
        {
            return loginEntity.GetNextAvailableUserID("Customer");
        }

        private void SignUpBT_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string passwordRetype = passwordRetypeTextBox.Text;

            if (password == passwordRetype)
            {
                if (IsPasswordValid(password))
                {
                    int nextID = GetNextAvailableCustomerID();

                    bool signUpSuccess = loginEntity.InsertCustomerToLoginTable(new LoginEntity
                    {
                        UserName = username,
                        Password = password,
                        Role = "Customer",
                        ID = nextID,
                        Email = email
                    });

                    if (signUpSuccess)
                    {
                        CustomerEntity CustomerEntity = new CustomerEntity
                        {
                            Email = email,
                            UserID = nextID
                        };
                        bool insertEmailSuccess = CustomerEntity.InsertEmailAndIDToCustomerTable(CustomerEntity);

                        if (insertEmailSuccess)
                        {
                            MessageBox.Show("Signup successful!");
                            ClearTextBoxes();
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert email into the customer table.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Signup failed. Please try again.");
                    }
                }
                else
                {
                    MessageBox.Show("Password does not meet complexity requirements.");
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match. Please re-enter your passwords.");
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

        private void ClearTextBoxes()
        {
            emailTextBox.Text = string.Empty;
            usernameTextBox.Text = string.Empty;
            passwordTextBox.Text = string.Empty;
            passwordRetypeTextBox.Text = string.Empty;
        }

        private void passwordRetypeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                signUpBT.PerformClick();
            }
        }
    }
}
