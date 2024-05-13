using HotelManagementSystem;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Add_Lost_and_Found : Form
    {
        private readonly Panel homePanelReference;

        public Add_Lost_and_Found(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            Add_Lost_and_Found_Menu addLostAndFoundMenu = new Add_Lost_and_Found_Menu(homePanelReference)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(addLostAndFoundMenu);

            addLostAndFoundMenu.Show();
        }

        private void Add_Item_Bt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLostItem.Text) || string.IsNullOrEmpty(txtItemDescription.Text) || string.IsNullOrEmpty(txtCustomerID.Text))
            {
                MessageBox.Show("All fields are mandatory");
            }
            else
            {
                if (!int.TryParse(txtCustomerID.Text, out int customerID))
                {
                    MessageBox.Show("Customer ID must be an integer");
                    return;
                }

                CustomerEntity customerEntity = new CustomerEntity();
                if (!customerEntity.CustomerExists(customerID))
                {
                    MessageBox.Show("Customer ID does not exist");
                    return;
                }

                try
                {
                    string foundItemDescription = txtItemDescription.Text;
                    string lostItem = txtLostItem.Text;
                    LostAndFound lostItemDetails = new LostAndFound
                    {
                        ItemDescription = foundItemDescription,
                        Lost_Item = lostItem,
                        Customer_ID = customerID
                    };
                    bool inserted = lostItemDetails.InsertLostItem(lostItemDetails);

                    if (inserted)
                    {
                        MessageBox.Show("Lost item details added successfully");
                        txtItemDescription.Clear();
                        txtLostItem.Clear();
                        txtCustomerID.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Error inserting Lost and Found details");
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

        private void txtRoomNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                submitBT.PerformClick();
            }
        }
    }
}
