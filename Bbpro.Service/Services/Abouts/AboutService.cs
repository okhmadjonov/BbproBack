using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.About;
using Bbpro.Domain.Entities.About;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.AboutModel;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Abouts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bbpro.Service.Services.Abouts;


internal sealed class AboutService : IAboutRepository
{
    private readonly IGenericRepository<About> _aboutRepository;

    public AboutService(IGenericRepository<About> aboutRepo)
    {
        _aboutRepository = aboutRepo;
    }
    public async ValueTask<AboutModel> CreateAsync(AboutCreateDto about)
    {
        string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(about.ImageUrl.FileName));
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await about.ImageUrl.CopyToAsync(fileStream);
        }

        var addAbout = new About
        {
            Title = about.Title,
            Description = about.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ImageUrl = fileName
        };
        var createdAbout = await _aboutRepository.CreateAsync(addAbout);
        await _aboutRepository.SaveChangesAsync();
        return new AboutModel().MapFromEntity(createdAbout);
    }
    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findAbout = await _aboutRepository.GetAsync(p => p.Id == id);
        if (findAbout is null)
        {
            throw new BbproException(404, "about_not_found");
        }

        if (!string.IsNullOrEmpty(findAbout.ImageUrl))
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", findAbout.ImageUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await _aboutRepository.DeleteAsync(id);
        await _aboutRepository.SaveChangesAsync();
        return true;
    }
    public async ValueTask<IEnumerable<AboutModel>> GetAll(PaginationParams @params, Expression<Func<About, bool>> expression = null)
    {
        var abouts = _aboutRepository.GetAll(expression: expression, isTracking: false);
        var aboutsList = await abouts.ToPagedList(@params).ToListAsync();
        return aboutsList.Select(e => new AboutModel().MapFromEntity(e)).ToList();
    }
    public async ValueTask<AboutModel> GetAsync(Expression<Func<About, bool>> expression)
    {
        var about = await _aboutRepository.GetAsync(expression, false);
        if (about is null)
            throw new BbproException(404, "about_not_found");
        return new AboutModel().MapFromEntity(about);
    }

    public async ValueTask<AboutModel> UpdateAsync(int id, AboutUpdateDto about)
    {
        var existingAbout = await _aboutRepository.GetAsync(p => p.Id == id);
        if (existingAbout == null)
        {
            throw new BbproException(404, "about_not_found");
        }
        if (about.Title != null)
        {
            if (about.Title.UZ is not null)
            {
                existingAbout.Title.UZ = about.Title.UZ;
            }
            if (about.Title.RU is not null)
            {
                existingAbout.Title.RU = about.Title.RU;
            }
            if (about.Title.EN is not null)
            {
                existingAbout.Title.EN = about.Title.EN;
            }
        }
        if (about.Description != null)
        {
            if (about.Description.UZ is not null)
            {
                existingAbout.Description.UZ = about.Description.UZ;
            }
            if (about.Description.RU is not null)
            {
                existingAbout.Description.RU = about.Description.RU;
            }
            if (about.Description.EN is not null)
            {
                existingAbout.Description.EN = about.Description.EN;
            }
        }
        existingAbout.UpdatedAt = DateTime.UtcNow;

        if (about.ImageUrl != null)
        {
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingAbout.ImageUrl);
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }

            string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(about.ImageUrl.FileName));
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await about.ImageUrl.CopyToAsync(fileStream);
            }
            existingAbout.ImageUrl = fileName;
        }
        await _aboutRepository.SaveChangesAsync();
        return new AboutModel().MapFromEntity(existingAbout);
    }
}
