using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Solutions;
using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Entities.Solutions;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.Latests;
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

    public SolutionService(IGenericRepository<Solution> solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }

    public async ValueTask<SolutionModel> CreateAsync(SolutionCreateDto solution)
    {
        string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(solution.ImageUrl.FileName));
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await solution.ImageUrl.CopyToAsync(fileStream);
        }
        var addSolution = new Solution
        {
            Title = solution.Title,
            Description = solution.Description,
            ImageUrl = fileName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var createdSolution = await _solutionRepository.CreateAsync(addSolution);
        await _solutionRepository.SaveChangesAsync();
        return new SolutionModel().MapFromEntity(createdSolution);
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
        var solutions = _solutionRepository.GetAll(expression: expression, isTracking: false);
        var solutionsList = await solutions.ToPagedList(@params).ToListAsync();
        return solutionsList.Select(e => new SolutionModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<SolutionModel> GetAsync(Expression<Func<Solution, bool>> expression)
    {
        var solution = await _solutionRepository.GetAsync(expression, false);
        if (solution is null)
            throw new BbproException(404, "solution_not_found");
        return new SolutionModel().MapFromEntity(solution);
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

        await _solutionRepository.SaveChangesAsync();
        return new SolutionModel().MapFromEntity(existingSolution);
    }
}
