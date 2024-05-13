using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Invoice : Form
    {
        private readonly int customerId;
        private readonly Panel homePanelReference;

        public Invoice(Panel panelReference, int customerId)
        {
            InitializeComponent();
            homePanelReference = panelReference;
            this.customerId = customerId;
        }

        private void PrintBT_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Invoice Printed Successfully");
            OpenForm(new Customer(homePanelReference));
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            PaymentEntity paymentEntity = new PaymentEntity();
            CustomerEntity customerEntity = new CustomerEntity();

            try
            {
                DataTable paymentTable = paymentEntity.GetPaymentDetailsByCustomerID(customerId);

                if (paymentTable.Rows.Count > 0)
                {
                    DataRow row = paymentTable.Rows[0];
                    txtPaymentID.Text = row["PaymentID"].ToString();
                    txtTotalBill.Text = row["TotalBill"].ToString();
                    txtPaymentMethod.Text = row["PaymentMethod"].ToString();
                    txtReceivedAmount.Text = row["ReceivedAmount"].ToString();
                    txtReturnedAmount.Text = row["ReturnedAmount"].ToString();
                    txtCardType.Text = row["CardType"].ToString();

                    int customerIdFromPayment = Convert.ToInt32(row["CustomerID"]);
                    string customerName = customerEntity.GetCustomerNameByID(customerIdFromPayment);

                    txtCustomerName.Text = customerName;
                }
                else
                {
                    MessageBox.Show("No payment details available for the customer.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void OpenForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            homePanelReference.Controls.Clear();
            homePanelReference.Controls.Add(form);

            form.Show();
        }

        private void BackBT_Click(object sender, EventArgs e)
        {
            OpenForm(new Customer(homePanelReference));
        }
    }
}
