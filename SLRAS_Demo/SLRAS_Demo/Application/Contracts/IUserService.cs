using SLRAS_Demo.ViewModels;

namespace SLRAS_Demo.Application.Contracts
{
    public interface IUserService
    {
        List<UserViewModel> GetUsers();
        UserViewModel GetUser(int id);
        Task<(UserViewModel, byte[]?, byte[]?)> GetUserByEmailId(string emailId);
        Task<UserViewModel> CreateUser(UserViewModel userViewModel);
        bool DeleteUserById(int id);
    }
}
