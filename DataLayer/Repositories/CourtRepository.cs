using DataLayer.Dtos;
using MySqlConnector;

namespace DataLayer.Repositories;

public class CourtRepository : Database, ICourtRepository
{
    public Task<List<CourtDto>?> GetAll()
    {
        List<CourtDto>? courtDtos = new List<CourtDto>();
        TaskCompletionSource<List<CourtDto>?> tcs = new TaskCompletionSource<List<CourtDto>?>();
        
        try
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * FROM `Court`", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courtDtos.Add(new CourtDto()
                            {
                                Id = reader.GetInt32("id"),
                                Double = reader.GetBoolean("double"),
                                Indoor = reader.GetBoolean("indoor"),
                                Number = reader.GetInt32("number"),
                            });
                        }

                        tcs.SetResult(courtDtos);

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
    
    public Task<CourtDto?> FindById(int id)
    {
        TaskCompletionSource<CourtDto?> tcs = new TaskCompletionSource<CourtDto?>();
        
        try
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * FROM `Court` WHERE `Id` = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CourtDto courtDto = new CourtDto()
                            {
                                Id = reader.GetInt32("id"),
                                Double = reader.GetBoolean("double"),
                                Indoor = reader.GetBoolean("indoor"),
                                Number = reader.GetInt32("number"),  
                            };
                            
                            tcs.SetResult(courtDto);

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

    public Task<bool> Create(CourtDto courtDto)
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        
        try
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("INSERT INTO `Court` (`Id`, `Number`, `Indoor`, `Double`) " +
                                                  "VALUES (NULL, @number, @indoor, @double);", conn))
                {
                    cmd.Parameters.AddWithValue("@number", courtDto.Number);
                    cmd.Parameters.AddWithValue("@indoor", courtDto.Indoor);
                    cmd.Parameters.AddWithValue("@double", courtDto.Double);
                    
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

    public Task<bool> Edit(int id, CourtDto courtDto)
    {
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        
        try
        {
            using (var conn = new MySqlConnection(ConnectionString))
            {
                conn.Open();
                ;
                using (var cmd = new MySqlCommand("UPDATE `Court` SET " +
                                                  "`Number` = @number, " +
                                                  "`Indoor` = @indoor," +
                                                  "`Double` = @double" +
                                                  " WHERE `Id` = @id;", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@number", courtDto.Number);
                    cmd.Parameters.AddWithValue("@indoor", courtDto.Indoor);
                    cmd.Parameters.AddWithValue("@double", courtDto.Double);
                    
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
                ;
                using (var cmd = new MySqlCommand("DELETE FROM `Court` WHERE `Id` = @id", conn))
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