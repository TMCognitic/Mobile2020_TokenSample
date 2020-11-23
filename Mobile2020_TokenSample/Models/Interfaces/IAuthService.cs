using Mobile2020_TokenSample.Models.Services;

namespace Mobile2020_TokenSample.Models.Interfaces
{
    public interface IAuthService
    {
        User Login(string email, string passwd);
        int Register(User user);
        bool Check(User user);
    }
}