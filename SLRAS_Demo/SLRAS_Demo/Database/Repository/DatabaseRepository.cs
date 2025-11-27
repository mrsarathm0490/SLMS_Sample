using Microsoft.Data.SqlClient;
using SLRAS_Demo.Database.Contract;
using System.Data;

namespace SLRAS_Demo.Database.Repository
{
    public class DatabaseRepository:IDatabaseRepository
    {
        private readonly string _connectionString;

        public DatabaseRepository(IConfiguration _configuration)
        {
            _connectionString = _configuration.GetConnectionString("Default");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, CommandType commandType, List<SqlParameter>? parameters)
        {
            try
            {
                using (var conn = GetConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }


                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
            catch
            {
                throw;
            }
            
        }

        public Task<object> ExecuteScalarAsync(string command, CommandType commandType, List<SqlParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<(SqlConnection,SqlDataReader)> ExecuteDataReaderAsync(string command, CommandType commandType, List<SqlParameter> parameters)
        {
            try
            {
                var conn = GetConnection();
                using (var cmd = new SqlCommand(command, conn))
                {
                    cmd.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }


                    await conn.OpenAsync();
                    SqlDataReader reader= await cmd.ExecuteReaderAsync();
                    return(conn, reader);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
