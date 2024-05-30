using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Users;
using Bbpro.Domain.Entities.Users;
using Bbpro.Domain.Models.Users;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Users;

public interface IUserRepository
{
    ValueTask<IEnumerable<UserModel>> GetAll(PaginationParams @params, Expression<Func<User, bool>> expression = null);
    ValueTask<UserModel> GetAsync(Expression<Func<User, bool>> expression);
    ValueTask<UserModel> CreateAsync(UserForCreationDto userForCreationDTO);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<UserModel> UpdateAsync(int id, UserForCreationDto userForUpdateDTO);
}

