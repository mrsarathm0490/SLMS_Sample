using SLRAS_Demo.ViewModels;

namespace SLRAS_Demo.Application.Contracts
{
    public interface IJwtTokenService
    {
        string GenerateToken(UserViewModel user);
    }
}
