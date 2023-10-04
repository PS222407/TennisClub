using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using MySqlConnector;

namespace DataLayer.Repositories;

public class CourtRepository : Database, ICourtRepository
{
    public List<Court>? GetAll()
    {
        List<Court> courts = new();

        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new MySqlCommand("SELECT `Id`, `Double`, `Indoor`, `Number` FROM `Court`", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                courts.Add(new Court
                {
                    Id = reader.GetInt32("id"),
                    Double = reader.GetBoolean("double"),
                    Indoor = reader.GetBoolean("indoor"),
                    Number = reader.GetInt32("number"),
                });
            }

            return courts;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }

    public Court? FindById(int id)
    {
        try
        {
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new("SELECT `Id`, `Double`, `Indoor`, `Number` FROM `Court` WHERE `Id` = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Court courtDto = new Court
                {
                    Id = reader.GetInt32("id"),
                    Double = reader.GetBoolean("double"),
                    Indoor = reader.GetBoolean("indoor"),
                    Number = reader.GetInt32("number"),
                };

                return courtDto;
            }

            return null;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }

    public bool Create(Court court)
    {
        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new MySqlCommand("INSERT INTO `Court` (`Id`, `Number`, `Indoor`, `Double`) VALUES (NULL, @number, @indoor, @double);", conn);
            cmd.Parameters.AddWithValue("@number", court.Number);
            cmd.Parameters.AddWithValue("@indoor", court.Indoor);
            cmd.Parameters.AddWithValue("@double", court.Double);

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
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

    public bool Edit(int id, Court court)
    {
        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new MySqlCommand("UPDATE `Court` SET `Number` = @number, `Indoor` = @indoor,`Double` = @double WHERE `Id` = @id;", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@number", court.Number);
            cmd.Parameters.AddWithValue("@indoor", court.Indoor);
            cmd.Parameters.AddWithValue("@double", court.Double);

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
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

    public bool Delete(int id)
    {
        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new MySqlCommand("DELETE FROM `Court` WHERE `Id` = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
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