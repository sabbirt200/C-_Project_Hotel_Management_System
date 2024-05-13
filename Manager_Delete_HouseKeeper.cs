using HotelManagementSystem;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class deleteHouseKeeper : Form
    {
        private readonly Panel homePanelReference;

        public deleteHouseKeeper(Panel homePanel)
        {
            InitializeComponent();
            DataTable data = HousekeeperEntity.GetHousekeeperDataFromDatabase();
            deletekeeper.DataSource = data;
            homePanelReference = homePanel;
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            HouseKeeper HouseKeeper = new HouseKeeper(homePanelReference);
            HouseKeeper.TopLevel = false;
            HouseKeeper.FormBorderStyle = FormBorderStyle.None;
            HouseKeeper.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(HouseKeeper);

            HouseKeeper.Show();
            this.Close();
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            if (deletekeeper.SelectedRows.Count > 0)
            {
                try
                {
                    int selectedIndex = deletekeeper.SelectedRows[0].Index;
                    string ID = deletekeeper.Rows[selectedIndex].Cells["HK_ID"].Value.ToString();

                    if (!string.IsNullOrEmpty(ID))
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this HouseKeeper?", "Confirmation", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            int rowsAffected = HousekeeperEntity.DeleteHousekeeper(Convert.ToInt32(ID));

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Row deleted successfully.");
                                DataTable data = HousekeeperEntity.GetHousekeeperDataFromDatabase();
                                deletekeeper.DataSource = data;
                            }
                            else
                            {
                                MessageBox.Show("No HouseKeeper were deleted.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Deletion canceled.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to retrieve ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a HouseKeeper to delete.");
            }
        }
    }
}
