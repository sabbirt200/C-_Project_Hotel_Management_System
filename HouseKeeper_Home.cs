using HotelManagementSystem;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class HouseKeeper_Home : Form
    {
        private readonly string loggedInUsername;
        readonly HousekeeperEntity HousekeeperEntity = new HousekeeperEntity();
        private readonly int userID;

        public HouseKeeper_Home(int userID)
        {
            InitializeComponent();

            this.userID = userID;
            LoadData();
            loggedInUsername = HousekeeperEntity.GetHousekeeperNameByID(userID);
            label1.Text = "Welcome " + loggedInUsername;
        }

        private void LoadData()
        {
            DataTable housekeeperDataTable = HousekeeperEntity.GetHousekeeperData(userID);
            tableManager.DataSource = housekeeperDataTable;
        }

        private void ServiceRequestBT_Click(object sender, EventArgs e)
        {
            HouseKeeper_Service houseKeeperService = new HouseKeeper_Service(homepanel);
            ConfigureFormDisplay(houseKeeperService);
        }

        private void LandF_Click(object sender, EventArgs e)
        {
            Lost_And_Found houseKeeperLostAndFound = new Lost_And_Found(homepanel);
            ConfigureFormDisplay(houseKeeperLostAndFound);
        }

        private void ConfigureFormDisplay(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            homepanel.Controls.Clear();
            homepanel.Controls.Add(form);

            form.Show();
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void HomeBT_Click(object sender, EventArgs e)
        {
            HouseKeeper_Home houseKeeperHome = new HouseKeeper_Home(userID);
            houseKeeperHome.Show();
            this.Hide();
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

        private void UpdateDataBT_Click(object sender, EventArgs e)
        {
            houseKeeperAdd houseKeeperAddForm = new houseKeeperAdd(homepanel, userID)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            homepanel.Controls.Clear();
            homepanel.Controls.Add(houseKeeperAddForm);

            houseKeeperAddForm.Show();
        }
    }
}
