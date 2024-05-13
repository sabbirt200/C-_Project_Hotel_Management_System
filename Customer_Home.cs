using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Customer_Home : Form
    {
        private readonly string loggedInUsername;
        private readonly int userID;
        private readonly CustomerEntity CustomerEntity = new CustomerEntity();

        public Customer_Home(int userID)
        {
            InitializeComponent();
            this.userID = userID;
            LoadData();
            loggedInUsername = CustomerEntity.GetCustomerNameByID(userID);
            label1.Text = "Welcome " + loggedInUsername;
        }

        private void LoadData()
        {
            DataTable data = CustomerEntity.GetCustomertDataFromDatabase(userID);
            tableManager.DataSource = data;
        }

        private void RoomReserveBT_Click(object sender, EventArgs e)
        {
            Customer_Manage_Room Customer_Manage_Room = new Customer_Manage_Room(homepanel, userID)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            homepanel.Controls.Clear();
            homepanel.Controls.Add(Customer_Manage_Room);

            Customer_Manage_Room.Show();
        }

        private void UpdateBT_Click(object sender, EventArgs e)
        {
            Update_Customer_Data Update_Customer_Data = new Update_Customer_Data(homepanel, userID)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            homepanel.Controls.Clear();
            homepanel.Controls.Add(Update_Customer_Data);

            Update_Customer_Data.Show();
        }

        private void LogOutBT_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void MinimizeBT_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void HomeBT_Click(object sender, EventArgs e)
        {
            Customer_Home Customer_Home = new Customer_Home(userID);
            Customer_Home.Show();
            this.Hide();
        }
    }
}
