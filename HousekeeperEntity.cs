using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace HotelManagementSystem
{
    public class HousekeeperEntity
    {
        private static readonly string connectionString = @"Data Source=DESKTOP-E8KTCSJ\SQLEXPRESS01;Initial Catalog=Hotel_Management_System;Integrated Security=True";

        public string HK_Name { get; set; }
        public int Salary { get; set; }
        public string Gender { get; set; }
        public int Phone { get; set; }
        public string Joining_Date { get; set; }
        public string DOB { get; set; }
        public string Shift { get; set; }

        public static int UpdateHousekeeperFields(int userId, Dictionary<string, string> columnsToUpdate)
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
                    string query = $"UPDATE [housekeeper] SET {setClause} WHERE HK_ID = @userId";

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

        public static DataTable GetHousekeeperData(int userID)
        {
            DataTable housekeeperDataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM [housekeeper] WHERE [HK_ID] = @userID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);
                        SqlDataReader reader = command.ExecuteReader();
                        housekeeperDataTable.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return housekeeperDataTable;
        }

        public string GetHousekeeperNameByID(int ID)
        {
            string housekeeperName = string.Empty;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT [HK_Name] FROM [housekeeper] WHERE [HK_ID] = @ID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", ID);

                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        housekeeperName = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving housekeeper name: " + ex.Message);
            }

            return housekeeperName;
        }

        public static int AddShiftJoiningSalary(HousekeeperEntity housekeeper)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    int newId = 0;

                    string getMaxHousekeeperIdQuery = "SELECT MAX(HK_ID) FROM housekeeper";
                    SqlCommand getMaxHousekeeperIdCommand = new SqlCommand(getMaxHousekeeperIdQuery, connection);
                    object maxHousekeeperId = getMaxHousekeeperIdCommand.ExecuteScalar();
                    newId = (maxHousekeeperId != DBNull.Value) ? Convert.ToInt32(maxHousekeeperId) + 1 : 1;

                    if (newId != 0)
                    {
                        string query = "INSERT INTO Housekeeper (Shift, Joining_Date, Salary, HK_ID) VALUES (@shift, @joiningDate, @salary, @Id)";
                        SqlCommand command = new SqlCommand(query, connection);

                        command.Parameters.AddWithValue("@shift", housekeeper.Shift);
                        command.Parameters.AddWithValue("@joiningDate", housekeeper.Joining_Date);
                        command.Parameters.AddWithValue("@salary", housekeeper.Salary);
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

        public static DataTable GetHousekeeperDataFromDatabase()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM [dbo].[housekeeper]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching housekeeper data: " + ex.Message);
            }

            return dataTable;
        }

        public static void UpdateHousekeeperData(int hkID, string columnName, string newValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"UPDATE [housekeeper] SET {columnName} = @newValue WHERE [HK_ID] = @hkID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newValue", newValue);
                        command.Parameters.AddWithValue("@hkID", hkID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }

        public static int DeleteHousekeeper(int hkID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM [housekeeper] WHERE [HK_ID] = @hkID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@hkID", hkID);
                        rowsAffected = command.ExecuteNonQuery();
                    }
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
