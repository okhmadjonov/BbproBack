using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Solutions;
using Bbpro.Domain.Entities.Solutions;
using Bbpro.Domain.Models.PaginationParams;
using Bbpro.Domain.Models.Solutions;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Solutions;

public interface ISolutionRepository
{
    ValueTask<IEnumerable<SolutionModel>> GetAll(PaginationParams @params, Expression<Func<Solution, bool>> expression = null);
    ValueTask<SolutionModel> GetAsync(Expression<Func<Solution, bool>> expression);
    ValueTask<SolutionModel> CreateAsync(SolutionCreateDto solutionCreateDTO);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<SolutionModel> UpdateAsync(int id, SolutionUpdateDto solutionUpdateDTO);
    ValueTask<PagedResult<SolutionFilterCategoryModel>> GetSolutionsByCategoryId(PaginationParams @params, int categoryId);

}
