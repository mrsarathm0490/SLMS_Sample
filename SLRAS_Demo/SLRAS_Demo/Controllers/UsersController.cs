using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SLRAS_Demo.Application.Contracts;
using SLRAS_Demo.Helper;
using SLRAS_Demo.ViewModels;
using System.Data;

namespace SLRAS_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Users([FromBody] UserViewModel user) 
        {
            try
            {
                if(user.Id == null) 
                user=await _userService.CreateUser(user);    
                return Ok(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        } 
    }
}
