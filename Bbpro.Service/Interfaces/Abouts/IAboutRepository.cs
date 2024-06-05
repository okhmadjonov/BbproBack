using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.About;
using Bbpro.Domain.Entities.About;
using Bbpro.Domain.Models.AboutModel;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Abouts;

public interface IAboutRepository
{
    ValueTask<IEnumerable<AboutModel>> GetAll(PaginationParams @params, Expression<Func<About, bool>> expression = null);
    ValueTask<AboutModel> GetAsync(Expression<Func<About, bool>> expression);
    ValueTask<AboutModel> CreateAsync(AboutCreateDto aboutForCreationDTO);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<AboutModel> UpdateAsync(int id, AboutUpdateDto aboutForUpdateDTO);
}
