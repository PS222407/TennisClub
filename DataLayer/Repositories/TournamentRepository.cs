using DataLayer.Dtos;
using MySqlConnector;

namespace DataLayer.Repositories;

public class TournamentRepository : Database, ITournamentRepository
{
    public Task<List<TournamentDto>?> GetAll()
    {
        List<TournamentDto> tournamentDtos = new List<TournamentDto>();
        TaskCompletionSource<List<TournamentDto>?> tcs = new TaskCompletionSource<List<TournamentDto>?>();

        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new MySqlCommand("SELECT `Id`,`Name`,`Description`,`Price`,`MaxMembers`,`StartDateTime` FROM `Tournament`", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tournamentDtos.Add(new TournamentDto
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description"),
                    Price = reader.GetInt32("price"),
                    MaxMembers = reader.GetInt32("maxmembers"),
                    StartDateTime = reader.GetDateTime("startdatetime"),
                });
            }

            tcs.SetResult(tournamentDtos);

            return tcs.Task;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            tcs.SetResult(null);
            return tcs.Task;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            tcs.SetResult(null);
            return tcs.Task;
        }
    }

    public Task<TournamentDto?> FindById(int id)
    {
        TournamentDto tournamentDto = new TournamentDto
        {
            Courts = new List<CourtDto>()
        };
        TaskCompletionSource<TournamentDto?> tcs = new TaskCompletionSource<TournamentDto?>();

        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            
            using var cmd = new MySqlCommand("SELECT t.Id, t.Name, t.Description, t.Price, t.MaxMembers, t.StartDateTime, c.Id AS c_Id, c.Number AS c_Number, c.Indoor AS c_Indoor, c.Double AS c_Double " +
                                             "FROM Tournament AS t " +
                                             "LEFT JOIN CourtTournament AS ct ON t.Id = ct.TournamentsId " +
                                             "LEFT JOIN Court AS c ON ct.CourtsId = c.Id " +
                                             "WHERE t.Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = cmd.ExecuteReader();
            bool firstIteration = true;
            while (reader.Read())
            {
                if (firstIteration)
                {
                    tournamentDto.Id = reader.GetInt32("id");
                    tournamentDto.Name = reader.GetString("name");
                    tournamentDto.Description = reader.GetString("description");
                    tournamentDto.Price = reader.GetInt32("price");
                    tournamentDto.MaxMembers = reader.GetInt32("maxmembers");
                    tournamentDto.StartDateTime = reader.GetDateTime("startdatetime");
                }

                if (!reader.IsDBNull(reader.GetOrdinal("c_id")))
                {
                    CourtDto courtDto = new CourtDto
                    {
                        Id = reader.GetInt32("c_id"),
                        Number = reader.GetInt32("c_number"),
                        Double = reader.GetBoolean("c_double"),
                        Indoor = reader.GetBoolean("c_indoor"),
                    };
                    tournamentDto.Courts.Add(courtDto);
                }
                firstIteration = false;
            }

            if (!firstIteration)
            {
                tcs.SetResult(tournamentDto);
                return tcs.Task;
            }

            tcs.SetResult(null);

            return tcs.Task;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            tcs.SetResult(null);
            return tcs.Task;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            tcs.SetResult(null);
            return tcs.Task;
        }
    }

    public Task<bool> Create(TournamentDto tournamentDto)
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new MySqlCommand("INSERT INTO `Tournament` (`Name`, `Description`, `Price`, `MaxMembers`, `StartDateTime`) VALUES (@name, @description, @price, @maxMembers, @startDateTime); SELECT LAST_INSERT_ID();", conn);
            cmd.Parameters.AddWithValue("@name", tournamentDto.Name);
            cmd.Parameters.AddWithValue("@description", tournamentDto.Description);
            cmd.Parameters.AddWithValue("@price", tournamentDto.Price);
            cmd.Parameters.AddWithValue("@maxMembers", tournamentDto.MaxMembers);
            cmd.Parameters.AddWithValue("@startDateTime", tournamentDto.StartDateTime);
            object? lastInsertedId = cmd.ExecuteScalar();
            bool tournamentSuccess = lastInsertedId != null;

            bool pivotSuccess = true;
            if (tournamentDto.CourtIds != null)
            {
                using var cmdPivot = new MySqlCommand("INSERT INTO `CourtTournament` (`CourtsId`, `TournamentsId`) VALUES (@courtsid, @tournamentsid);", conn);
                foreach (int courtId in tournamentDto.CourtIds)
                {
                    cmdPivot.Parameters.Clear();
                    cmdPivot.Parameters.AddWithValue("@courtsid", courtId);
                    cmdPivot.Parameters.AddWithValue("@tournamentsid", lastInsertedId);

                    if (cmdPivot.ExecuteNonQuery() <= 0)
                    {
                        pivotSuccess = false;
                    }
                }
            }

            tcs.SetResult(tournamentSuccess && pivotSuccess);

            return tcs.Task;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            tcs.SetResult(false);
            return tcs.Task;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            tcs.SetResult(false);
            return tcs.Task;
        }
    }

    public Task<bool> Edit(int id, TournamentDto tournamentDto)
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new MySqlCommand("UPDATE `Tournament` SET `Name` = @name, `Description` = @description,`Price` = @price,`MaxMembers` = @maxMembers,`StartDateTime` = @startDateTime WHERE `Id` = @id;", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", tournamentDto.Name);
            cmd.Parameters.AddWithValue("@description", tournamentDto.Description);
            cmd.Parameters.AddWithValue("@price", tournamentDto.Price);
            cmd.Parameters.AddWithValue("@maxMembers", tournamentDto.MaxMembers);
            cmd.Parameters.AddWithValue("@startDateTime", tournamentDto.StartDateTime);
            bool tournamentSuccess = cmd.ExecuteNonQuery() > 0;
            
            using var cmdDeletePivot = new MySqlCommand("DELETE FROM CourtTournament WHERE TournamentsId = @id;", conn);
            cmdDeletePivot.Parameters.AddWithValue("@id", id);
            int rowsAffectedDelete = cmdDeletePivot.ExecuteNonQuery();
            // TODO: can be 0 if already hasn't related items
            bool deletePivotSuccess = rowsAffectedDelete > 0;
            
            bool pivotSuccess = true;
            if (tournamentDto.CourtIds != null)
            {
                using var cmdPivot = new MySqlCommand("INSERT INTO `CourtTournament` (`CourtsId`, `TournamentsId`) VALUES (@courtsid, @tournamentsid);", conn);
                foreach (int courtId in tournamentDto.CourtIds)
                {
                    cmdPivot.Parameters.Clear();
                    cmdPivot.Parameters.AddWithValue("@courtsid", courtId);
                    cmdPivot.Parameters.AddWithValue("@tournamentsid", id);
            
                    if (cmdPivot.ExecuteNonQuery() <= 0)
                    {
                        pivotSuccess = false;
                    }
                }
            }
            
            tcs.SetResult(tournamentSuccess && deletePivotSuccess && pivotSuccess);

            return tcs.Task;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            tcs.SetResult(false);
            return tcs.Task;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            tcs.SetResult(false);
            return tcs.Task;
        }
    }

    public Task<bool> Delete(int id)
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        try
        {
            using var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            using var cmd = new MySqlCommand("DELETE FROM `Tournament` WHERE `Id` = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            
            // TODO: not tested yet
            using var cmdDeletePivot = new MySqlCommand("DELETE FROM CourtTournament WHERE TournamentsId = @id;", conn);
            cmdDeletePivot.Parameters.AddWithValue("@id", id);
            int rowsAffectedDelete = cmdDeletePivot.ExecuteNonQuery();
            bool deletePivotSuccess = rowsAffectedDelete > 0;

            int rowsAffected = cmd.ExecuteNonQuery();
            tcs.SetResult(rowsAffected > 0);

            return tcs.Task;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
            tcs.SetResult(false);
            return tcs.Task;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            tcs.SetResult(false);
            return tcs.Task;
        }
    }
}