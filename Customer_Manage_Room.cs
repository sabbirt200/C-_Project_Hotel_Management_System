using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Customer_Manage_Room : Form
    {
        private readonly RoomManagerEntity roomManager = new RoomManagerEntity();
        private readonly Panel homePanelReference;
        private readonly int userID;

        public Customer_Manage_Room(Panel homePanel, int userID)
        {
            InitializeComponent();
            homePanelReference = homePanel;
            LoadDataIntoDataGridView();
            this.userID = userID;
        }

        private void LoadDataIntoDataGridView()
        {
            DataTable data = roomManager.GetRoomManagerData();
            if (data != null)
            {
                tableManager.DataSource = data;
            }
            else
            {
                MessageBox.Show("Failed to load data.");
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && tableManager.Columns[e.ColumnIndex].Name == "Room_No")
            {
                string roomNumber = tableManager.Rows[e.RowIndex].Cells["Room_No"].Value.ToString();
                ShowRoomForm(roomNumber);
            }
        }

        private void ShowRoomForm(string roomNumber)
        {
            Form roomForm = GetRoomFormByNumber(roomNumber);
            ShowForm(roomForm);
        }

        private Form GetRoomFormByNumber(string roomNumber)
        {
            string status = GetStatusByRoomNumber(roomNumber);

            if (status != "Approved")
            {
                Type roomType = Type.GetType($"HotelManageMentSystem._{roomNumber}");

                if (roomType != null)
                {
                    return (Form)Activator.CreateInstance(roomType, roomNumber, homePanelReference, userID);
                }
            }

            return null;
        }

        private string GetStatusByRoomNumber(string roomNumber)
        {
            string status = null;

            foreach (DataGridViewRow row in tableManager.Rows)
            {
                if (row.Cells["Room_No"].Value != null && row.Cells["Status"].Value != null)
                {
                    if (row.Cells["Room_No"].Value.ToString() == roomNumber)
                    {
                        status = row.Cells["Status"].Value.ToString();
                        break;
                    }
                }
            }

            return status;
        }

        private void ShowForm(Form roomForm)
        {
            if (roomForm != null)
            {
                roomForm.TopLevel = false;
                roomForm.FormBorderStyle = FormBorderStyle.None;
                roomForm.Dock = DockStyle.Fill;
                homePanelReference.Controls.Clear();
                homePanelReference.Controls.Add(roomForm);
                roomForm.Show();
            }
        }
    }
}
