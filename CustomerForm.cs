using HotelManagementSystem;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Customer : Form
    {
        private readonly Panel homePanelReference;
        private readonly CustomerEntity customerEntity = new CustomerEntity();

        public Customer(Panel homepanel)
        {
            InitializeComponent();
            homePanelReference = homepanel;
            LoadData();
        }

        private bool CustomerHasPayment(int customerId)
        {
            PaymentEntity paymentEntity = new PaymentEntity();
            DataTable paymentData = paymentEntity.GetDataFromPaymentTable();

            foreach (DataRow row in paymentData.Rows)
            {
                if (row["CustomerID"] != DBNull.Value && int.TryParse(row["CustomerID"].ToString(), out int storedCustomerId))
                {
                   
                    if (storedCustomerId == customerId)
                    {
                        return true; 
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Customer ID");
                }
            }

            return false;
        }

        private void tableManager_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int customerId = Convert.ToInt32(tableManager.Rows[e.RowIndex].Cells["ID"].Value);
                int roomNumber = Convert.ToInt32(tableManager.Rows[e.RowIndex].Cells["Room_No"].Value);

                bool customerHasPayment = CustomerHasPayment(customerId);

                if (!customerHasPayment)
                {
                    int billAmount = GetCustomerBill(customerId);

                    if (billAmount != 0)
                    {
                        Payment paymentForm = new Payment(billAmount, homePanelReference, customerId, roomNumber)
                        {
                            TopLevel = false,
                            FormBorderStyle = FormBorderStyle.None,
                            Dock = DockStyle.Fill
                        };

                        homePanelReference.Controls.Clear();
                        homePanelReference.Controls.Add(paymentForm);

                        paymentForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Error retrieving bill amount for the customer.");
                    }
                }
                else
                {
                    MessageBox.Show("Payment already made for this customer.");
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            try
            {
                DataTable dataTable = customerEntity.SearchCustomers(searchTerm);

                if (dataTable.Rows.Count == 0)
                {
                    tableManager.DataSource = null;
                }
                else
                {
                    tableManager.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                DataTable data = customerEntity.GetAllCustomers();
                tableManager.DataSource = data;

                foreach (DataGridViewColumn column in tableManager.Columns)
                {
                    column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    tableManager.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private int GetCustomerBill(int customerId)
        {
            try
            {
                return customerEntity.GetCustomerBillAmount(customerId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return 0;
            }
        }
    }
}
