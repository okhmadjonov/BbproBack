using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Brands;
using Bbpro.Domain.Entities.Brands;
using Bbpro.Domain.Models.BrandsModel;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Brands;

public interface IBrandRepository
{
    ValueTask<IEnumerable<BrandModel>> GetAll(PaginationParams @params, Expression<Func<Brand, bool>> expression = null);
    ValueTask<BrandModel> GetAsync(Expression<Func<Brand, bool>> expression);
    ValueTask<List<BrandModel>> CreateAsync(BrandDto brandDto);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<List<BrandModel>> UpdateAsync(int id, BrandDto brandDto);
}
