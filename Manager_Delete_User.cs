using HotelManagementSystem;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class DeleteUser : Form
    {
        private Panel homePanelReference;
        private LoginEntity loginEntity = new LoginEntity();

        public DeleteUser(Panel homePanel)
        {
            InitializeComponent();
            homePanelReference = homePanel;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            DataTable data = loginEntity.GetAllUserData();
            deletekeeper.DataSource = data;
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            if (deletekeeper.SelectedRows.Count > 0)
            {
                try
                {
                    int selectedIndex = deletekeeper.SelectedRows[0].Index;
                    string userName = deletekeeper.Rows[selectedIndex].Cells["UserName"].Value.ToString();

                    if (!string.IsNullOrEmpty(userName))
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this user?", "Delete Confirmation", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            bool isDeleted = loginEntity.DeleteUserByUsername(userName);

                            if (isDeleted)
                            {
                                MessageBox.Show("User deleted successfully.");
                                DataTable data = loginEntity.GetAllUserData();
                                deletekeeper.DataSource = data;
                            }
                            else
                            {
                                MessageBox.Show("No user was deleted.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to retrieve username.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            UserTable UserTable = new UserTable(homePanelReference);
            UserTable.TopLevel = false;
            UserTable.FormBorderStyle = FormBorderStyle.None;
            UserTable.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(UserTable);

            UserTable.Show();
            Close();
        }
    }
}
