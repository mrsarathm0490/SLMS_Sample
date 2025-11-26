using Microsoft.Data.SqlClient;
using System.Data;

namespace SLRAS_Demo.Database.Contract
{
    public interface IDatabaseRepository
    {
        Task<int> ExecuteNonQueryAsync(string command, CommandType commandType, List<SqlParameter>? parameters);
        Task<object> ExecuteScalarAsync(string command, CommandType commandType, List<SqlParameter> parameters);
        Task<DataTable> ExecuteDataReaderAsync(string command, CommandType commandType, List<SqlParameter> parameters);
    }
}
