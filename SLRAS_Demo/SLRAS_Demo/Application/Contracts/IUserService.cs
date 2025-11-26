using SLRAS_Demo.ViewModels;

namespace SLRAS_Demo.Application.Contracts
{
    public interface IUserService
    {
        public List<UserViewModel> GetUsers();
        public UserViewModel GetUser(int id);
        UserViewModel GetUserByEmailId(string emailId);
        public Task<UserViewModel> CreateUser(UserViewModel userViewModel);
        public bool DeleteUserById(int id);
    }
}
