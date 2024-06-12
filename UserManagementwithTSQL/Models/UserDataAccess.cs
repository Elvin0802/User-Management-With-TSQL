using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace UserManagementwithTSQL.Models;

public static class UserDataAccess
{
	private static string _connectionString = "Data Source=localhost;Initial Catalog=usersdb;Integrated Security=True;";

	public static List<User> GetAllUsers()
	{
		List<User> users = new();

		try
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				const string query = "SELECT [Id], [Username], [Password] FROM dbo.Table_1";

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							users.Add(new User
							{
								Id = Guid.Parse(reader.GetString(reader.GetOrdinal("Id"))),
								Username = reader.GetString(reader.GetOrdinal("Username")),
								Password = reader.GetString(reader.GetOrdinal("Password"))
							});
						}
					}
				}
			}
		}
		catch { MessageBox.Show("Error reading data into Database!"); }

		return users;
	}

	public static void SaveAllUsers(List<User> users)
	{
		try
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				foreach (var user in users)
				{
					const string query = "INSERT INTO dbo.Table_1 (Id, Username, Password) VALUES (@Id, @Username, @Password)";

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = user.Id;
						command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
						command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;

						int result = command.ExecuteNonQuery();

						if (result < 0)
							MessageBox.Show("Error inserting data into Database!");
					}
				}
			}
		}
		catch { MessageBox.Show("Error saving data into Database!"); }
	}

	public static void DeleteAllUsers()
	{
		try
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				const string query = "DELETE FROM dbo.Table_1"; // Use this if you don't need to reset identity column

				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					int result = command.ExecuteNonQuery();

					if (result < 0)
						MessageBox.Show("Error deleting data from Database!");
				}
			}
		}
		catch { MessageBox.Show($"Error deleting data from Database!"); }
	}

}
