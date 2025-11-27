using Microsoft.Data.SqlClient;
using SLRAS_Demo.Database.Contract;
using SLRAS_Demo.Models;
using System.Data;

namespace SLRAS_Demo.Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private IDatabaseRepository _databaseRepository;
        public UserRepository(IDatabaseRepository databaseRepository) { 
           _databaseRepository = databaseRepository;
        }
        public async Task<Users> CreateUser(Users user)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {                    
                     new SqlParameter("@emailId", user.Email), 
                     new SqlParameter("@passwordHash", user.Passwordhash),
                     new SqlParameter("@passwordSalt", user.PasswordSalt),
                     new SqlParameter("@FirstName", user.FirstName),
                     new SqlParameter("@LastName", user.LastName),
                     new SqlParameter("@mobileNumber", user.MobileNumber),
                     new SqlParameter("@roleId",string.Join(",", user.Roles.Select(r => r.Id)))
                };

                SqlParameter outputIdParam = new SqlParameter("@userId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                parameters.Add(outputIdParam);
                int affectedRows = await _databaseRepository.ExecuteNonQueryAsync("CreateUser",CommandType.StoredProcedure,parameters);
                if (affectedRows > 0)
                {
                    user.Id = (int)outputIdParam.Value;
                }
                return user;

            }
            catch
            {
                throw;
            }
        }

        public async Task<Users?> GetUserByEmail(string email)
        {
            try
            {
                Users user = null;
                var parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@email",email)
                };
                var(conn,reader)= await _databaseRepository.ExecuteDataReaderAsync("GetUserByEmail", CommandType.StoredProcedure, parameters);
                if (reader.HasRows)
                {
                    reader.Read();
                    user=new Users() { 
                        Email = reader["Email"].ToString(),
                        Passwordhash = (byte[])reader["PasswordHash"],
                        PasswordSalt = (byte[])reader["PasswordSalt"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        MobileNumber= reader["Mobile"].ToString(),
                        Roles=new List<Roles>()
                    };
                    reader.Close();
                }
                conn.Close();
                return user;
            }
            catch
            {
                throw;
            }

        }
    }
}
