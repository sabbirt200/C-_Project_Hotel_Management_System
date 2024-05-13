using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HotelManageMentSystem
{
    public class CustomerEntity
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";

        public string Email { get; set; }
        public int UserID { get; set; }

        public CustomerEntity() { }

        public DataTable GetCustomertDataFromDatabase(int ID)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM customer WHERE ID = @ID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", ID);

                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving customer Data : " + ex.Message);
            }

            return dataTable;
        }

        // Calculate age based on Date of Birth
        private static int CalculateAge(DateTime dob)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - dob.Year;
            if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day))
            {
                age--;
            }
            return age;
        }

        public static int UpdateCustomerFields(int userId, Dictionary<string, string> columnsToUpdate)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (columnsToUpdate.ContainsKey("DOB"))
                    {
                        DateTime dob = DateTime.Parse(columnsToUpdate["DOB"]);
                        int age = CalculateAge(dob);
                        columnsToUpdate["Age"] = age.ToString();
                    }

                    string setClause = string.Join(", ", columnsToUpdate.Select(kv => $"{kv.Key} = @{kv.Key}"));
                    string query = $"UPDATE [customer] SET {setClause} WHERE ID = @userId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userId", userId);

                    foreach (var column in columnsToUpdate)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return rowsAffected;
        }

        // Get customer name by ID
        public string GetCustomerNameByID(int customerId)
        {
            string customerName = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT [Name] FROM [customer] WHERE [ID] = @customerId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@customerId", customerId);

                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        customerName = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving customer name: " + ex.Message);
            }

            return customerName;
        }

        // Insert email and ID into the customer table
        public bool InsertEmailAndIDToCustomerTable(CustomerEntity customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand insertCommand = new SqlCommand(
                        "INSERT INTO customer (Email, ID) VALUES (@email, @id)", connection);

                    insertCommand.Parameters.AddWithValue("@email", customer.Email);
                    insertCommand.Parameters.AddWithValue("@id", customer.UserID);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting email and ID into customer table: " + ex.Message);
                return false;
            }
        }

        // Search customers based on a search term
        public DataTable SearchCustomers(string searchTerm)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT [ID], [Name], [Gender], [ID_Type], [ID_No], [DOB], [Address], [Room_No], [Contact] FROM [customer] WHERE [Name] LIKE @searchTerm OR [Gender] LIKE @searchTerm OR [ID_Type] LIKE @searchTerm OR [ID_No] LIKE @searchTerm OR [DOB] LIKE @searchTerm OR [Address] LIKE @searchTerm OR [Room_No] LIKE @searchTerm OR [Contact] LIKE @searchTerm";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error searching customers: " + ex.Message);
                }
            }

            return dataTable;
        }

        // Get all customers from the database
        public DataTable GetAllCustomers()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT [ID], [Name], [Gender], [ID_Type], [ID_No], [DOB], [Address], [Room_No], [Contact] FROM [customer]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving all customers: " + ex.Message);
                }
            }

            return dataTable;
        }

        // Check if a customer exists based on the ID
        public bool CustomerExists(int customerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM customer WHERE ID = @CustomerId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Get the bill amount for a customer based on the room price and stay duration
        public int GetCustomerBillAmount(int customerId)
        {
            int billAmount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Room_No, Check_In_Date FROM Customer WHERE ID = @customerId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@customerId", customerId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int roomId = reader.GetInt32(reader.GetOrdinal("Room_No"));
                        DateTime checkInDate = reader.GetDateTime(reader.GetOrdinal("Check_In_Date"));

                        DateTime checkOutDate = DateTime.Now;

                        int numberOfDays = (checkOutDate - checkInDate).Days;

                        RoomManagerEntity roomManager = new RoomManagerEntity();
                        decimal roomPrice = roomManager.GetRoomPriceByRoomNumber(roomId);

                        billAmount = (int)(roomPrice * numberOfDays);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return billAmount;
        }

        // Update the checkout date for a customer
        public bool UpdateCustomerCheckoutDate(int customerId, DateTime newCheckoutDate, int billAmount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE customer SET Check_Out_Date = @newCheckoutDate, Bill = @billAmount WHERE ID = @customerId";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@newCheckoutDate", newCheckoutDate);
                    updateCommand.Parameters.AddWithValue("@customerId", customerId);
                    updateCommand.Parameters.AddWithValue("@billAmount", billAmount);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating checkout date and inserting bill: " + ex.Message);
                return false;
            }
        }

    }
}
