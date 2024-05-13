using HotelManagementSystem;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class AddUser : Form
    {
        private readonly Panel homePanelReference;

        public AddUser(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            UserTable roomManager1 = new UserTable(homePanelReference);
            roomManager1.TopLevel = false;
            roomManager1.FormBorderStyle = FormBorderStyle.None;
            roomManager1.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(roomManager1);

            roomManager1.Show();
            this.Close();
        }

        private void addUserBT_Click(object sender, EventArgs e)
        {
            try
            {
                string UserName = txtUserName.Text;
                string PassWord = txtPassWord.Text;
                string Role = txtRole.Text;

                if (!IsPasswordValid(PassWord))
                {
                    MessageBox.Show("Password should contain at least one lowercase letter, one uppercase letter, one special character, and a number.");
                    return;
                }

                if (!int.TryParse(txtSalary.Text, out int salary))
                {
                    MessageBox.Show("Please enter a valid integer for the salary.");
                    return;
                }

                LoginEntity loginEntity = new LoginEntity();
                loginEntity.UserName = UserName;
                loginEntity.Password = PassWord;
                loginEntity.Role = Role;

                if (Role.ToLower() == "housekeeper")
                {
                    HousekeeperEntity housekeeper = new HousekeeperEntity();
                    housekeeper.Shift = txtShift.Text;
                    housekeeper.Joining_Date = txtJoiningDate.Text;
                    housekeeper.Salary = salary;

                    bool userAdded = loginEntity.AddUser(loginEntity);

                    if (userAdded)
                    {
                        int rowsAffected = HousekeeperEntity.AddShiftJoiningSalary(housekeeper);

                        if (rowsAffected == 1)
                        {
                            MessageBox.Show("User and housekeeper details added successfully");
                            txtUserName.Clear();
                            txtPassWord.Clear();
                            txtRole.SelectedIndex = -1;
                            txtSalary.Clear();
                            txtShift.SelectedIndex = -1;
                        }
                        else
                        {
                            MessageBox.Show("Error inserting housekeeper details");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error inserting user details");
                    }
                }
                else if (Role.ToLower() == "receptionist")
                {
                    ReceptionistEntity receptionistEntity = new ReceptionistEntity();
                    receptionistEntity.Shift = txtShift.Text;
                    receptionistEntity.Joining_Date = txtJoiningDate.Text;
                    receptionistEntity.Salary = salary;

                    bool userAdded = loginEntity.AddUser(loginEntity);

                    if (userAdded)
                    {
                        int rowsAffected = ReceptionistEntity.AddShiftJoiningSalary(receptionistEntity);

                        if (rowsAffected == 1)
                        {
                            MessageBox.Show("User added successfully");
                            txtUserName.Clear();
                            txtPassWord.Clear();
                            txtRole.SelectedIndex = -1;
                            txtSalary.Clear();
                            txtShift.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error inserting user details");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid role specified");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.");
                }
                else
                {
                    MessageBox.Show("An SQL error occurred: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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

        private void txtShift_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addUserBT_Click(sender, e);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
