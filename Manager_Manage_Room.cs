using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class mRoomManager : Form
    {
        private readonly RoomManagerEntity roomManagerEntity = new RoomManagerEntity();
        private readonly Panel homePanelReference;

        public mRoomManager(Panel homePanel)
        {
            InitializeComponent();
            homePanelReference = homePanel;
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    string columnName = tableManager.Columns[e.ColumnIndex].Name;
                    string newValue = Convert.ToString(tableManager.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    string roomNo = Convert.ToString(tableManager.Rows[e.RowIndex].Cells[0].Value);

                    bool updated = roomManagerEntity.UpdateRoomManagerData(columnName, newValue, roomNo);

                    MessageBox.Show(updated ? "Data updated successfully." : "Failed to update data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable data = roomManagerEntity.GetRoomManagerData();
                tableManager.DataSource = data;
                tableManager.CellEndEdit += DataGridView1_CellEndEdit;

                foreach (DataGridViewColumn column in tableManager.Columns)
                {
                    if (column.Name != "Price")
                    {
                        column.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void OpenFormInPanel(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(form);

            form.Show();
        }

        private void AddBt_Click(object sender, EventArgs e)
        {
            RoomAdd roomAdd = new RoomAdd(homePanelReference);
            OpenFormInPanel(roomAdd);
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            deleteForm deleteForm = new deleteForm(homePanelReference);
            OpenFormInPanel(deleteForm);
        }
    }
}
