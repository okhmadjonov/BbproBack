using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.CategoryDto;
using Bbpro.Domain.Entities.Categories;
using Bbpro.Domain.Models.Categories;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Categories;

public interface ICategoryRepository
{
    ValueTask<IEnumerable<CategoryModel>> GetAll(PaginationParams @params, Expression<Func<Category, bool>> expression = null);
    ValueTask<CategoryModel> GetAsync(Expression<Func<Category, bool>> expression);
    ValueTask<CategoryModel> CreateAsync(CategoryCreateDto categoryDto);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<CategoryModel> UpdateAsync(int id, CategoryUpdateDto categoryDto);
}
