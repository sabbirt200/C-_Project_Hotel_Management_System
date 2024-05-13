using System.Data;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public partial class Manager_Payment : Form
    {
        private readonly PaymentEntity paymentEntity = new PaymentEntity();

        public Manager_Payment(Panel homepanel)
        {
            InitializeComponent();
            DisplayPaymentData();
        }

        private void DisplayPaymentData()
        {
            DataTable data = paymentEntity.GetDataFromPaymentTable();
            dataGridViewPayment.DataSource = data;
        }
    }
}
