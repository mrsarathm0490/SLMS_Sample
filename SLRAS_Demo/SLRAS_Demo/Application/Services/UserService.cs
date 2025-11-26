using SLRAS_Demo.Application.Contracts;
using SLRAS_Demo.Database.Contract;
using SLRAS_Demo.Helper;
using SLRAS_Demo.Models;
using SLRAS_Demo.ViewModels;

namespace SLRAS_Demo.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) { 
           _userRepository = userRepository;
        }
        public async Task<UserViewModel> CreateUser(UserViewModel userViewModel)
        {
            try
            {
                string password = PasswordHelper.GeneratePassword();
                var (hash, salt) = PasswordHelper.CreatePasswordHash(password);

                var roles = userViewModel.roleViewModel.Select(r => new Roles
                {
                    Id = r.Id,
                    Name = r.name
                }).ToList();
                      
                Users user = new Users()
                {
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    MobileNumber = userViewModel.MobileNumber,
                    Passwordhash=hash,
                    PasswordSalt=salt,
                    Roles=roles
                };
                user=await _userRepository.CreateUser(user);
                 userViewModel.Id = user.Id;
                return userViewModel;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetUserByEmailId(string emailId)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
