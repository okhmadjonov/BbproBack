using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.CategoryDto;
using Bbpro.Domain.Entities.Categories;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.Categories;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Categories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bbpro.Service.Services.Categories;

public class CategoryService : ICategoryRepository
{
    private readonly IGenericRepository<Category> _categoryRepository;
    public CategoryService(IGenericRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async ValueTask<CategoryModel> CreateAsync(CategoryCreateDto categoryDto)
    {
        if (categoryDto is null)
        {
            throw new BbproException(400, "category_cannot_be_empty");
        }
        var addCategory = new Category
        {
            Title = categoryDto.Title,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        var createdCategory = await _categoryRepository.CreateAsync(addCategory);
        await _categoryRepository.SaveChangesAsync();
        return new CategoryModel().MapFromEntity(createdCategory);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findCategory = await _categoryRepository.GetAsync(p => p.Id == id);
        if (findCategory is null)
        {
            throw new BbproException(404, "category_not_found");
        }

        await _categoryRepository.DeleteAsync(id);
        await _categoryRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<CategoryModel>> GetAll(PaginationParams @params, Expression<Func<Category, bool>> expression = null)
    {
        var categoriesQuery = _categoryRepository.GetAll(expression: expression, isTracking: false);

        categoriesQuery = categoriesQuery.OrderBy(category => category.Id);

        var categoriesList = await categoriesQuery.ToPagedList(@params).ToListAsync();

        return categoriesList.Select(category => new CategoryModel().MapFromEntity(category)).ToList();
    }


    public async ValueTask<CategoryModel> GetAsync(Expression<Func<Category, bool>> expression)
    {
        var category = await _categoryRepository.GetAsync(expression, false);
        if (category is null)
            throw new BbproException(404, "category_not_found");
        return new CategoryModel().MapFromEntity(category);
    }

    public async ValueTask<CategoryModel> UpdateAsync(int id, CategoryUpdateDto categoryDto)
    {
        var existingCategory = await _categoryRepository.GetAsync(p => p.Id == id);

        if (existingCategory == null)
        {
            throw new BbproException(404, "category_not_found");
        }

        if (categoryDto.Title != null)
        {
            if (categoryDto.Title.EN != null)
            {
                existingCategory.Title.EN = categoryDto.Title.EN;
            }
            if (categoryDto.Title.RU != null)
            {
                existingCategory.Title.RU = categoryDto.Title.RU;
            }
            if (categoryDto.Title.UZ != null)
            {
                existingCategory.Title.UZ = categoryDto.Title.UZ;
            }
        }

        existingCategory.UpdatedAt = DateTime.UtcNow;
        await _categoryRepository.SaveChangesAsync();
        return new CategoryModel().MapFromEntity(existingCategory);
    }
}
