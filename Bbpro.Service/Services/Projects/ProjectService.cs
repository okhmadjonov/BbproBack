using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Projects;
using Bbpro.Domain.Entities.Projects;
using Bbpro.Domain.Interface;
using Bbpro.Domain.Models.PaginationParams;
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

    public async ValueTask<ProjectModel> CreateAsync(ProjectCreateDto projectCreateDTO)
    {
        string folderName = "";
        string downloadLink = "";
        string imageUrl = "";

       
        if (projectCreateDTO.DownloadLink != null && projectCreateDTO.DownloadLink.Length > 0)
        {
            string templatesRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates");
            if (!Directory.Exists(templatesRootPath))
                Directory.CreateDirectory(templatesRootPath);

            int folderCount = Directory.GetDirectories(templatesRootPath).Length + 1;
            folderName = folderCount.ToString("D2");

            string templatesPath = Path.Combine(templatesRootPath, folderName);
            Directory.CreateDirectory(templatesPath);

            using (var archive = new System.IO.Compression.ZipArchive(projectCreateDTO.DownloadLink.OpenReadStream()))
            {
                foreach (var entry in archive.Entries)
                {
                    string destinationPath = Path.Combine(templatesPath, entry.FullName);
                    if (string.IsNullOrEmpty(entry.Name))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                        continue;
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    using (var fileStream = new FileStream(destinationPath, FileMode.Create))
                    using (var entryStream = entry.Open())
                    {
                        await entryStream.CopyToAsync(fileStream);
                    }
                }
            }

            string indexPath = Path.Combine(templatesPath, "index.html");
            if (File.Exists(indexPath))
            {
                downloadLink = $"/templates/{folderName}/index.html";
            }
            else
            {
                throw new BbproException(400, "index_file_not_found");
            }
        }

       
        if (projectCreateDTO.ImageUrl != null && projectCreateDTO.ImageUrl.Length > 0)
        {
          
            string fileName = Path.Combine("images", Guid.NewGuid().ToString("N") + Path.GetExtension(projectCreateDTO.ImageUrl.FileName));
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await projectCreateDTO.ImageUrl.CopyToAsync(fileStream);
            }

          
            imageUrl = $"/images/{Path.GetFileName(fileName)}";
        }

       
        var addProject = new Project
        {
            Title = projectCreateDTO.Title,
            Description = projectCreateDTO.Description,
            DownloadLink = downloadLink, 
            ImageUrl = imageUrl,          
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", findProject.ImageUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        await _projectRepository.DeleteAsync(id);
        await _projectRepository.SaveChangesAsync();
        return true;
    }


    public async ValueTask<PagedResult<ProjectModel>> GetAll(PaginationParams @params, Expression<Func<Project, bool>> expression = null)
    {
        var projects = _projectRepository.GetAll(expression: expression, isTracking: false)
                                          .OrderByDescending(e => e.Id)
                                          .AsQueryable();

        var projectsList = await projects.ToPagedList(@params).ToListAsync();
        var resultList = projectsList.Select(e => new ProjectModel().MapFromEntity(e)).ToList();

        int totalCount = await projects.CountAsync();
        if (totalCount == 0)
        {
            return PagedResult<ProjectModel>.Create(
                Enumerable.Empty<ProjectModel>(),
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

        var pagedResult = PagedResult<ProjectModel>.Create(
            resultList,
            totalCount,
            itemsPerPage,
            resultList.Count,
            @params.PageIndex,
            totalPages
        );

        return pagedResult;
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

        existingProject.DownloadLink = project.DownloadLink;

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
