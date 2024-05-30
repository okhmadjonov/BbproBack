using Bbpro.Domain.Entities.Multilanguage;
using Bbpro.Domain.Entities.Projects;
using Bbpro.Domain.Entities.Solutions;
using Bbpro.Domain.Models.Solutions;

namespace Bbpro.Domain.Models.Projects;

public class ProjectModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ProjectModel MapFromEntity(Project entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl,
            Title = entity.Title,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
