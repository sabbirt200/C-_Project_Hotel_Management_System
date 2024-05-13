using System;
using System.Data;
using System.Data.SqlClient;

namespace HotelManageMentSystem
{
    public class RoomServiceEntity
    {
        private readonly string connectionString = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";

        public int Room_No { get; set; }
        public string ITEM { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }

        public RoomServiceEntity() { }

        public bool InsertRoomService(RoomServiceEntity roomServiceEntity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int newId = 0;
                    string getMaxIdQuery = "SELECT MAX(ID) FROM RoomService";
                    SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, connection);
                    object maxId = getMaxIdCommand.ExecuteScalar();
                    newId = (maxId != DBNull.Value) ? Convert.ToInt32(maxId) + 1 : 1;

                    string insertQuery = "INSERT INTO RoomService (ID, Room_No, Item, Note, Status) VALUES (@Id, @RoomNo, @Item, @Note, @Status)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    insertCommand.Parameters.AddWithValue("@Id", newId);
                    insertCommand.Parameters.AddWithValue("@RoomNo", roomServiceEntity.Room_No);
                    insertCommand.Parameters.AddWithValue("@Item", roomServiceEntity.ITEM);
                    insertCommand.Parameters.AddWithValue("@Note", roomServiceEntity.Note);
                    insertCommand.Parameters.AddWithValue("@Status", roomServiceEntity.Status);

                    insertCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new Exception("An entry with the same details already exists. Please enter different details.");
                }
                else
                {
                    throw new Exception("SQL Error: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            return true;
        }

        public DataTable GetRoomServiceData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT [ID], [Room_No], [ITEM], [Note], [Status] FROM [RoomService]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching room service data: " + ex.Message);
            }

            return dataTable;
        }

        public void UpdateRoomServiceStatus(int id, string newStatus)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE RoomService SET Status = @status WHERE ID = @id";
                    SqlCommand updateCommand = new SqlCommand(query, connection);

                    updateCommand.Parameters.AddWithValue("@status", newStatus);
                    updateCommand.Parameters.AddWithValue("@id", id);

                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating room service status: " + ex.Message);
            }
        }

        public bool DeleteRoomService(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM RoomService WHERE ID = @id";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                    deleteCommand.Parameters.AddWithValue("@id", id);

                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting room service: " + ex.Message);
            }
        }
    }
}
