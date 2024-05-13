using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelManageMentSystem
{
    public class PaymentEntity
    {
        readonly string connectionString = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";

        public int TotalBill { get; set; }
        public string PaymentMethod { get; set; }
        public int ReceivedAmount { get; set; }
        public int ReturnedAmount { get; set; }
        public string CardType { get; set; }
        public int CustomerID { get; set; }

        public DataTable GetPaymentDetailsByCustomerID(int customerId)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM [payment] WHERE [CustomerID] = @customerId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@customerId", customerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving payment details: " + ex.Message);
            }

            return dataTable;
        }

        public bool InsertPaymentDetails(PaymentEntity payment)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO [payment] (TotalBill, PaymentMethod, ReceivedAmount, ReturnedAmount, CardType, CustomerID)
                             VALUES (@totalBill, @paymentMethod, @receivedAmount, @returnedAmount, @cardType, @customerID)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@totalBill", payment.TotalBill);
                    command.Parameters.AddWithValue("@paymentMethod", payment.PaymentMethod);
                    command.Parameters.AddWithValue("@receivedAmount", payment.ReceivedAmount);
                    command.Parameters.AddWithValue("@returnedAmount", payment.ReturnedAmount);
                    command.Parameters.AddWithValue("@cardType", payment.CardType);
                    command.Parameters.AddWithValue("@customerID", payment.CustomerID);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting payment details: " + ex.Message);
                return false;
            }
        }


        public DataTable GetDataFromPaymentTable()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM [payment]";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }
    }
}
