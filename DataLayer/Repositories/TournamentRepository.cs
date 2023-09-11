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
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT `Id`,`Name`,`Description`,`Price`,`MaxMembers`,`StartDateTime` FROM `Tournament`", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
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
                }
            }
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
        TaskCompletionSource<TournamentDto?> tcs = new TaskCompletionSource<TournamentDto?>();
        
        try
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT `Id`,`Name`,`Description`,`Price`,`MaxMembers`,`StartDateTime`FROM `Tournament` WHERE `Id` = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TournamentDto tournamentDto = new TournamentDto
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Description = reader.GetString("description"),
                                Price = reader.GetInt32("price"),
                                MaxMembers = reader.GetInt32("maxmembers"),
                                StartDateTime = reader.GetDateTime("startdatetime"),
                            };
                            tcs.SetResult(tournamentDto);
                            
                            return tcs.Task;
                        }
                        tcs.SetResult(null);
                        
                        return tcs.Task;
                    }
                }
            }
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
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("INSERT INTO `Tournament` (`Name`, `Description`, `Price`, `MaxMembers`, `StartDateTime`) VALUES (@name, @description, @price, @maxMembers, @startDateTime);", conn))
                {
                    cmd.Parameters.AddWithValue("@name", tournamentDto.Name);
                    cmd.Parameters.AddWithValue("@description", tournamentDto.Description);
                    cmd.Parameters.AddWithValue("@price", tournamentDto.Price);
                    cmd.Parameters.AddWithValue("@maxMembers", tournamentDto.MaxMembers);
                    cmd.Parameters.AddWithValue("@startDateTime", tournamentDto.StartDateTime);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    tcs.SetResult(rowsAffected > 0);

                    return tcs.Task;
                }
            }
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
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("UPDATE `Tournament` SET `Name` = @name, `Description` = @description,`Price` = @price,`MaxMembers` = @maxMembers,`StartDateTime` = @startDateTime WHERE `Id` = @id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", tournamentDto.Name);
                    cmd.Parameters.AddWithValue("@description", tournamentDto.Description);
                    cmd.Parameters.AddWithValue("@price", tournamentDto.Price);
                    cmd.Parameters.AddWithValue("@maxMembers", tournamentDto.MaxMembers);
                    cmd.Parameters.AddWithValue("@startDateTime", tournamentDto.StartDateTime);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    tcs.SetResult(rowsAffected > 0);

                    return tcs.Task;
                }
            }
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
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("DELETE FROM `Tournament` WHERE `Id` = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    tcs.SetResult(rowsAffected > 0);

                    return tcs.Task;
                }
            }
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