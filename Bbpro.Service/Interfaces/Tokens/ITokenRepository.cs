using Bbpro.Domain.Entities.Users;

namespace Bbpro.Service.Interfaces.Tokens;

public interface ITokenRepository
{
    string CreateToken(User user);
}
