using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Brands;
using Bbpro.Domain.Entities.Brands;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.BrandsModel;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Brands;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bbpro.Service.Services.Brands;

public class BrandService : IBrandRepository
{
    private readonly IGenericRepository<Brand> _brandRepository;

    public BrandService(IGenericRepository<Brand> brandRepository)
    {
        _brandRepository = brandRepository;
    }
    public async ValueTask<List<BrandModel>> CreateAsync(BrandDto brandDto)
    {
        var brandModels = new List<BrandModel>();

        if (brandDto.ImageUrl != null && brandDto.ImageUrl.Any())
        {
            foreach (var imageUrl in brandDto.ImageUrl)
            {
                string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(imageUrl.FileName));
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageUrl.CopyToAsync(fileStream);
                }

                var subBrandEntity = new Brand
                {
                    ImageUrl = new List<string> { fileName },
                };

                var subBrandModel = new BrandModel().MapFromEntity(subBrandEntity);
                brandModels.Add(subBrandModel);

                await _brandRepository.CreateAsync(subBrandEntity);
            }
            await _brandRepository.SaveChangesAsync();
        }
        return brandModels;
    }


    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findBrand = await _brandRepository.GetAsync(f => f.Id == id);
        if (findBrand == null)
        {
            throw new BbproException(404, "brand_not_found");
        }

        foreach (var imageUrl in findBrand.ImageUrl)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await _brandRepository.DeleteAsync(findBrand.Id);
        await _brandRepository.SaveChangesAsync();
        return true;
    }


    public async ValueTask<IEnumerable<BrandModel>> GetAll(PaginationParams @params, Expression<Func<Brand, bool>> expression = null)
    {
        var brands = _brandRepository.GetAll(expression: expression, isTracking: false);
        var brandsList = await brands.ToPagedList(@params).ToListAsync();
        return brandsList.Select(e => new BrandModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<BrandModel> GetAsync(Expression<Func<Brand, bool>> expression)
    {
        var brand = await _brandRepository.GetAsync(expression, false);
        if (brand is null)
            throw new BbproException(404, "brand_not_found");
        return new BrandModel().MapFromEntity(brand);
    }
    public async ValueTask<List<BrandModel>> UpdateAsync(int id, BrandDto brandDto)
    {
        var brandModels = new List<BrandModel>();

        var existingBrand = await _brandRepository.GetAsync(br => br.Id == id);

        if (existingBrand == null)
        {
            throw new BbproException(404, "brand_not_found");
        }

        if (brandDto.ImageUrl != null && brandDto.ImageUrl.Any())
        {

            if (existingBrand.ImageUrl != null && existingBrand.ImageUrl.Any())
            {
                foreach (var oldImageUrl in existingBrand.ImageUrl.ToList())
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldImageUrl);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                existingBrand.ImageUrl.Clear();
            }

            existingBrand.ImageUrl = new List<string>();
            foreach (var imageUrl in brandDto.ImageUrl)
            {
                string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(imageUrl.FileName));
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageUrl.CopyToAsync(fileStream);
                }

                existingBrand.ImageUrl.Add(fileName);
            }
        }

        _brandRepository.Update(existingBrand);
        await _brandRepository.SaveChangesAsync();
        var updatedBrandModel = new BrandModel().MapFromEntity(existingBrand);
        brandModels.Add(updatedBrandModel);

        return brandModels;
    }
}
