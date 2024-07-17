using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Solutions;
using Bbpro.Domain.Entities.Categories;
using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Entities.Solutions;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.Latests;
using Bbpro.Domain.Models.PaginationParams;
using Bbpro.Domain.Models.Projects;
using Bbpro.Domain.Models.Solutions;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Latests;
using Bbpro.Service.Interfaces.Solutions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bbpro.Service.Services.Solutions;

internal sealed class SolutionService : ISolutionRepository
{
    private readonly IGenericRepository<Solution> _solutionRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IGenericRepository<CategoryConnectSolution> _categoryConnectSolutionRepository;
    public SolutionService(IGenericRepository<Solution> solutionRepository, IGenericRepository<CategoryConnectSolution> categoryConnectSolutionRepository, IGenericRepository<Category> categoryRepository)
    {
        _solutionRepository = solutionRepository;
        _categoryRepository = categoryRepository;
        _categoryConnectSolutionRepository = categoryConnectSolutionRepository;
    }

    public async ValueTask<SolutionModel> CreateAsync(SolutionCreateDto solutionDto)
    {
        string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(solutionDto.ImageUrl.FileName));
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await solutionDto.ImageUrl.CopyToAsync(fileStream);
        }
        var solution = new Solution
        {
            Title = solutionDto.Title,
            Description = solutionDto.Description,
            ImageUrl = fileName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var createdSolution = await _solutionRepository.CreateAsync(solution);
        await _solutionRepository.SaveChangesAsync();
        var getCategoryById = await _categoryRepository.GetAsync(c => c.Id == solutionDto.CategoryId);
        if (getCategoryById == null)
        {
            throw new BbproException(404, "category_not_found");
        }

        var solutionAddCategory = new CategoryConnectSolution
        {
            SolutionId = solution.Id,
            CategoryId = getCategoryById.Id,
        };
        await _categoryConnectSolutionRepository.CreateAsync(solutionAddCategory);
        await _solutionRepository.SaveChangesAsync();

        return new SolutionModel().MapFromEntity(createdSolution, getCategoryById.Id);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findSolution = await _solutionRepository.GetAsync(p => p.Id == id);
        if (findSolution is null)
        {
            throw new BbproException(404, "solution_not_found");
        }

        if (!string.IsNullOrEmpty(findSolution.ImageUrl))
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", findSolution.ImageUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await _solutionRepository.DeleteAsync(id);
        await _solutionRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<SolutionModel>> GetAll(PaginationParams @params, Expression<Func<Solution, bool>> expression = null)
    {
        var solutions = _solutionRepository.GetAll(expression: expression, isTracking: false)
             .OrderByDescending(e => e.Id); 
        var solutionsList = await solutions.ToPagedList(@params).ToListAsync();
        var solutionModels = new List<SolutionModel>();

        foreach (var solution in solutionsList)
        {
            var categoryId = await _categoryConnectSolutionRepository.GetAsync(ccp => ccp.SolutionId == solution.Id);

            if (categoryId == null)
            {
                continue;
            }

            var solutionModel = new SolutionModel().MapFromEntity(solution, categoryId.CategoryId);
            solutionModels.Add(solutionModel);
        }

        return solutionModels;
    }

    public async ValueTask<SolutionModel> GetAsync(Expression<Func<Solution, bool>> expression)
    {
        var solution = await _solutionRepository.GetAsync(expression, false);
        if (solution is null)
            throw new BbproException(404, "solution_not_found");
        var categoryId = await _categoryConnectSolutionRepository.GetAsync(ccp => ccp.SolutionId == solution.Id);
        if (categoryId == null)
            throw new BbproException(404, "category_not_found");

        return new SolutionModel().MapFromEntity(solution, categoryId.CategoryId);
    }

    public async ValueTask<SolutionModel> UpdateAsync(int id, SolutionUpdateDto solutionUpdateDTO)
    {
        var existingSolution = await _solutionRepository.GetAsync(p => p.Id == id);
        if (existingSolution == null)
        {
            throw new BbproException(404, "solution_not_found");
        }

        if (solutionUpdateDTO.Title != null)
        {
            if (solutionUpdateDTO.Title.UZ is not null)
            {
                existingSolution.Title.UZ = solutionUpdateDTO.Title.UZ;
            }
            if (solutionUpdateDTO.Title.RU is not null)
            {
                existingSolution.Title.RU = solutionUpdateDTO.Title.RU;
            }
            if (solutionUpdateDTO.Title.EN is not null)
            {
                existingSolution.Title.EN = solutionUpdateDTO.Title.EN;
            }
        }
        if (solutionUpdateDTO.Description != null)
        {
            if (solutionUpdateDTO.Description.UZ is not null)
            {
                existingSolution.Description.UZ = solutionUpdateDTO.Description.UZ;
            }
            if (solutionUpdateDTO.Description.RU is not null)
            {
                existingSolution.Description.RU = solutionUpdateDTO.Description.RU;
            }
            if (solutionUpdateDTO.Description.EN is not null)
            {
                existingSolution.Description.EN = solutionUpdateDTO.Description.EN;
            }
        }

        existingSolution.UpdatedAt = DateTime.UtcNow;

        if (solutionUpdateDTO.ImageUrl != null)
        {
            if (!string.IsNullOrEmpty(existingSolution.ImageUrl))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingSolution.ImageUrl);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(solutionUpdateDTO.ImageUrl.FileName));
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await solutionUpdateDTO.ImageUrl.CopyToAsync(fileStream);
            }

            existingSolution.ImageUrl = fileName;
        }
        var checkCategory = await _categoryRepository.GetAsync(c => c.Id == solutionUpdateDTO.CategoryId);
        if (checkCategory == null)
        {
            throw new BbproException(404, "category_is_not_found");
        }

        var getSolutionRelation = await _categoryConnectSolutionRepository.GetAsync(ccp => ccp.SolutionId == existingSolution.Id);
        getSolutionRelation.CategoryId = solutionUpdateDTO.CategoryId;
        _categoryConnectSolutionRepository.Update(getSolutionRelation);

        _solutionRepository.Update(existingSolution);
        await _solutionRepository.SaveChangesAsync();
        return new SolutionModel().MapFromEntity(existingSolution, checkCategory.Id);
    }


    public async ValueTask<PagedResult<SolutionFilterCategoryModel>> GetSolutionsByCategoryId(PaginationParams @params, int categoryId)
    {
       
        Solution solution;
        var solutionsQuery = _categoryConnectSolutionRepository.GetAll(ccp => ccp.CategoryId == categoryId)
                                                             .Include(ccp => ccp.Solution)
                                                             .OrderByDescending(ccp=> ccp.Solution.Id)
                                                             .AsQueryable();

        var solutionsList = await solutionsQuery.ToPagedList(@params).ToListAsync();

        var resultList = new List<SolutionFilterCategoryModel>();

        foreach (var solutionCategory in solutionsList)
        {
            solution = solutionCategory.Solution;
         
            var result = new SolutionFilterCategoryModel().MapFromEntity(solution);

            resultList.Add(result);
        }

        int totalCount = await solutionsQuery.CountAsync();
        if (totalCount == 0)
        {
            return PagedResult<SolutionFilterCategoryModel>.Create(
                Enumerable.Empty<SolutionFilterCategoryModel>(),
                0,
                @params.PageSize,
                0,
                @params.PageIndex,
                0
            );
        }
        if (@params.PageIndex == 0)
        {
            @params.PageIndex = 1;
        }

        if (@params.PageSize == 0)
        {
            @params.PageSize = totalCount;
        }

        int itemsPerPage = @params.PageSize;
        int totalPages = (totalCount / itemsPerPage) + (totalCount % itemsPerPage == 0 ? 0 : 1);


        var pagedResult = PagedResult<SolutionFilterCategoryModel>.Create(resultList,
                totalCount,
                itemsPerPage,
                resultList.Count,
                @params.PageIndex,
                totalPages
                );

        return pagedResult;
    }

}
