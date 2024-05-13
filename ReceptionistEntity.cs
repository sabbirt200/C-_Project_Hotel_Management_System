using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HotelManageMentSystem
{
    public class ReceptionistEntity
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";

        public string Name { get; set; }
        public int Salary { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Joining_Date { get; set; }
        public string DOB { get; set; }
        public string Shift { get; set; }

        public static string GetReceptionistNameByID(int receptionistID)
        {
            string receptionistName = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT [Name] FROM [dbo].[Receptionist] WHERE [ID] = @receptionistID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@receptionistID", receptionistID);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        receptionistName = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return receptionistName;
        }

        public static int AddShiftJoiningSalary(ReceptionistEntity receptionist)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int newId = 0;

                    // Logic to generate ID based on the role (Receptionist)
                    string getMaxReceptionistIdQuery = "SELECT MAX(ID) FROM Receptionist";
                    SqlCommand getMaxReceptionistIdCommand = new SqlCommand(getMaxReceptionistIdQuery, connection);
                    object maxReceptionistId = getMaxReceptionistIdCommand.ExecuteScalar();
                    newId = (maxReceptionistId != DBNull.Value) ? Convert.ToInt32(maxReceptionistId) + 1 : 1;

                    if (newId != 0)
                    {
                        string query = "INSERT INTO Receptionist (Shift, Joining_Date, Salary, ID) VALUES (@shift, @joiningDate, @salary, @Id)";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@shift", receptionist.Shift);
                        command.Parameters.AddWithValue("@joiningDate", receptionist.Joining_Date);
                        command.Parameters.AddWithValue("@salary", receptionist.Salary);
                        command.Parameters.AddWithValue("@Id", newId);

                        rowsAffected = command.ExecuteNonQuery();
                    }
                    else
                    {
                        throw new Exception("Error generating ID");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return rowsAffected;
        }

        public DataTable GetReceptionistDataFromDatabase(int ID)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM [Hotel_Management_System].[dbo].[Receptionist] WHERE [ID] = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", ID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return dataTable;
        }

        public static DataTable GetReceptionistDataFromDatabase()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM [dbo].[Receptionist]";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return dataTable;
        }

        public static bool UpdateReceptionistData(int id, string columnName, string newValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE [Receptionist] SET " + columnName + " = @newValue WHERE [ID] = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return false;
            }
        }
        public static int UpdateReceptionistFields(int userId, Dictionary<string, string> columnsToUpdate)
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
                    string query = $"UPDATE [Receptionist] SET {setClause} WHERE ID = @userId";

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

        public static int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - birthDate.Year;

            if (birthDate.Date > currentDate.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public static int DeleteReceptionistByID(string ID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM [dbo].[Receptionist] WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", ID);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }

            return rowsAffected;
        }
    }
}
