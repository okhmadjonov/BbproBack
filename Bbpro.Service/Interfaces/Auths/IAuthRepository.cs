using Bbpro.Domain.Dto.Users;
using Bbpro.Domain.Models.Users;

namespace Bbpro.Service.Interfaces.Auths;

public interface IAuthRepository
{
    ValueTask<UserModel> Registration(UserForCreationDto user);
    Task<string> Login(LoginDto loginDto);
}
