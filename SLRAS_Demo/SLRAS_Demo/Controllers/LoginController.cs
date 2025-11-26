using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SLRAS_Demo.Application.Contracts;
using SLRAS_Demo.Helper;
using SLRAS_Demo.ViewModels;

namespace SLRAS_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
       
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenService _jwt;
        public LoginController(IConfiguration configuration,
            IJwtTokenService jwt)
        {
            _configuration = configuration;
            _jwt = jwt;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("Default"));
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "GetUserByEmail";
            cmd.Parameters.AddWithValue("@email", request.Email);
            SqlDataReader reader= await cmd.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                reader.Read();                

                byte[] storedHash = (byte[])reader["PasswordHash"];
                byte[] storedSalt = (byte[])reader["PasswordSalt"];
                bool valid = PasswordHelper.VerifyPasswordHash(request.Password, storedHash, storedSalt);
                if (!valid)
                {
                    reader.Close();
                    return Unauthorized("Invalid Password");
                }

                //UserViewModel user = new UserViewModel()
                //{
                //    Id = Convert.ToInt32(reader["Id"]),
                //    Email = Convert.ToString(reader["Email"]),
                //    FirstName = Convert.ToString(reader["FirstName"]),
                //    LastName = Convert.ToString(reader["LastName"]),
                //    MobileNumber = Convert.ToString(reader["Mobile"]),
                //    //roleViewModel=new RoleViewModel()
                //    //{
                //    //    Id=Convert.ToInt32(reader["RoleId"]),
                //    //    name = Convert.ToString(reader["RoleName"]),    
                //    //}

                //};
                //string token=_jwt.GenerateToken(user);
                reader.Close();
                return Ok("");
            }

            return Unauthorized("Invalid UserName");
            

        }
    }
}
