using BusinessLogicLayer.Interfaces.Repositories;
using MySqlConnector;

namespace DataLayer.Repositories;

public class UserRepository : Database, IUserRepository
{
    public bool Exists(string id)
    {
        try
        {
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new("SELECT EXISTS(SELECT id FROM aspnetusers WHERE Id = @id) AS `exists`;", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                return reader.GetBoolean("exists");
            }

            return false;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return false;
        }
    }
}