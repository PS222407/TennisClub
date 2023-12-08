using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using MySqlConnector;

namespace DataLayer.Repositories;

public class TournamentRepository : Database, ITournamentRepository
{
    public List<Tournament>? GetAll()
    {
        List<Tournament> tournaments = new();

        try
        {
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new("SELECT t.Id, t.Name, t.Description, t.Price, t.MaxMembers, t.StartDateTime, t.ImageUrl, " +
                                         "c.Id AS c_Id, c.Number AS c_Number, c.Indoor AS c_Indoor, c.Double AS c_Double, " +
                                         "u.Id AS u_Id, u.UserName AS u_UserName " +
                                         "FROM Tournament AS t " +
                                         "LEFT JOIN CourtTournament AS ct ON t.Id = ct.TournamentsId " +
                                         "LEFT JOIN Court AS c ON ct.CourtsId = c.Id " +
                                         "LEFT JOIN TournamentUser AS tu ON t.Id = tu.TournamentsId " +
                                         "LEFT JOIN AspNetUsers AS u ON tu.UsersId = u.Id;", conn);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int tournamentId = reader.GetInt32("id");
                if (!tournaments.Any(t => t.Id == tournamentId))
                {
                    tournaments.Add(new Tournament
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Description = reader.GetString("description"),
                        Price = reader.GetInt32("price"),
                        MaxMembers = reader.GetInt32("maxmembers"),
                        StartDateTime = reader.GetDateTime("startdatetime"),
                        ImageUrl = reader.GetString("imageurl"),
                    });
                }

                Tournament tournament = tournaments.Find(t => t.Id == tournamentId)!;

                if (!reader.IsDBNull(reader.GetOrdinal("c_id")))
                {
                    tournament.AddCourt(new Court
                    {
                        Id = reader.GetInt32("c_id"),
                        Number = reader.GetInt32("c_number"),
                        Double = reader.GetBoolean("c_double"),
                        Indoor = reader.GetBoolean("c_indoor"),
                    });
                }

                if (!reader.IsDBNull(reader.GetOrdinal("u_id")))
                {
                    tournament.AddUser(new User
                    {
                        Id = reader.GetString("u_id"),
                        UserName = reader.GetString("u_username"),
                    });
                }
            }

            return tournaments;
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

    public Tournament? FindById(int id)
    {
        Tournament tournament = new()
        {
            Courts = new List<Court>(),
            Users = new List<User>(),
        };

        try
        {
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new("SELECT t.Id, t.Name, t.Description, t.Price, t.MaxMembers, t.StartDateTime, t.ImageUrl, " +
                                         "c.Id AS c_Id, c.Number AS c_Number, c.Indoor AS c_Indoor, c.Double AS c_Double, " +
                                         "u.Id AS u_Id, u.UserName AS u_UserName " +
                                         "FROM Tournament AS t " +
                                         "LEFT JOIN CourtTournament AS ct ON t.Id = ct.TournamentsId " +
                                         "LEFT JOIN Court AS c ON ct.CourtsId = c.Id " +
                                         "LEFT JOIN TournamentUser AS tu ON t.Id = tu.TournamentsId " +
                                         "LEFT JOIN AspNetUsers AS u ON tu.UsersId = u.Id " +
                                         "WHERE t.Id = @Id;", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using MySqlDataReader reader = cmd.ExecuteReader();
            bool firstIteration = true;

            while (reader.Read())
            {
                if (firstIteration)
                {
                    tournament.Id = reader.GetInt32("id");
                    tournament.Name = reader.GetString("name");
                    tournament.Description = reader.GetString("description");
                    tournament.Price = reader.GetInt32("price");
                    tournament.MaxMembers = reader.GetInt32("maxmembers");
                    tournament.StartDateTime = reader.GetDateTime("startdatetime");
                    tournament.ImageUrl = reader.GetString("imageurl");
                }

                if (!reader.IsDBNull(reader.GetOrdinal("c_id")))
                {
                    Court court = new()
                    {
                        Id = reader.GetInt32("c_id"),
                        Number = reader.GetInt32("c_number"),
                        Double = reader.GetBoolean("c_double"),
                        Indoor = reader.GetBoolean("c_indoor"),
                    };
                    tournament.AddCourt(court);
                }

                if (!reader.IsDBNull(reader.GetOrdinal("u_id")))
                {
                    User user = new()
                    {
                        Id = reader.GetString("u_id"),
                        UserName = reader.GetString("u_username"),
                    };
                    tournament.AddUser(user);
                }

                firstIteration = false;
            }

            return !firstIteration ? tournament : null;
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

    public bool Create(Tournament tournament)
    {
        try
        {
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new(
                "INSERT INTO `Tournament` (`Name`, `Description`, `Price`, `MaxMembers`, `StartDateTime`, `ImageUrl`) VALUES (@name, @description, @price, @maxMembers, @startDateTime, @imageUrl); SELECT LAST_INSERT_ID();",
                conn);
            cmd.Parameters.AddWithValue("@name", tournament.Name);
            cmd.Parameters.AddWithValue("@description", tournament.Description);
            cmd.Parameters.AddWithValue("@price", tournament.Price);
            cmd.Parameters.AddWithValue("@maxMembers", tournament.MaxMembers);
            cmd.Parameters.AddWithValue("@startDateTime", tournament.StartDateTime);
            cmd.Parameters.AddWithValue("@imageUrl", tournament.ImageUrl);
            object? lastInsertedId = cmd.ExecuteScalar();
            bool tournamentSuccess = lastInsertedId != null;

            bool pivotSuccess = true;
            if (tournament.CourtIds != null)
            {
                using MySqlCommand cmdPivot = new("INSERT INTO `CourtTournament` (`CourtsId`, `TournamentsId`) VALUES ", conn);
                string values = string.Join(", ", tournament.CourtIds.Select(courtId => $"(@courtid_{courtId}, @tournamentid_{courtId})"));
                cmdPivot.CommandText += values + ";";

                foreach (int courtId in tournament.CourtIds)
                {
                    cmdPivot.Parameters.AddWithValue($"@courtid_{courtId}", courtId);
                    cmdPivot.Parameters.AddWithValue($"@tournamentid_{courtId}", lastInsertedId);
                }

                if (cmdPivot.ExecuteNonQuery() <= 0)
                {
                    pivotSuccess = false;
                }
            }

            return tournamentSuccess && pivotSuccess;
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

    public bool Edit(int id, Tournament tournament)
    {
        try
        {
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new(
                "UPDATE `Tournament` SET `Name` = @name, `Description` = @description,`Price` = @price,`MaxMembers` = @maxMembers,`StartDateTime` = @startDateTime, `ImageUrl` = @imageUrl WHERE `Id` = @id;",
                conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", tournament.Name);
            cmd.Parameters.AddWithValue("@description", tournament.Description);
            cmd.Parameters.AddWithValue("@price", tournament.Price);
            cmd.Parameters.AddWithValue("@maxMembers", tournament.MaxMembers);
            cmd.Parameters.AddWithValue("@startDateTime", tournament.StartDateTime);
            cmd.Parameters.AddWithValue("@imageUrl", tournament.ImageUrl);
            bool tournamentSuccess = cmd.ExecuteNonQuery() > 0;

            using MySqlCommand cmdDeletePivot = new("DELETE FROM CourtTournament WHERE TournamentsId = @id;", conn);
            cmdDeletePivot.Parameters.AddWithValue("@id", id);
            cmdDeletePivot.ExecuteNonQuery();

            bool pivotSuccess = true;
            if (tournament.CourtIds != null)
            {
                using MySqlCommand cmdPivot = new("INSERT INTO `CourtTournament` (`CourtsId`, `TournamentsId`) VALUES ", conn);
                string values = string.Join(", ", tournament.CourtIds.Select(courtId => $"(@courtid_{courtId}, @tournamentid_{courtId})"));
                cmdPivot.CommandText += values + ";";

                foreach (int courtId in tournament.CourtIds)
                {
                    cmdPivot.Parameters.AddWithValue($"@courtid_{courtId}", courtId);
                    cmdPivot.Parameters.AddWithValue($"@tournamentid_{courtId}", id);
                }

                if (cmdPivot.ExecuteNonQuery() <= 0)
                {
                    pivotSuccess = false;
                }
            }

            return tournamentSuccess && pivotSuccess;
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
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new("DELETE FROM `Tournament` WHERE `Id` = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            using MySqlCommand cmdDeletePivot = new("DELETE FROM CourtTournament WHERE TournamentsId = @id;", conn);
            cmdDeletePivot.Parameters.AddWithValue("@id", id);
            cmdDeletePivot.ExecuteNonQuery();

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

    public StatusMessage AddUser(int tournamentId, string userId)
    {
        try
        {
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new("INSERT INTO `TournamentUser` (`UsersId`, `TournamentsId`) VALUES (@userId, @tournamentId);", conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@tournamentId", tournamentId);
            cmd.ExecuteNonQuery();

            return new StatusMessage
            {
                Success = true,
                Reason = "Successfully stored in database",
            };
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            string reason = ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry ? "U bent al ingeschreven voor dit toernooi" : "Failed to store in database";
            return new StatusMessage
            {
                Success = false,
                Reason = reason,
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return new StatusMessage
            {
                Success = false,
                Reason = "Failed to store in database",
            };
        }
    }

    public bool Exists(int id)
    {
        try
        {
            using MySqlConnection conn = new(ConnectionString);
            conn.Open();

            using MySqlCommand cmd = new("SELECT EXISTS(SELECT id FROM Tournament WHERE Id = @id) AS `exists`;", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using MySqlDataReader reader = cmd.ExecuteReader();

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