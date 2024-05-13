using HotelManagementSystem;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Add_Found_Item : Form
    {
        private readonly Panel homePanelReference;

        public Add_Found_Item(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            Add_Lost_and_Found_Menu addLostAndFoundMenu = new Add_Lost_and_Found_Menu(homePanelReference);
            addLostAndFoundMenu.TopLevel = false;
            addLostAndFoundMenu.FormBorderStyle = FormBorderStyle.None;
            addLostAndFoundMenu.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(addLostAndFoundMenu);

            addLostAndFoundMenu.Show();
        }

        private void submitBT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFoundItem.Text) || string.IsNullOrEmpty(txtItemDescription.Text))
            {
                MessageBox.Show("All fields are mandatory");
            }
            else
            {
                try
                {
                    string foundItemDescription = txtItemDescription.Text;
                    string foundItem = txtFoundItem.Text;

                    LostAndFound foundItemDetails = new LostAndFound();

                    foundItemDetails.ItemDescription = foundItemDescription;
                    foundItemDetails.Found_Item = foundItem;

                    bool inserted = foundItemDetails.InsertFoundItem(foundItemDetails);

                    if (inserted)
                    {
                        MessageBox.Show("Found item details added successfully");
                        txtItemDescription.Clear();
                        txtFoundItem.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Error inserting found item details");
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("An entry with the same details already exists. Please enter different details.");
                    }
                    else
                    {
                        MessageBox.Show("SQL Error: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void txtItemDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submitBT.PerformClick();
            }
        }
    }
}
