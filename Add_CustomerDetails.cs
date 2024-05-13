using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Add_CustomerDetails : Form
    {

        private readonly Panel homePanelReference;
        readonly RoomManagerEntity roomReq = new RoomManagerEntity();
        private readonly int userID;
        public Add_CustomerDetails(Panel homepanel, string roomNumber, int userID)
        {
            InitializeComponent();
            homePanelReference = homepanel;
            txtRoomNo.Text = roomNumber;
            txtRoomNo.ReadOnly = true;
            this.userID = userID;
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            Customer_Manage_Room Customer_Manage_Room = new Customer_Manage_Room(homePanelReference, userID);
            Customer_Manage_Room.TopLevel = false;
            Customer_Manage_Room.FormBorderStyle = FormBorderStyle.None;
            Customer_Manage_Room.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(Customer_Manage_Room);

            Customer_Manage_Room.Show();
            this.Close();
        }


        private void SubmitBT_Click(object sender, EventArgs e)
        {



            Dictionary<string, string> columnsToUpdate = new Dictionary<string, string>();

            columnsToUpdate.Add("Name", txtFirstName.Text + " " + txtLastName.Text);
            columnsToUpdate.Add("Gender", txtGender.Text);
            columnsToUpdate.Add("DOB", txtDOB.Value.ToString("yyyy-MM-dd"));
            columnsToUpdate.Add("ID_Type", txtIDType.Text);
            columnsToUpdate.Add("Contact", txtContact.Text);
            columnsToUpdate.Add("Address", txtAddress.Text);

            columnsToUpdate.Add("ID_No", txtIDNo.Text);
            columnsToUpdate.Add("Check_In_Date", txtCheckInDate.Value.ToString("yyyy-MM-dd"));
            columnsToUpdate.Add("Room_No", txtRoomNo.Text);


            int rowsAffected = CustomerEntity.UpdateCustomerFields(userID, columnsToUpdate);

            if (rowsAffected > 0)
            {
                roomReq.Status = "Pending";
                roomReq.Room_No = Int32.Parse(txtRoomNo.Text);

                bool roomStatusUpdated = roomReq.UpdateRoomStatus(roomReq);

                if (roomStatusUpdated)
                {
                    MessageBox.Show("Customer details and room status updated successfully!");
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Customer details updated successfully, but failed to update room status.");
                }

            }
            else
            {
                MessageBox.Show("Failed to update customer details.");

            }
        }
        private void ClearTextBoxes()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtGender.Text = string.Empty;
            txtDOB.Value = DateTime.Today;
            txtIDType.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtIDNo.Text = string.Empty;
            txtCheckInDate.Value = DateTime.Today;
            txtRoomNo.Text = string.Empty;
        }

        private void txtRoomNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubmitBT.PerformClick();
            }
        }
    }
}