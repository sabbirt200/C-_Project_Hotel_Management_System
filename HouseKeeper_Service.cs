using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class HouseKeeper_Service : Form
    {
        private readonly Panel homePanelReference;
        private readonly RoomServiceEntity roomServiceEntity = new RoomServiceEntity();

        public HouseKeeper_Service(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
            tableManager.CellValueChanged += tableManager_CellValueChanged;
            LoadData();
        }

        public void LoadData()
        {
            DataTable data = roomServiceEntity.GetRoomServiceData();
            tableManager.DataSource = data;

            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Status",
                Name = "StatusColumn",
                DataSource = GetStatusOptions(),
                DataPropertyName = "Status"
            };
            tableManager.Columns.Add(comboBoxColumn);

            foreach (DataGridViewColumn column in tableManager.Columns)
            {
                if (column.Name != "StatusColumn")
                {
                    column.ReadOnly = true;
                }
            }

            if (tableManager.Columns.Contains("Note"))
            {
                DataGridViewColumn column = tableManager.Columns["Note"];
                column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                tableManager.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }
        }

        private List<string> GetStatusOptions()
        {
            return new List<string> {
                "Pending",
                "In Progress",
                "Completed"
            };
        }

        private void tableManager_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = tableManager.Columns[e.ColumnIndex].Name;

                if (columnName == "StatusColumn")
                {
                    DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)tableManager.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string newStatus = comboBoxCell.Value.ToString();
                    int id = Convert.ToInt32(tableManager.Rows[e.RowIndex].Cells["ID"].Value);
                    roomServiceEntity.UpdateRoomServiceStatus(id, newStatus);
                }
            }
        }
    }
}
