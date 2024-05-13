using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Receptionist : Form
    {
        private readonly Panel homePanelReference;

        public Receptionist(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
            LoadData();
        }

        private void LoadData()
        {
            DataTable data = GetReceptionistDataFromDatabase();
            ReceptionistTable.DataSource = data;
            MakeColumnsReadOnly();
        }

        public DataTable GetReceptionistDataFromDatabase()
        {
            return ReceptionistEntity.GetReceptionistDataFromDatabase();
        }

        private void ReceptionistTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string columnName = ReceptionistTable.Columns[e.ColumnIndex].Name;
                string newValue = Convert.ToString(ReceptionistTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int ID = Convert.ToInt32(ReceptionistTable.Rows[e.RowIndex].Cells[0].Value);

                ReceptionistEntity.UpdateReceptionistData(ID, columnName, newValue);
                MessageBox.Show("Data updated successfully.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MakeColumnsReadOnly()
        {
            foreach (DataGridViewColumn column in ReceptionistTable.Columns)
            {
                if (column.Name != "Salary")
                {
                    column.ReadOnly = true;
                }
            }
        }

        private void DeleteBT_Click_1(object sender, EventArgs e)
        {
            OpenDeleteReceptionistForm();
        }

        private void OpenDeleteReceptionistForm()
        {
            deleteRecep deleteRecep = new deleteRecep(homePanelReference);
            ConfigureFormAppearance(deleteRecep);
            deleteRecep.Show();
        }

        private void ConfigureFormAppearance(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(form);
        }
    }
}
