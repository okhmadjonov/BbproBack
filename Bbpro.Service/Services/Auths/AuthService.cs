using Bbpro.Domain.Dto.Users;
using Bbpro.Domain.Entities.Users;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.Users;
using Bbpro.Infrastructure.Extentions;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Interfaces.Auths;
using Bbpro.Service.Interfaces.Tokens;

namespace Bbpro.Service.Services.Auths;

internal sealed class AuthService : IAuthRepository
{
    private readonly IGenericRepository<User> _genericRepository;
    private readonly ITokenRepository _tokenGenerator;

    public AuthService(IGenericRepository<User> genericRepository, ITokenRepository tokenGenerator)
    {
        _genericRepository = genericRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        if (loginDto.Email != null)
        {
            var user = await _genericRepository.GetAsync(u => u.Email == loginDto.Email);

            if (user != null)
            {
                bool verify = Verify(loginDto.Password, user.Password);

                if (verify)
                {
                    return _tokenGenerator.CreateToken(user);
                }
                else
                {
                    throw new BbproException(401, "incorrect_password");
                }
            }
            else
            {
                throw new BbproException(404, "user_not_found");
            }
        }
        throw new BbproException(404, "user_not_found");
    }

    public async ValueTask<UserModel> Registration(UserForCreationDto user)
    {
        var existingUser = await _genericRepository.GetAsync(u => u.Email == user.Email);

        if (existingUser == null)
        {
            string passwordHash = user.Password.Encrypt();

            User newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                Phonenumber = user.Phonenumber,
                Password = passwordHash
            };

            var registeredUser = await _genericRepository.CreateAsync(newUser);
            await _genericRepository.SaveChangesAsync();
            return new UserModel().MapFromEntity(registeredUser);
        }
        else
        {
            throw new BbproException(401, "user_already_exist");
        }
    }
    public static bool Verify(string password, string hashedPassword)
    {
        string hashedInputPassword = password.Encrypt();
        return string.Equals(hashedInputPassword, hashedPassword);
    }
}


