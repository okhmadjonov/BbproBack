using Bbpro.Domain.Entities.Latests;
using Bbpro.Domain.Entities.Multilanguage;
using Bbpro.Domain.Entities.Projects;
using Bbpro.Domain.Models.Projects;

namespace Bbpro.Domain.Models.Latests;

public class LatestModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual LatestModel MapFromEntity(Latest entity)
    {
        return new LatestModel
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
