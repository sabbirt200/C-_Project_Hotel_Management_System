using HotelManagementSystem;
using System;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Login : Form
    {
        private readonly LoginEntity loginEntity = new LoginEntity();

        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string UserName, PassWord;
            UserName = UserNameTB.Text;
            PassWord = PassTB.Text;
            string storedPassword = loginEntity.FetchPasswordByUsername(UserName);
            bool passwordsMatchCase = PassWord.Equals(storedPassword, StringComparison.Ordinal);

            if (passwordsMatchCase)
            {
                try
                {
                    string role = loginEntity.GetUserRole(UserName, PassWord);

                    if (role != null)
                    {
                        switch (role)
                        {
                            case "Manager":
                                this.Hide();
                                ManagerHome managerHomeForm = new ManagerHome();
                                managerHomeForm.Show();
                                break;
                            case "Receptionist":
                                this.Hide();
                                int userID = loginEntity.GetUserIDByUsername(UserName);
                                Receptionist_Home receptionistHomeForm = new Receptionist_Home(userID);
                                receptionistHomeForm.Show();
                                break;
                            case "HouseKeeper":
                                this.Hide();
                                int userIDH = loginEntity.GetUserIDByUsername(UserName);
                                HouseKeeper_Home housekeeperHomeForm = new HouseKeeper_Home(userIDH);
                                housekeeperHomeForm.Show();
                                break;
                            case "Customer":
                                this.Hide();
                                int userIDC = loginEntity.GetUserIDByUsername(UserName);
                                Customer_Home Customer_Home = new Customer_Home(userIDC);
                                Customer_Home.Show();
                                break;
                            default:
                                MessageBox.Show("Invalid role detected");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Incorrect Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ForgetPassBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ForgetPass forgetPassForm = new ForgetPass();
            forgetPassForm.Show();
        }

        private void PassTB_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginButton.PerformClick();
            }
        }

        private void PassTB_Enter(object sender, EventArgs e)
        {
            PassTB.UseSystemPasswordChar = true;
        }

        private void pictureBox3_MouseDown_1(object sender, MouseEventArgs e)
        {
            PassTB.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            PassTB.UseSystemPasswordChar = true;
        }

        private void createAccount_Click(object sender, EventArgs e)
        {
            CustomerSignUp CustomerSignUp = new CustomerSignUp();
            CustomerSignUp.Show();
            this.Hide();
        }
    }
}
