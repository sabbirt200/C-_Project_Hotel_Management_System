using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HotelManageMentSystem
{
    public class RoomManagerEntity
    {
        private readonly string connectionString = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";

        public int Room_No { get; set; }
        public string Room_Type { get; set; }
        public int Price { get; set; }
        public string Room_Availability { get; set; }
        public int Room_Capacity { get; set; }
        public int Telephone_Num { get; set; }
        public string Status { get; set; }

        public DataTable GetRoomManagerData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM RoomManager";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dataTable;
        }

        public bool UpdateRoomAvailability(RoomManagerEntity roomManagerEntity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE RoomManager SET Room_Availability = @RoomAvailability, Status = @Room_Status WHERE Room_No = @RoomID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RoomAvailability", roomManagerEntity.Room_Availability);
                    command.Parameters.AddWithValue("@Room_Status", roomManagerEntity.Status);
                    command.Parameters.AddWithValue("@RoomID", roomManagerEntity.Room_No);
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating availability: " + ex.Message);
                return false;
            }
        }

        public bool UpdateRoomStatus(RoomManagerEntity roomManagerEntity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE RoomManager SET Status = @Status, Room_Availability = " +
                                   "@RoomAvailability WHERE Room_No = @RoomID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Status", roomManagerEntity.Status);
                    command.Parameters.AddWithValue("@RoomID", roomManagerEntity.Room_No);

                    string roomAvailability = roomManagerEntity.Status == "Approved" ? "Booked" : "Available";
                    command.Parameters.AddWithValue("@RoomAvailability", roomAvailability);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating status and availability: " + ex.Message);
                return false;
            }
        }

        public bool UpdateRoomManagerData(string columnName, string newValue, string roomNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"UPDATE RoomManager SET {columnName} = @newValue WHERE Room_No = @Room_No";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@Room_No", roomNo);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetRoomPriceByRoomNumber(int roomNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Price FROM RoomManager WHERE Room_No = @RoomNo";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RoomNo", roomNumber);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching room price: " + ex.Message);
                return 0;
            }
        }

        public bool AddRoomToDatabase(RoomManagerEntity roomManagerEntity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO RoomManager(Room_No, Room_Type, Price, Room_Availability, Room_Capacity, Telephone_Num) " +
                      "VALUES (@roomNo, @roomType, @price, @roomAvailability, @roomCapacity, @telephoneNo)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@roomNo", roomManagerEntity.Room_No);
                    command.Parameters.AddWithValue("@roomType", roomManagerEntity.Room_Type);
                    command.Parameters.AddWithValue("@price", roomManagerEntity.Price);
                    command.Parameters.AddWithValue("@roomAvailability", roomManagerEntity.Room_Availability);
                    command.Parameters.AddWithValue("@roomCapacity", roomManagerEntity.Room_Capacity);
                    command.Parameters.AddWithValue("@telephoneNo", roomManagerEntity.Telephone_Num);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RoomExists(int roomNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM RoomManager WHERE Room_No = @RoomNo";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RoomNo", roomNumber);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRoom(int roomNo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM RoomManager WHERE Room_No = @Room_No";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Room_No", roomNo);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
