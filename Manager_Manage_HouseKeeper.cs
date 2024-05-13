using HotelManagementSystem;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class HouseKeeper : Form
    {
        private readonly Panel homePanelReference;

        public HouseKeeper(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
            LoadData();
        }

        private void MakeColumnsReadOnly()
        {
            foreach (DataGridViewColumn column in HouseKeeperTable.Columns)
            {
                if (column.Name != "Salary")
                {
                    column.ReadOnly = true;
                }
            }
        }

        private void LoadData()
        {
            DataTable data = HousekeeperEntity.GetHousekeeperDataFromDatabase();
            HouseKeeperTable.DataSource = data;
            MakeColumnsReadOnly();
        }

        private void HouseKeeperTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string columnName = HouseKeeperTable.Columns[e.ColumnIndex].Name;
                string newValue = Convert.ToString(HouseKeeperTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int hkID = Convert.ToInt32(HouseKeeperTable.Rows[e.RowIndex].Cells[0].Value);

                HousekeeperEntity.UpdateHousekeeperData(hkID, columnName, newValue);
                MessageBox.Show("Data updated successfully.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            OpenDeleteHouseKeeperForm();
        }

        private void OpenDeleteHouseKeeperForm()
        {
            deleteHouseKeeper deleteHouseKeeper = new deleteHouseKeeper(homePanelReference);
            ConfigureFormAppearance(deleteHouseKeeper);
            deleteHouseKeeper.Show();
        }

        private void ConfigureFormAppearance(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(form);
        }

        private void HouseKeeperTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
