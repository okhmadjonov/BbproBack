using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Projects;
using Bbpro.Domain.Entities.Projects;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.Projects;
using Bbpro.Service.Exceptions;
using Bbpro.Service.Extentions;
using Bbpro.Service.Interfaces.Projects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bbpro.Service.Services.Projects;

internal sealed class ProjectService : IProjectRepository
{
    private readonly IGenericRepository<Project> _projectRepository;
    public ProjectService(IGenericRepository<Project> projectRepositoriy)
    {
        _projectRepository = projectRepositoriy;
    }

    public async ValueTask<ProjectModel> CreateAsync(ProjectCreateDto project)
    {
        string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(project.ImageUrl.FileName));
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await project.ImageUrl.CopyToAsync(fileStream);
        }
        var addProject = new Project
        {
            Title = project.Title,
            Description = project.Description,
            DownloadLink = project.DownloadLink,
            ImageUrl = fileName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var createdProject = await _projectRepository.CreateAsync(addProject);
        await _projectRepository.SaveChangesAsync();
        return new ProjectModel().MapFromEntity(createdProject);
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var findProject = await _projectRepository.GetAsync(p => p.Id == id);
        if (findProject is null)
        {
            throw new BbproException(404, "project_not_found");
        }

        if (!string.IsNullOrEmpty(findProject.ImageUrl))
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", findProject.ImageUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await _projectRepository.DeleteAsync(id);
        await _projectRepository.SaveChangesAsync();
        return true;
    }

    public async ValueTask<IEnumerable<ProjectModel>> GetAll(PaginationParams @params, Expression<Func<Project, bool>> expression = null)
    {
        var projects = _projectRepository.GetAll(expression: expression, isTracking: false);
        var projectsList = await projects.ToPagedList(@params).ToListAsync();
        return projectsList.Select(e => new ProjectModel().MapFromEntity(e)).ToList();
    }

    public async ValueTask<ProjectModel> GetAsync(Expression<Func<Project, bool>> expression)
    {
        var project = await _projectRepository.GetAsync(expression, false);
        if (project is null)
            throw new BbproException(404, "project_not_found");
        return new ProjectModel().MapFromEntity(project);
    }

    public async ValueTask<ProjectModel> UpdateAsync(int id, ProjectUpdateDto project)
    {
        var existingProject = await _projectRepository.GetAsync(p => p.Id == id);
        if (existingProject == null)
        {
            throw new BbproException(404, "project_not_found");
        }

       
        if (project.Title != null)
        {
            if (project.Title.UZ is not null)
            {
                existingProject.Title.UZ = project.Title.UZ;
            }
            if (project.Title.RU is not null)
            {
                existingProject.Title.RU = project.Title.RU;
            }
            if (project.Title.EN is not null)
            {
                existingProject.Title.EN = project.Title.EN;
            }
        }
        
        if (project.DownloadLink != null)
        {
            if (project.DownloadLink.UZ is not null)
            {
                existingProject.DownloadLink.UZ = project.DownloadLink.UZ;
            }
            if (project.DownloadLink.RU is not null)
            {
                existingProject.DownloadLink.RU = project.DownloadLink.RU;
            }
            if (project.DownloadLink.EN is not null)
            {
                existingProject.DownloadLink.EN = project.DownloadLink.EN;
            }
        }
        

        if (project.Description != null)
        {
            if (project.Description.UZ is not null)
            {
                existingProject.Description.UZ = project.Description.UZ;
            }
            if (project.Description.RU is not null)
            {
                existingProject.Description.RU = project.Description.RU;
            }
            if (project.Description.EN is not null)
            {
                existingProject.Description.EN = project.Description.EN;
            }
        }

        existingProject.UpdatedAt = DateTime.UtcNow;

        if (project.ImageUrl != null)
        {
            if (!string.IsNullOrEmpty(existingProject.ImageUrl))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProject.ImageUrl);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(project.ImageUrl.FileName));
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await project.ImageUrl.CopyToAsync(fileStream);
            }

            existingProject.ImageUrl = fileName;
           
        }

        await _projectRepository.SaveChangesAsync();
        return new ProjectModel().MapFromEntity(existingProject);
    }
}
