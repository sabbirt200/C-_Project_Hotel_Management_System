using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public class LostAndFound
    {
        private readonly string connectionString = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";

        public string Found_Item { get; set; }
        public string ItemDescription { get; set; }
        public string Lost_Item { get; set; }
        public int Customer_ID { get; set; }

        public bool InsertLostItem(LostAndFound lostAndFound)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int newId = 0;
                    string getMaxIdQuery = "SELECT MAX(ID) FROM LostAndFound";
                    SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, connection);
                    object maxId = getMaxIdCommand.ExecuteScalar();
                    newId = (maxId != DBNull.Value) ? Convert.ToInt32(maxId) + 1 : 1;

                    if (newId != 0)
                    {
                        string query = "INSERT INTO LostAndFound (ItemDescription, Lost_Item, Customer_ID, ID) VALUES (@ItemDescription, @LostItem, @Customer_ID, @Id)";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@ItemDescription", lostAndFound.ItemDescription);
                        command.Parameters.AddWithValue("@LostItem", lostAndFound.Lost_Item);
                        command.Parameters.AddWithValue("@Customer_ID", lostAndFound.Customer_ID);
                        command.Parameters.AddWithValue("@Id", newId);

                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        throw new Exception("Error generating ID");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            return true;
        }

        public bool InsertFoundItem(LostAndFound lostAndFound)
        {
            try
            {
                using (SqlConnection newConnection = new SqlConnection(connectionString))
                {
                    newConnection.Open();
                    int newId = 0;
                    string getMaxIdQuery = "SELECT MAX(ID) FROM LostAndFound";
                    SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, newConnection);
                    object maxId = getMaxIdCommand.ExecuteScalar();
                    newId = (maxId != DBNull.Value) ? Convert.ToInt32(maxId) + 1 : 1;

                    string query = "INSERT INTO lostandfound (ID, ItemDescription, Found_Item) VALUES (@Id, @ItemDescription, @FoundItem)";
                    SqlCommand command = new SqlCommand(query, newConnection);

                    command.Parameters.AddWithValue("@Id", newId);
                    command.Parameters.AddWithValue("@ItemDescription", lostAndFound.ItemDescription);
                    command.Parameters.AddWithValue("@FoundItem", lostAndFound.Found_Item);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            return true;
        }

        public DataTable GetDataFromLostAndFoundTable()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection newConnection = new SqlConnection(connectionString))
                {
                    newConnection.Open();

                    string query = "SELECT * FROM [lostandfound]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, newConnection);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dataTable;
        }

        public DataTable SearchLostAndFound(string searchTerm)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT [ID], [Found_Item], [ItemDescription], [Lost_Item], [Customer_ID] FROM [lostandfound] WHERE [Found_Item] LIKE @searchTerm OR [ItemDescription] LIKE @searchTerm OR [Lost_Item] LIKE @searchTerm OR [Customer_ID] LIKE @searchTerm";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error searching: " + ex.Message);
                }
            }
            return dataTable;
        }

        public bool UpdateLostAndFoundItem(int id, string columnName, string newValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE lostandfound SET [" + columnName + "] = @newValue WHERE [ID] = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        public bool DeleteLostAndFoundItem(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM lostandfound WHERE ID = @SelectedID";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@SelectedID", id);
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
    }
}
