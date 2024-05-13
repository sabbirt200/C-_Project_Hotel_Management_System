using HotelManagementSystem;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class houseKeeperAdd : Form
    {
        private readonly Panel homePanelReference;
        private readonly int userID;

        public houseKeeperAdd(Panel homepanel, int userID)
        {
            InitializeComponent();
            homePanelReference = homepanel;
            this.userID = userID;
        }

        private void ClearTextBoxes()
        {
            txtName.Text = "";
            txtGender.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtIDNo.Text = "";
            txtDOB.Text = "";
            txtIDType.Text = "";
            txtAddress.Text = "";
        }

        private void textBoxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateBT_Click(sender, e);
            }
        }

        private void UpdateBT_Click(object sender, EventArgs e)
        {
            int userId = userID;

            Dictionary<string, string> columnsToUpdate = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(txtName.Text))
                columnsToUpdate.Add("HK_Name", txtName.Text);

            if (!string.IsNullOrEmpty(txtGender.Text))
                columnsToUpdate.Add("Gender", txtGender.Text);

            if (txtDOB.Value != null)
                columnsToUpdate.Add("DOB", txtDOB.Value.ToString("yyyy-MM-dd"));

            if (!string.IsNullOrEmpty(txtIDType.Text))
                columnsToUpdate.Add("ID_Type", txtIDType.Text);

            if (!string.IsNullOrEmpty(txtIDNo.Text))
            {
                if (int.TryParse(txtIDNo.Text, out int idNumber))
                {
                    columnsToUpdate.Add("ID_No", idNumber.ToString());
                }
                else
                {
                    MessageBox.Show("Please enter a valid integer for ID number");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtEmail.Text))
                columnsToUpdate.Add("Email", txtEmail.Text);

            if (!string.IsNullOrEmpty(txtPhone.Text))
            {
                if (int.TryParse(txtPhone.Text, out int phoneNumber))
                {
                    columnsToUpdate.Add("Phone", phoneNumber.ToString());
                }
                else
                {
                    MessageBox.Show("Please enter a valid integer for Phone number");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtAddress.Text))
                columnsToUpdate.Add("Address", txtAddress.Text);

            if (columnsToUpdate.Count > 0)
            {
                if (!string.IsNullOrEmpty(txtEmail.Text) || !string.IsNullOrEmpty(txtPhone.Text))
                {
                    LoginEntity.UpdateFields(userId, "HouseKeeper", txtEmail.Text, txtPhone.Text);
                }

                int rowsAffected = HousekeeperEntity.UpdateHousekeeperFields(userId, columnsToUpdate);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update successful");
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Update failed");
                }
            }
            else
            {
                MessageBox.Show("No fields to update");
            }
        }
    }
}
