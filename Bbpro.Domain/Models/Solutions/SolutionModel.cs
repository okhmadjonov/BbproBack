using Bbpro.Domain.Entities.Multilanguage;
using Bbpro.Domain.Entities.Solutions;

namespace Bbpro.Domain.Models.Solutions;

public class SolutionModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual SolutionModel MapFromEntity(Solution entity)
    {
        return new SolutionModel
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
