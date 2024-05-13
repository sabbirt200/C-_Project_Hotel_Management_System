using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class deleteRecep : Form
    {
        private readonly Panel homePanelReference;

        public deleteRecep(Panel homePanel)
        {
            InitializeComponent();
            homePanelReference = homePanel;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            DataTable data = ReceptionistEntity.GetReceptionistDataFromDatabase();
            deletekeeper.DataSource = data;
        }

        private void BackBT_Click_1(object sender, EventArgs e)
        {
            Receptionist Receptionist = new Receptionist(homePanelReference);
            Receptionist.TopLevel = false;
            Receptionist.FormBorderStyle = FormBorderStyle.None;
            Receptionist.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(Receptionist);

            Receptionist.Show();
            this.Close();
        }

        private void DeleteBT_Click(object sender, EventArgs e)
        {
            if (deletekeeper.SelectedRows.Count > 0)
            {
                try
                {
                    int selectedIndex = deletekeeper.SelectedRows[0].Index;
                    string ID = deletekeeper.Rows[selectedIndex].Cells["ID"].Value.ToString();

                    if (!string.IsNullOrEmpty(ID))
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this Receptionist?", "Delete Confirmation", MessageBoxButtons.YesNo);

                        if (dialogResult == DialogResult.Yes)
                        {
                            int rowsAffected = ReceptionistEntity.DeleteReceptionistByID(ID);

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Receptionist deleted successfully.");
                                RefreshDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("No Receptionist were deleted.");
                            }
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
                MessageBox.Show("Please select a Receptionist to delete.");
            }
        }
    }
}
