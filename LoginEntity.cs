using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManagementSystem
{
    public class LoginEntity
    {
        private readonly string connectionString = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int ID { get; set; }

        public bool InsertCustomerToLoginTable(LoginEntity loginEntity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand insertCommand = new SqlCommand(
                        "INSERT INTO login (UserName, Password, Role, Email, ID) " +
                        "VALUES (@userName, @password, @role, @email, @ID)",
                        connection);

                    insertCommand.Parameters.AddWithValue("@userName", loginEntity.UserName);
                    insertCommand.Parameters.AddWithValue("@password", loginEntity.Password);
                    insertCommand.Parameters.AddWithValue("@role", loginEntity.Role);
                    insertCommand.Parameters.AddWithValue("@email", loginEntity.Email);
                    insertCommand.Parameters.AddWithValue("@ID", loginEntity.ID);

                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting data into login table: " + ex.Message);
                return false;
            }
        }

        public static bool UpdateFields(int userId, string role, string newEmail, string newPhone)
        {
            try
            {
                string connectionString1 = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString1))
                {
                    connection.Open();

                    string updateQuery = "UPDATE Login SET ";
                    List<string> updateFields = new List<string>();

                    if (!string.IsNullOrEmpty(newEmail))
                    {
                        updateFields.Add("Email = @newEmail");
                    }

                    if (!string.IsNullOrEmpty(newPhone))
                    {
                        updateFields.Add("Phone = @newPhone");
                    }

                    if (updateFields.Count > 0)
                    {
                        updateQuery += string.Join(", ", updateFields) + " WHERE ID = @userId AND Role = @role";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

                        if (!string.IsNullOrEmpty(newEmail))
                        {
                            updateCommand.Parameters.AddWithValue("@newEmail", newEmail);
                        }

                        if (!string.IsNullOrEmpty(newPhone))
                        {
                            updateCommand.Parameters.AddWithValue("@newPhone", newPhone);
                        }

                        updateCommand.Parameters.AddWithValue("@userId", userId);
                        updateCommand.Parameters.AddWithValue("@role", role);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                    else
                    {
                        Console.WriteLine("No valid fields to update.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating email and phone: " + ex.Message);
                return false;
            }
        }


        public int GetNextAvailableUserID(string role)
        {
            int nextID = 1;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(ID), 0) FROM Login WHERE Role = @Role";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Role", role);
                    object result = command.ExecuteScalar();

                    nextID = Convert.ToInt32(result) + 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving next available user ID for role '" + role + "': " + ex.Message);
            }

            return nextID;
        }

        public string FetchPasswordByUsername(string username)
        {
            string query = @"
        SELECT PassWord COLLATE SQL_Latin1_General_CP1_CS_AS 
        FROM Login 
        WHERE UserName = @username 
        OR Email = @username 
        OR Phone = @username"; // Using COLLATE for case sensitivity

            string password = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        password = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching password from the database: " + ex.Message);
                }
            }

            return password;
        }


        public bool UpdatePassword(string username, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Login SET PassWord = @newPassword WHERE UserName = @username";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newPassword", newPassword);
                    command.Parameters.AddWithValue("@username", username);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating password: " + ex.Message);
            }
        }

        public string GetUserRole(string username, string password)
        {
            string query = @"
        SELECT Role 
        FROM Login 
        WHERE (UserName = @username OR Email = @username OR Phone = @username) 
        AND PassWord = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        string role = reader["Role"].ToString();
                        return role;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching user role from the database: " + ex.Message);
                }
            }

            return null;
        }


        public int GetUserIDByUsername(string username)
        {
            int userId = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                SELECT ID 
                FROM Login 
                WHERE UserName = @username 
                OR Email = @username 
                OR Phone = @username";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out userId))
                    {
                        return userId;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching user ID from the database: " + ex.Message);
            }
        }


        public DataTable GetAllUserData()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Login";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving user data: " + ex.Message);
            }

            return dataTable;
        }

        public bool DeleteUserByUsername(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Login WHERE UserName = @username";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user: " + ex.Message);
            }
        }

        public bool AddUser(LoginEntity loginEntity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int newId = 0;

                    if (loginEntity.Role.ToLower() == "housekeeper")
                    {
                        string getMaxHousekeeperIdQuery = "SELECT MAX(ID) FROM Login WHERE Role = 'Housekeeper'";
                        SqlCommand getMaxHousekeeperIdCommand = new SqlCommand(getMaxHousekeeperIdQuery, connection);
                        object maxHousekeeperId = getMaxHousekeeperIdCommand.ExecuteScalar();
                        newId = (maxHousekeeperId != DBNull.Value) ? Convert.ToInt32(maxHousekeeperId) + 1 : 1;
                    }
                    else if (loginEntity.Role.ToLower() == "receptionist")
                    {
                        string getMaxReceptionistIdQuery = "SELECT MAX(ID) FROM Login WHERE Role = 'Receptionist'";
                        SqlCommand getMaxReceptionistIdCommand = new SqlCommand(getMaxReceptionistIdQuery, connection);
                        object maxReceptionistId = getMaxReceptionistIdCommand.ExecuteScalar();
                        newId = (maxReceptionistId != DBNull.Value) ? Convert.ToInt32(maxReceptionistId) + 1 : 1;
                    }
                    else
                    {
                        throw new Exception("Invalid role specified");
                    }

                    if (newId != 0)
                    {
                        string query = "INSERT INTO Login (UserName, PassWord, Role, ID) VALUES (@UserName, @PassWord, @Role, @Id)";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@UserName", loginEntity.UserName);
                        command.Parameters.AddWithValue("@PassWord", loginEntity.Password);
                        command.Parameters.AddWithValue("@Role", loginEntity.Role);
                        command.Parameters.AddWithValue("@Id", newId);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                    else
                    {
                        throw new Exception("Error generating ID");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user: " + ex.Message);
            }
        }

        public bool UpdateUserField(string username, string columnName, string newValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Login SET " + columnName + " = @newValue WHERE UserName = @username";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@username", username);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user field: " + ex.Message);
            }
        }
    }
}
