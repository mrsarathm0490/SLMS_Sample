using SLRAS_Demo.Models;

namespace SLRAS_Demo.Database.Contract
{
    public interface IUserRepository
    {
        Task<Users> CreateUser(Users user);
    }
}
