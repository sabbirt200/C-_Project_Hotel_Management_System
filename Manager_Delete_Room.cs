using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class deleteForm : Form
    {
        private readonly Panel homePanelReference;
        private readonly RoomManagerEntity roomManagerEntity = new RoomManagerEntity();

        public deleteForm(Panel homePanel)
        {
            InitializeComponent();
            homePanelReference = homePanel;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable data = roomManagerEntity.GetRoomManagerData();
                deleterow.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            if (deleterow.SelectedRows.Count > 0)
            {
                try
                {
                    int selectedIndex = deleterow.SelectedRows[0].Index;
                    int roomNo = Convert.ToInt32(deleterow.Rows[selectedIndex].Cells["Room_No"].Value);

                    DialogResult result = MessageBox.Show("Are you sure you want to delete this Room?", "Confirmation", MessageBoxButtons.YesNoCancel);

                    if (result == DialogResult.Yes)
                    {
                        bool roomDeleted = roomManagerEntity.DeleteRoom(roomNo);

                        if (roomDeleted)
                        {
                            MessageBox.Show("Room deleted successfully.");
                            DataTable data = roomManagerEntity.GetRoomManagerData();
                            deleterow.DataSource = data;
                        }
                        else
                        {
                            MessageBox.Show("No Room were deleted.");
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a Room to delete.");
            }
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
    }
}
