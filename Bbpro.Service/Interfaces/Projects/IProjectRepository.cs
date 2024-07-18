using Bbpro.Domain.Configurations;
using Bbpro.Domain.Dto.Projects;
using Bbpro.Domain.Entities.Projects;
using Bbpro.Domain.Models.PaginationParams;
using Bbpro.Domain.Models.Projects;
using System.Linq.Expressions;

namespace Bbpro.Service.Interfaces.Projects;

public interface IProjectRepository
{
    ValueTask<PagedResult<ProjectModel>> GetAll(PaginationParams @params, Expression<Func<Project, bool>> expression = null);
    ValueTask<ProjectModel> GetAsync(Expression<Func<Project, bool>> expression);
    ValueTask<ProjectModel> CreateAsync(ProjectCreateDto projectCreateDTO);
    ValueTask<bool> DeleteAsync(int id);
    ValueTask<ProjectModel> UpdateAsync(int id, ProjectUpdateDto projectUpdateDTO);
}
