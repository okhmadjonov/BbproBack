using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Latests;
using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.Latests;
using Bbpro.Domain.Models.PaginationParams;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Latests;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bbpro.Service.Services.Latests;

internal sealed class LatestService : ILatestRepository
{
    private readonly IGenericRepository<Latest> _latestRepository;
    public LatestService(IGenericRepository<Latest> latestRepository)
    {
        _latestRepository = latestRepository;        
    }

    public async ValueTask<LatestModel> CreateAsync(LatestCreateDto latest)
    {
        string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(latest.ImageUrl.FileName));
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await latest.ImageUrl.CopyToAsync(fileStream);
        }
        var addLatest = new Latest
        {
            Title = latest.Title,
            Description = latest.Description,
            ImageUrl = fileName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var createdLatest = await _latestRepository.CreateAsync(addLatest);
        await _latestRepository.SaveChangesAsync();
        return new LatestModel().MapFromEntity(createdLatest);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findLatest = await _latestRepository.GetAsync(p => p.Id == id);
        if (findLatest is null)
        {
            throw new BbproException(404, "latest_not_found");
        }

        if (!string.IsNullOrEmpty(findLatest.ImageUrl))
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", findLatest.ImageUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await _latestRepository.DeleteAsync(id);
        await _latestRepository.SaveChangesAsync();
        return true;
    }
    /*
    public async ValueTask<IEnumerable<LatestModel>> GetAll(PaginationParams @params, Expression<Func<Latest, bool>> expression = null)
    {
      
        var latests = _latestRepository.GetAll(expression: expression, isTracking: false)
                              
                                  .OrderByDescending(e => e.Id);

        var latestsList = await latests.ToPagedList(@params).ToListAsync();
        return latestsList.Select(e => new LatestModel().MapFromEntity(e)).ToList();
    }

  */
    
    public async ValueTask<PagedResult<LatestModel>> GetAll(PaginationParams @params, Expression<Func<Latest, bool>> expression = null)
    {
        var latests = _latestRepository.GetAll(expression: expression, isTracking: false)
                                       .OrderByDescending(e => e.Id)
                                       .AsQueryable();

        var latestsList = await latests.ToPagedList(@params).ToListAsync();

        var resultList = latestsList.Select(e => new LatestModel().MapFromEntity(e)).ToList();

        int totalCount = await latests.CountAsync();
        if (totalCount == 0)
        {
            return PagedResult<LatestModel>.Create(
                Enumerable.Empty<LatestModel>(),
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

        var pagedResult = PagedResult<LatestModel>.Create(
            resultList,
            totalCount,
            itemsPerPage,
            resultList.Count,
            @params.PageIndex,
            totalPages
        );

        return pagedResult;
    }
    


    public async ValueTask<LatestModel> GetAsync(Expression<Func<Latest, bool>> expression)
    {
        var latest = await _latestRepository.GetAsync(expression, false);
        if (latest is null)
            throw new BbproException(404, "latest_not_found");
        return new LatestModel().MapFromEntity(latest);
    }

    public async ValueTask<LatestModel> UpdateAsync(int id, LatestUpdateDto latest)
    {
        var existingLatest = await _latestRepository.GetAsync(p => p.Id == id);
        if (existingLatest == null)
        {
            throw new BbproException(404, "latest_not_found");
        }

        if (latest.Title != null)
        {
            if (latest.Title.UZ is not null)
            {
                existingLatest.Title.UZ = latest.Title.UZ;
            }
            if (latest.Title.RU is not null)
            {
                existingLatest.Title.RU = latest.Title.RU;
            }
            if (latest.Title.EN is not null)
            {
                existingLatest.Title.EN = latest.Title.EN;
            }
        }
        if (latest.Description != null)
        {
            if (latest.Description.UZ is not null)
            {
                existingLatest.Description.UZ = latest.Description.UZ;
            }
            if (latest.Description.RU is not null)
            {
                existingLatest.Description.RU = latest.Description.RU;
            }
            if (latest.Description.EN is not null)
            {
                existingLatest.Description.EN = latest.Description.EN;
            }

        }

        existingLatest.UpdatedAt = DateTime.UtcNow;

        if (latest.ImageUrl != null)
        {
            if (!string.IsNullOrEmpty(existingLatest.ImageUrl))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingLatest.ImageUrl);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(latest.ImageUrl.FileName));
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await latest.ImageUrl.CopyToAsync(fileStream);
            }

            existingLatest.ImageUrl = fileName;
        }

        await _latestRepository.SaveChangesAsync();
        return new LatestModel().MapFromEntity(existingLatest);
    }
}
