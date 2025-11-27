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

        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwt;
        public LoginController(IUserService userService, IConfiguration configuration,
            IJwtTokenService jwt)
        {
             _userService= userService;
            _jwt = jwt;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            try
            {
                var (user,storedHash,storedSalt)=await _userService.GetUserByEmailId(request.Email);
                if (user == null || storedHash==null || storedSalt==null)
                {
                    return Unauthorized("No User Found");
                }
                bool valid = PasswordHelper.VerifyPasswordHash(request.Password, storedHash, storedSalt);
                if (!valid)
                {

                    return Unauthorized("Invalid Password");
                }

                string token=_jwt.GenerateToken(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                throw;
            }        
          
        }           
    }
}
