using HotelManagementSystem;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class UserTable : Form
    {
        private readonly Panel homePanelReference;
        private readonly LoginEntity loginEntity = new LoginEntity();

        public UserTable(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable data = loginEntity.GetAllUserData();
                tableManager.DataSource = data;

                // Make all columns readonly except "PassWord"
                foreach (DataGridViewColumn column in tableManager.Columns)
                {
                    if (column.Name != "PassWord")
                    {
                        column.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    string columnName = tableManager.Columns[e.ColumnIndex].Name;
                    string newValue = Convert.ToString(tableManager.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    string userName = Convert.ToString(tableManager.Rows[e.RowIndex].Cells["UserName"].Value);

                    bool updated = loginEntity.UpdateUserField(userName, columnName, newValue);

                    MessageBox.Show(updated ? "User data updated successfully." : "Failed to update user data.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating data: " + ex.Message);
                }
            }
        }

        private void OpenFormInPanel(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(form);

            form.Show();
        }

        private void addBt_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser(homePanelReference);
            OpenFormInPanel(addUser);
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser(homePanelReference);
            OpenFormInPanel(deleteUser);
        }
    }
}
