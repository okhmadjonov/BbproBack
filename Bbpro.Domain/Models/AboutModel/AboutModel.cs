using Bbpro.Domain.Entities.About;
using Bbpro.Domain.Entities.Multilanguage;

namespace Bbpro.Domain.Models.AboutModel;

public class AboutModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual AboutModel MapFromEntity(About entity)
    {
        return new AboutModel
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
