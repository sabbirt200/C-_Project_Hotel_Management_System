using System;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class RoomAdd : Form
    {
        private RoomManagerEntity roomManagerEntity = new RoomManagerEntity();
        private Panel homePanelReference;

        public RoomAdd(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
            txtAvailability.Text = "Available";
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            mRoomManager roomManager1 = new mRoomManager(homePanelReference);
            roomManager1.TopLevel = false;
            roomManager1.FormBorderStyle = FormBorderStyle.None;
            roomManager1.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(roomManager1);

            roomManager1.Show();
            this.Close();
        }

        private void SubmitBT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNo.Text) ||
                string.IsNullOrWhiteSpace(txtCapacity.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtRoomType.Text) ||
                string.IsNullOrWhiteSpace(txtTelephoneNum.Text))
            {
                MessageBox.Show("All fields are mandatory");
            }
            else
            {
                try
                {
                    int roomNo, price, roomCapacity, telephoneNo;

                    if (!int.TryParse(txtRoomNo.Text, out roomNo) || !int.TryParse(txtCapacity.Text, out roomCapacity) || !int.TryParse(txtPrice.Text, out price) || !int.TryParse(txtTelephoneNum.Text, out telephoneNo))
                    {
                        MessageBox.Show("Please enter valid integer values in the number fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string roomType = txtRoomType.Text;
                    string roomAvailability = txtAvailability.Text;

                    roomManagerEntity.Room_No = roomNo;
                    roomManagerEntity.Room_Type = roomType;
                    roomManagerEntity.Price = price;
                    roomManagerEntity.Room_Availability = roomAvailability;
                    roomManagerEntity.Room_Capacity = roomCapacity;
                    roomManagerEntity.Telephone_Num = telephoneNo;

                    bool roomAdded = roomManagerEntity.AddRoomToDatabase(roomManagerEntity);

                    if (roomAdded)
                    {
                        MessageBox.Show("Room added successfully");

                        // Clear text boxes
                        txtRoomNo.Clear();
                        txtRoomType.SelectedIndex = -1;
                        txtPrice.Clear();
                        txtAvailability.Clear();
                        txtCapacity.Clear();
                        txtTelephoneNum.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Error adding room");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void txtTelephoneNum_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubmitBT_Click(sender, e);
            }
        }
    }
}
