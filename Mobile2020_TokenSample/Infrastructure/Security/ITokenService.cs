using Mobile2020_TokenSample.Models.Entities;

namespace Mobile2020_TokenSample.Infrastructure.Security
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        User ValidateToken(string token);
    }
}